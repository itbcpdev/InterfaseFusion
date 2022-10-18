using FusionClass;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Configuration;
using System.ComponentModel;
using Application = System.Windows.Forms.Application;

namespace InterfaceFusion
{
    public partial class frmFusion : Form
    {

        IOP_TRANRepository op_tranRepository;
        FusionClass.Fusion cFusion = new FusionClass.Fusion();

        public frmFusion()
        {
            InitializeComponent();
        }

        private void frmFusion_Load(object sender, EventArgs e)
        {
            txtFusionIp.Text = ConfigurationManager.AppSettings["IpFusion"];
            op_tranRepository = new OP_TRANRepository();
            dgvTransactions.DataSource = op_tranRepository.GetAllOP_TRAN();
            cFusion.Connection(txtFusionIp.Text);
            tmrFusionProcesses.Enabled = true;
            tmrFusionProcesses.Interval = Convert.ToInt16(ConfigurationManager.AppSettings["GetSaleInterval"]) * 1000; ; //Intervalo en segundos según configuración
            tmrFusionReconnect.Enabled = false;
            tmrFusionReconnect.Interval = 3600000; //Una hora para reconexión con el controlador Fusion
        }

        private void tmrFusionProcesses_Tick(object sender, EventArgs e)
        {
                                    
            op_tranRepository = new OP_TRANRepository();

            int LastFusionSaleId = 0;
            int LastSigesSaleId = 0;
            int PendingTransactions = 0;
            OP_TRAN op_tran = new OP_TRAN();            

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
                            op_tranRepository.Insert(op_tran);

                            //Marcar transacción

                            bool ClearSale = false;

                            ClearSale = ClearSaleFusion(op_tran.C_INTERNO);

                            if (!ClearSale)
                            {
                                MessageBox.Show("Error al marcar venta");
                            }
                        } 
                    }

                    //Obtener transacciones OP_TRAN

                    dgvTransactions.DataSource = op_tranRepository.GetAllOP_TRAN();

                }

                //Obtener transacciones OP_TRAN

                //dgvTransactions.DataSource = op_tranRepository.GetAllOP_TRAN();

            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ShiftClose();
            }

        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            txtMessage.Text = "Intentando conectarse a Fusion";
            
            try
            {
                cFusion.Connection(txtFusionIp.Text);
                txtMessage.Text = "Conexión establecida";
            }
            catch (Exception)
            {
                txtMessage.Text = "No se pudo establecer conexión";
            }
            
            //cFusion.Close();
        }

        public OP_TRAN FusionSaleById(int id)
        {

            OP_TRAN op_tran = new OP_TRAN();

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

                        op_tran.C_INTERNO = cFusionSale.GetSaleID();
                        op_tran.CONTROLADOR = "01";
                        op_tran.NUMERO = Convert.ToString(cFusionSale.GetSaleID());
                        op_tran.SOLES = Convert.ToDecimal(cFusionSale.GetAmount());

                        switch (gradeName)
                        {
                            case "G84":
                                op_tran.PRODUCTO = "01";
                                break;
                            case "G90":
                                op_tran.PRODUCTO = "02";
                                break;
                            case "G95":
                                op_tran.PRODUCTO = "03";
                                break;
                            case "G97":
                                op_tran.PRODUCTO = "03";
                                break;
                            case "DB5":
                                op_tran.PRODUCTO = "05";
                                break;
                            case "GLP":
                                op_tran.PRODUCTO = "07";
                                break;
                            default:
                                op_tran.PRODUCTO = gradeName;
                                break;
                        }
                        
                        op_tran.PRECIO = Convert.ToDecimal(cFusionSale.GetPPU());
                        op_tran.GALONES = Convert.ToDecimal(cFusionSale.GetVolume());
                        op_tran.CARA = Convert.ToString(cFusionSale.GetPumpNr()).PadLeft(2,'0');
                        op_tran.FECHA = DateTime.Now.Date;
                        op_tran.HORA = DateTime.Now;
                        op_tran.TURNO = "1";
                        op_tran.ESTADO = "";
                        op_tran.DOCUMENTO = null;
                        op_tran.DATEPROCE = null;
                        op_tran.CDTIPODOC = null;
                        op_tran.MANGUERA = cFusionSale.GetHoseNr();
                        op_tran.FECSISTEMA = DateTime.Now;
                        op_tran.VolumenFinal = Convert.ToDecimal(cFusionSale.GetFinalVolume());
                        op_tran.MontoFinal = 0;
                        //op_tran.VolumenFinal = Convert.ToDecimal(totalVolume);
                        //op_tran.MontoFinal = Convert.ToDecimal(totalMoney);
                        op_tran.IdTran = null;

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                //cFusion.Close();
            }

            return op_tran;
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
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);                
                return false;
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
                    MessageBox.Show(ex.Message);
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
                cFusion.Close();
                cFusion.Connection(txtFusionIp.Text);
            }
            catch (Exception ex)
            {
                tmrFusionProcesses.Enabled = true;                
                MessageBox.Show(ex.Message);
                return false;
            }

            tmrFusionProcesses.Enabled = true;

            return true;
        }

        private void tmrFusionReconnect_Tick(object sender, EventArgs e)
        {
            Reconnect();
        }

        private void frmFusion_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                cFusion.Close();                
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.Message);                
            }
        }
    }
}