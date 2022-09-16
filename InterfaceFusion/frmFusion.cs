using FusionClass;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Configuration;
using System.ComponentModel;

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
                MessageBox.Show("Last Sale on Fusion: " + Convert.ToString(LastFusionSaleId));

                // Obtener última transacción de Siges

                LastSigesSaleId = op_tranRepository.GetLastOP_TRAN();
                MessageBox.Show("Last Sale on Siges: " + Convert.ToString(LastSigesSaleId));

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

                // Obtener transacciones OP_TRAN

                dgvTransactions.DataSource = op_tranRepository.GetAllOP_TRAN();
              
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
            finally
            { 
                
            }

        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            cFusion.Connection(txtFusionIp.Text);

            tmrFusionProcesses.Interval = 2000; //dos segudos
        }

        public OP_TRAN FusionSaleById(int id)
        {

            OP_TRAN op_tran = new OP_TRAN();

            if (cFusion.ConnectionStatus())
            {
                FusionClass.FusionSale cFusionSale = new FusionClass.FusionSale();

                try
                {
                    cFusion.GetSale(id, cFusionSale);

                    int nro = cFusionSale.GetPumpNr();

                    if (cFusionSale.GetPumpNr() != 0)
                    {
                        op_tran.C_INTERNO = cFusionSale.GetSaleID();
                        op_tran.CONTROLADOR = "01";
                        op_tran.NUMERO = Convert.ToString(cFusionSale.GetSaleID());
                        op_tran.SOLES = Convert.ToDecimal(cFusionSale.GetAmount());
                        op_tran.PRODUCTO = Convert.ToString(cFusionSale.GetGradeNr()); //Obtener el nombre
                        op_tran.PRECIO = Convert.ToDecimal(cFusionSale.GetPPU());
                        op_tran.GALONES = Convert.ToDecimal(cFusionSale.GetVolume());
                        op_tran.CARA = Convert.ToString(cFusionSale.GetPumpNr());
                        op_tran.FECHA = DateTime.Now.Date;
                        op_tran.HORA = DateTime.Now;
                        op_tran.TURNO = "1";
                        op_tran.ESTADO = "";
                        op_tran.DOCUMENTO = null;
                        op_tran.DATEPROCE = null;
                        op_tran.CDTIPODOC = null;
                        op_tran.MANGUERA = cFusionSale.GetHoseNr();
                        op_tran.FECSISTEMA = null;
                        op_tran.VolumenFinal = Convert.ToDecimal(cFusionSale.GetFinalVolume());
                        op_tran.MontoFinal = 0;
                        op_tran.IdTran = null;

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return op_tran;
        }

        public int GetLastSaleFusion()
        {
            cFusion.Connection(txtFusionIp.Text);

            FusionClass.FusionSale cFusionSale = new FusionClass.FusionSale();
            
            int lastSaleId = 0;

            try
            {
                cFusion.GetLastSale(cFusionSale);
                lastSaleId = cFusionSale.GetSaleID();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return lastSaleId;
        }

        public bool ClearSaleFusion(int id)
        {
            cFusion.Connection(txtFusionIp.Text);

            string error = "";
            string errorCode = "";

            try
            {
                cFusion.ClearSale(ref id, "POS", "InvoicedBy=POS", ref error, ref errorCode);
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.Message);
                return false;
            }
           
            return true;
        }

        public bool ShiftClose()
        {
            InterfaceData interfaceData = new InterfaceData();

            interfaceData = op_tranRepository.GetInterfaceData();

            if (interfaceData.Cierre == "1")
            {

                //Cerrar turno en controlador

                cFusion.Connection(txtFusionIp.Text);

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
                    MessageBox.Show(ex.Message);
                    return false;
                }
                finally
                {
                    op_tranRepository.UpdateShift();
                }

            }

            return true;
        }

    }
}