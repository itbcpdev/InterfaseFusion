using System.Configuration;
using System.ComponentModel;
using NLog;
using FusionClass;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using System.Globalization;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace InterfaceFusion
{
    public partial class frmFusion : Form
    {

        IOP_TRANRepository op_tranRepository;
        ITANKS_INFORepository tanks_infoRepository;

        FusionClass.Fusion cFusion = new FusionClass.Fusion();

        public readonly int delayWorker = Convert.ToInt16(ConfigurationManager.AppSettings["GetSaleInterval"]) * 1000; //Intervalo en segundos según configuración
        public string? testMode = ConfigurationManager.AppSettings["TestMode"];
        readonly Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public frmFusion()
        {
            InitializeComponent();                       
        }

        private void frmFusion_Load(object sender, EventArgs e)
        {
            txtFusionIp.Text = ConfigurationManager.AppSettings["IpFusion"];
            op_tranRepository = new OP_TRANRepository();

            SendLogToServer();

            try
            {
                dgvTransactions.DataSource = op_tranRepository.GetAllOP_TRAN();
                //cFusion.Connection(txtFusionIp.Text);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
            }
            
            tmrFusionProcesses.Enabled = false;
            tmrFusionProcesses.Interval = delayWorker;

            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }

        }

        private void tmrFusionProcesses_Tick(object sender, EventArgs e)
        {

            tmrFusionProcesses.Enabled = false;

            op_tranRepository = new OP_TRANRepository();

            int LastFusionSaleId = 0;
            int LastSigesSaleId = 0;
            int PendingTransactions = 0;
            OP_TRAN op_tran = new OP_TRAN();
            bool CheckIdOpTran = false;

            try
            {
                // Obtener última transacción de Fusion

                LastFusionSaleId = GetLastSaleFusion();              

                // Obtener última transacción de Siges

                LastSigesSaleId = op_tranRepository.GetLastOP_TRAN();

                // Obtener diferencia según Id

                PendingTransactions = LastFusionSaleId - LastSigesSaleId;

                if (PendingTransactions > 0)
                {
                    for (int count = 1; count <= PendingTransactions; count = count + 1)
                    {

                        LastSigesSaleId = LastSigesSaleId + 1;

                        // Obtener transacciones de Fusion según Id

                        op_tran = FusionSaleById(LastSigesSaleId);

                        // Insertar en tabla OP_TRAN

                        if (op_tran.CONTROLADOR != null)
                        {

                            // Si el Id no existe en OP_TRAN se inserta

                            CheckIdOpTran = op_tranRepository.CheckIdOP_TRAN(op_tran.C_INTERNO);

                            if (CheckIdOpTran == false)
                            {
                                op_tranRepository.Insert(op_tran);

                                //Marcar transacción

                                bool ClearSale = false;

                                ClearSale = ClearSaleFusion(op_tran.C_INTERNO);

                                if (!ClearSale)
                                {
                                    //Log.Error("Error al marcar venta");
                                    //MessageBox.Show("Error al marcar venta");
                                }
                            }
                        } 
                    }

                    //Obtener transacciones OP_TRAN

                    //dgvTransactions.DataSource = op_tranRepository.GetAllOP_TRAN();

                }

                //Obtener transacciones OP_TRAN

                //dgvTransactions.DataSource = op_tranRepository.GetAllOP_TRAN();

            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ShiftClose();
            }

            tmrFusionProcesses.Enabled = true;

        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            txtMessage.Text = "Intentando conectarse a Fusion";
            
            try
            {
                cFusion.Connection(txtFusionIp.Text);
                txtMessage.Text = "Conexión establecida";
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                txtMessage.Text = "No se pudo establecer conexión";
            }
            finally
            { 
            }
            
            //cFusion.Close();
        }

        public OP_TRAN FusionSaleById(int id)
        {

            OP_TRAN op_tran = new OP_TRAN();
            string? divideAmounts = ConfigurationManager.AppSettings["DivideAmounts"];

            //cFusion.Connection(txtFusionIp.Text);

            if (cFusion.ConnectionStatus())
            {
                FusionClass.FusionSale cFusionSale = new FusionClass.FusionSale();

                try
                {
                    cFusion.GetSale(id, cFusionSale);

                    int nro = cFusionSale.GetPumpNr();

                    if (cFusionSale.GetPumpNr() != 0)
                    {

                        string gradeName = "";

                        cFusion.GetGrade(cFusionSale.GetGradeNr(), ref gradeName);

                        //string totalVolume = "";
                        //string totalMoney = "";
                        //cFusion.GetTotalizers(cFusionSale.GetPumpNr(), cFusionSale.GetHoseNr(), ref totalVolume, ref totalMoney);

                        //logger.Info("SOLES: " + cFusionSale.GetAmount());

                        op_tran.C_INTERNO = cFusionSale.GetSaleID();
                        op_tran.CONTROLADOR = "01";
                        op_tran.NUMERO = Convert.ToString(cFusionSale.GetSaleID());
                        op_tran.PRODUCTO = GetProductId(gradeName);

                        if (divideAmounts == "1")
                        {
                            op_tran.SOLES = decimal.Parse(cFusionSale.GetAmount(), CultureInfo.InvariantCulture) / 100;
                            op_tran.PRECIO = decimal.Parse(cFusionSale.GetPPU(), CultureInfo.InvariantCulture) / 100000;
                            op_tran.GALONES = decimal.Parse(cFusionSale.GetVolume(), CultureInfo.InvariantCulture) / 100000;
                            op_tran.VolumenFinal = decimal.Parse(cFusionSale.GetFinalVolume(), CultureInfo.InvariantCulture) / 100000;

                            //op_tran.SOLES = Convert.ToDecimal(cFusionSale.GetAmount()) / 100;
                            //op_tran.PRECIO = Convert.ToDecimal(cFusionSale.GetPPU()) / 100000;
                            //op_tran.GALONES = Convert.ToDecimal(cFusionSale.GetVolume()) / 100000;
                            //op_tran.VolumenFinal = Convert.ToDecimal(cFusionSale.GetFinalVolume()) / 100000;
                        }
                        else 
                        {
                            op_tran.SOLES = decimal.Parse(cFusionSale.GetAmount(), CultureInfo.InvariantCulture);
                            op_tran.PRECIO = decimal.Parse(cFusionSale.GetPPU(), CultureInfo.InvariantCulture);
                            op_tran.GALONES = decimal.Parse(cFusionSale.GetVolume(), CultureInfo.InvariantCulture);
                            op_tran.VolumenFinal = decimal.Parse(cFusionSale.GetFinalVolume(), CultureInfo.InvariantCulture);

                            //op_tran.SOLES = Convert.ToDecimal(cFusionSale.GetAmount());
                            //op_tran.PRECIO = Convert.ToDecimal(cFusionSale.GetPPU());
                            //op_tran.GALONES = Convert.ToDecimal(cFusionSale.GetVolume());
                            //op_tran.VolumenFinal = Convert.ToDecimal(cFusionSale.GetFinalVolume());
                        }

                        op_tran.CARA = Convert.ToString(cFusionSale.GetPumpNr()).PadLeft(2, '0');
                        op_tran.FECHA = DateTime.Now.Date;
                        op_tran.HORA = DateTime.Now;
                        op_tran.TURNO = "1";
                        op_tran.ESTADO = "";
                        op_tran.DOCUMENTO = null;
                        op_tran.DATEPROCE = null;
                        op_tran.CDTIPODOC = null;
                        op_tran.MANGUERA = cFusionSale.GetHoseNr();
                        op_tran.FECSISTEMA = DateTime.Now;                                             
                        op_tran.MontoFinal = 0;
                        //op_tran.VolumenFinal = Convert.ToDecimal(totalVolume);
                        //op_tran.MontoFinal = Convert.ToDecimal(totalMoney);
                        op_tran.IdTran = null;

                    }

                }
                catch (Exception ex)
                {
                    logger.Error(ex, ex.Message);
                    MessageBox.Show(ex.Message);
                }
                finally
                {           
                }

                //cFusion.Close();
            }

            return op_tran;
        }

        public TANKS_INFO GetTankInfo(int tank) 
        {
            TANKS_INFO tanks_info = new TANKS_INFO();

            if (cFusion.ConnectionStatus())
            {

                try
                {

                    FusionTankInfo info = new FusionTankInfo();

                    cFusion.GetTankInfo(tank, info);

                    string gradeName = "";

                    cFusion.GetGrade(info.GetProductNr(), ref gradeName);
                    
                    tanks_info.TankNr = info.GetTankNr();
                    tanks_info.ProductNr = info.GetProductNr();
                    tanks_info.ProductId = GetProductId(gradeName);
                    tanks_info.DatelastMeasure = info.GetDateLastMeasure();
                    tanks_info.TimeLastMeasure = info.GetTimeLastMeasure();
                    tanks_info.Alarms = info.GetAlarms();
                    tanks_info.FuelHeight = decimal.Parse(info.GetFuelHeight(), CultureInfo.InvariantCulture);
                    tanks_info.FuelTemp = decimal.Parse(info.GetFuelTemperature(), CultureInfo.InvariantCulture);
                    tanks_info.FuelVolume = decimal.Parse(info.GetFuelVolume(), CultureInfo.InvariantCulture);
                    //tanks_info.FuelHeight = Convert.ToDecimal(info.GetFuelHeight());
                    //tanks_info.FuelTemp = Convert.ToDecimal(info.GetFuelTemperature());
                    //tanks_info.FuelVolume = Convert.ToDecimal(info.GetFuelVolume());
                    tanks_info.MeasureUnit = info.GetTankMeasureUnitType();
                    tanks_info.TemperatureUnit = info.GetTankTemperatureUnitType();
                    tanks_info.WaterHeight = decimal.Parse(info.GetWaterHeight(), CultureInfo.InvariantCulture);
                    tanks_info.WaterVolume = decimal.Parse(info.GetWaterVolume(), CultureInfo.InvariantCulture);
                    //tanks_info.WaterHeight = Convert.ToDecimal(info.GetWaterHeight());
                    //tanks_info.WaterVolume = Convert.ToDecimal(info.GetWaterVolume());

                }
                catch (Exception ex)
                {
                    logger.Error(ex, ex.Message);
                    MessageBox.Show(ex.Message);
                }
            }

            return tanks_info;

        }

        public int GetLastSaleFusion()
        {
            //cFusion.Connection(txtFusionIp.Text);

            FusionClass.FusionSale cFusionSale = new FusionClass.FusionSale();
            
            int lastSaleId = 0;

            try
            {
                cFusion.GetLastSale(cFusionSale);
                lastSaleId = cFusionSale.GetSaleID();
            }
            catch (Exception ex)
            {
                //cFusion.Close();
                logger.Error(ex, ex.Message);
                //MessageBox.Show(ex.StackTrace);
            }
            finally 
            { 
            }

            //cFusion.Close();

            return lastSaleId;
        }

        public bool ClearSaleFusion(int id)
        {
            //cFusion.Connection(txtFusionIp.Text);

            string error = "";
            string errorCode = "";

            try
            {
                cFusion.ClearSale(ref id, "POS", "InvoicedBy=POS", ref error, ref errorCode);
            }
            catch (Exception ex)
            {
                //cFusion.Close();
                logger.Error(ex, ex.Message);
                //MessageBox.Show(ex.StackTrace);                
                return false;
            }
            finally 
            { 
            }

            //cFusion.Close();

            return true;
        }

        public bool ShiftClose()
        {
            InterfaceData interfaceData = new InterfaceData();

            interfaceData = op_tranRepository.GetInterfaceData();

            if (interfaceData.Cierre == "1")
            {

                //Cerrar turno en controlador

                //cFusion.Connection(txtFusionIp.Text);

                string type = "S";
                string status = "";
                string message = "";
                string errorCode = "";
                string pid = "";
                string periodType = "";

                try
                {
                    cFusion.ShiftClose(type, ref status, ref message, ref errorCode, ref pid, ref periodType);
                }
                catch (Exception ex)
                {
                    //cFusion.Close();
                    logger.Error(ex, ex.Message);
                    //MessageBox.Show(ex.StackTrace);
                    return false;
                }
                finally
                {
                    op_tranRepository.UpdateShift();
                    dgvTransactions.DataSource = op_tranRepository.GetAllOP_TRAN();
                    //cFusion.Close();
                }

            }

            return true;
        }

        public bool Reconnect()
        {

            tmrFusionProcesses.Enabled = false;

            try
            {
                logger.Info("Haciendo reconección");
                cFusion.Close();
                cFusion.Connection(txtFusionIp.Text);
            }
            catch (Exception ex)
            {
                tmrFusionProcesses.Enabled = true;
                logger.Error(ex, ex.Message);
                //MessageBox.Show(ex.StackTrace);
                return false;
            }
            finally
            {                 
            }

            tmrFusionProcesses.Enabled = true;

            return true;
        }

        public string GetProductId(string gradeName)
        {

            string? nomG84 = ConfigurationManager.AppSettings["01"];
            string? nomG90 = ConfigurationManager.AppSettings["02"];
            string? nomG95 = ConfigurationManager.AppSettings["03"];
            string? nomG97 = ConfigurationManager.AppSettings["04"];
            string? nomDB5 = ConfigurationManager.AppSettings["05"];
            string? nomGLP = ConfigurationManager.AppSettings["06"];
            string? nomGR = ConfigurationManager.AppSettings["12"];
            string? nomGP = ConfigurationManager.AppSettings["13"];

            string productId = "";

            gradeName = Regex.Replace(gradeName, @"\s+", "");

            if (gradeName == nomG84)
            {
                productId = "01";
            }
            if (gradeName == nomG90)
            {
                productId = "02";
            }
            if (gradeName == nomG95)
            {
                productId = "03";
            }
            if (gradeName == nomG97)
            {
                productId = "04";
            }
            if (gradeName == nomDB5)
            {
                productId = "05";
            }
            if (gradeName == nomGLP)
            {
                productId = "06";
            }
            if (gradeName == nomGR)
            {
                productId = "12";
            }
            if (gradeName == nomGP)
            {
                productId = "13";
            }

            if (productId == null)
            {
                productId = gradeName;
            }

            return productId;
        }

        private void frmFusion_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                cFusion.Close();                
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                //MessageBox.Show(ex.StackTrace);                
            }
            finally
            { 
            }
        }

        private void btnRefreshData_Click(object sender, EventArgs e)
        {            
            dgvTransactions.DataSource = op_tranRepository.GetAllOP_TRAN();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            cFusion.Connection(txtFusionIp.Text);

            while (!backgroundWorker1.CancellationPending)
            {
                op_tranRepository = new OP_TRANRepository();
                tanks_infoRepository = new TANKS_INFORepository();

                int LastFusionSaleId = 0;
                int LastSigesSaleId = 0;
                int PendingTransactions = 0;
                OP_TRAN op_tran = new OP_TRAN();
                TANKS_INFO tanks_info = new TANKS_INFO();
                TANKS_INFO_HIST tanks_info_hist = new TANKS_INFO_HIST();
                bool CheckIdOpTran = false;
                bool CheckTankNumber = false;

                try
                {
                    
                    // Obtener última transacción de Fusion

                    LastFusionSaleId = GetLastSaleFusion();
                    
                    logger.Info("Última transacción Fusion: " + LastFusionSaleId.ToString());

                    // Obtener última transacción de Siges

                    LastSigesSaleId = op_tranRepository.GetLastOP_TRAN();

                    logger.Info("Última transacción Siges: " + LastSigesSaleId.ToString());

                    // Obtener diferencia según Id

                    PendingTransactions = LastFusionSaleId - LastSigesSaleId;

                    if (PendingTransactions > 0)
                    {
                        for (int count = 1; count <= PendingTransactions; count = count + 1)
                        {                            
                            LastSigesSaleId = LastSigesSaleId + 1;

                            logger.Info("Obteniendo transacción: " + LastSigesSaleId.ToString());

                            // Obtener transacciones de Fusion según Id

                            op_tran = FusionSaleById(LastSigesSaleId);

                            // Insertar en tabla OP_TRAN

                            if (op_tran.CONTROLADOR != null)
                            {

                                // Si el Id no existe en OP_TRAN se inserta

                                logger.Info("Revisando existencia de transacción en Siges: " + LastSigesSaleId.ToString());

                                CheckIdOpTran = op_tranRepository.CheckIdOP_TRAN(op_tran.C_INTERNO);

                                if (CheckIdOpTran == false)
                                {

                                    logger.Info("Insertando en Siges la transacción: " + LastSigesSaleId.ToString());

                                    if (testMode == "1")
                                    {
                                        logger.Info(JsonConvert.SerializeObject(op_tran));
                                    }
                                    else
                                    {
                                        op_tranRepository.Insert(op_tran);

                                        //Marcar transacción

                                        bool ClearSale = false;

                                        logger.Info("Marcando en Fusion transacción: " + LastSigesSaleId.ToString());

                                        ClearSale = ClearSaleFusion(op_tran.C_INTERNO);

                                        if (!ClearSale)
                                        {
                                            logger.Error("Error al marcar venta");
                                            //MessageBox.Show("Error al marcar venta");
                                        }
                                    }                                                                 
                                }
                            }
                            else
                            {
                                logger.Warn("No fue posible obtener la transacción: " + LastSigesSaleId.ToString());
                                Reconnect();
                            }
                        }

                        //Obtener transacciones OP_TRAN

                        var optran = op_tranRepository.GetAllOP_TRAN();

                        dgvTransactions.Invoke(new Action(() =>
                        {
                            dgvTransactions.DataSource = optran;
                        }));

                    }

                    //Obtener información de tanques

                    int tanksNumber = Convert.ToInt16(ConfigurationManager.AppSettings["TanksNumber"]) + 1;

                    for (int i = 1; i < tanksNumber; i++)
                    {

                        logger.Info("Obteniendo info de tanque: " + i);

                        tanks_info = GetTankInfo(i);
                        DateTime dateTimeMeasure = DateTime.Now;

                        if (tanks_info.ProductNr > 0)
                        {
                            CheckTankNumber = tanks_infoRepository.CheckTankTANK_INFO(tanks_info.TankNr);

                            if (CheckTankNumber == false)
                            {
                                logger.Info("Insertando info de tanque: " + i);

                                tanks_info.Estado = "A";

                                tanks_infoRepository.Insert(tanks_info);

                            }
                            else
                            {
                                logger.Info("Actualizando info de tanque: " + i);

                                tanks_info.Estado = "M";

                                tanks_infoRepository.Update(tanks_info);
                            }

                            logger.Info("Insertando en historial info de tanque: " + i);

                            tanks_info_hist.TankNr = tanks_info.TankNr;
                            tanks_info_hist.ProductNr = tanks_info.ProductNr;
                            tanks_info_hist.ProductId = tanks_info.ProductId;
                            tanks_info_hist.DatelastMeasure = tanks_info.DatelastMeasure;
                            tanks_info_hist.TimeLastMeasure = tanks_info.TimeLastMeasure;
                            tanks_info_hist.Alarms = tanks_info.Alarms;
                            tanks_info_hist.FuelHeight = tanks_info.FuelHeight;
                            tanks_info_hist.FuelTemp = tanks_info.FuelTemp;
                            tanks_info_hist.FuelVolume = tanks_info.FuelVolume;
                            tanks_info_hist.MeasureUnit = tanks_info.MeasureUnit;
                            tanks_info_hist.TemperatureUnit = tanks_info.TemperatureUnit;
                            tanks_info_hist.WaterHeight = tanks_info.WaterHeight;
                            tanks_info_hist.WaterVolume = tanks_info.WaterVolume;
                            tanks_info_hist.DateTimeMeasure = dateTimeMeasure;

                            tanks_infoRepository.InsertHist(tanks_info_hist);

                        }
                    }

                }
                catch (Exception ex)
                {
                    logger.Error(ex, ex.Message);
                }
                finally
                {                    
                    ShiftClose();
                }

                Thread.Sleep(delayWorker);

            }

            e.Cancel = true;
        }

        private async void SendLogToServer()
        {

            LocalDto local = new LocalDto();

            LocalRepository localRepository = new LocalRepository();

            local = localRepository.GetLocal();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://web-activa-service.itbcpwebservices.com");
                //client.BaseAddress = new Uri("https://localhost:44365");
                client.Timeout = TimeSpan.FromMinutes(120.00);

                var json = JsonConvert.SerializeObject(local, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var result = client.PostAsync("/api/services/app/Sagitarius/SaveLogFusion", content).Result;

            }
        }
    }
}