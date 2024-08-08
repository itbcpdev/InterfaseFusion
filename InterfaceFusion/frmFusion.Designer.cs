namespace InterfaceFusion
{
    partial class frmFusion
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFusion));
            this.dgvTransactions = new System.Windows.Forms.DataGridView();
            this.numero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soles = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cara = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hora = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateproce = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cdtipodoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.manguera = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecsistema = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.volumenfinal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.montofinal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tmrFusionProcesses = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.txtFusionIp = new System.Windows.Forms.TextBox();
            this.btnConectar = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnRefreshData = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTransactions
            // 
            this.dgvTransactions.AllowUserToAddRows = false;
            this.dgvTransactions.AllowUserToDeleteRows = false;
            this.dgvTransactions.AllowUserToResizeColumns = false;
            this.dgvTransactions.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTransactions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTransactions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.numero,
            this.soles,
            this.producto,
            this.precio,
            this.cantidad,
            this.cara,
            this.hora,
            this.documento,
            this.dateproce,
            this.cdtipodoc,
            this.manguera,
            this.fecsistema,
            this.volumenfinal,
            this.montofinal});
            this.dgvTransactions.Location = new System.Drawing.Point(10, 50);
            this.dgvTransactions.MultiSelect = false;
            this.dgvTransactions.Name = "dgvTransactions";
            this.dgvTransactions.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTransactions.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvTransactions.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvTransactions.RowTemplate.Height = 25;
            this.dgvTransactions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTransactions.Size = new System.Drawing.Size(932, 485);
            this.dgvTransactions.TabIndex = 0;
            // 
            // numero
            // 
            this.numero.DataPropertyName = "NUMERO";
            this.numero.HeaderText = "Número";
            this.numero.Name = "numero";
            this.numero.ReadOnly = true;
            this.numero.Width = 70;
            // 
            // soles
            // 
            this.soles.DataPropertyName = "SOLES";
            this.soles.HeaderText = "Total";
            this.soles.Name = "soles";
            this.soles.ReadOnly = true;
            this.soles.Width = 60;
            // 
            // producto
            // 
            this.producto.DataPropertyName = "PRODUCTO";
            this.producto.HeaderText = "Producto";
            this.producto.Name = "producto";
            this.producto.ReadOnly = true;
            this.producto.Width = 65;
            // 
            // precio
            // 
            this.precio.DataPropertyName = "PRECIO";
            this.precio.HeaderText = "Precio";
            this.precio.Name = "precio";
            this.precio.ReadOnly = true;
            this.precio.Width = 60;
            // 
            // cantidad
            // 
            this.cantidad.DataPropertyName = "GALONES";
            this.cantidad.HeaderText = "Volumen";
            this.cantidad.Name = "cantidad";
            this.cantidad.ReadOnly = true;
            this.cantidad.Width = 65;
            // 
            // cara
            // 
            this.cara.DataPropertyName = "CARA";
            this.cara.HeaderText = "Cara";
            this.cara.Name = "cara";
            this.cara.ReadOnly = true;
            this.cara.Width = 50;
            // 
            // hora
            // 
            this.hora.DataPropertyName = "HORA";
            this.hora.HeaderText = "Fecha";
            this.hora.Name = "hora";
            this.hora.ReadOnly = true;
            // 
            // documento
            // 
            this.documento.DataPropertyName = "DOCUMENTO";
            this.documento.HeaderText = "Documento";
            this.documento.Name = "documento";
            this.documento.ReadOnly = true;
            this.documento.Width = 80;
            // 
            // dateproce
            // 
            this.dateproce.DataPropertyName = "DATEPROCE";
            this.dateproce.HeaderText = "Fecha Proceso";
            this.dateproce.Name = "dateproce";
            this.dateproce.ReadOnly = true;
            this.dateproce.Width = 80;
            // 
            // cdtipodoc
            // 
            this.cdtipodoc.DataPropertyName = "CDTIPODOC";
            this.cdtipodoc.HeaderText = "Tipo Documento";
            this.cdtipodoc.Name = "cdtipodoc";
            this.cdtipodoc.ReadOnly = true;
            this.cdtipodoc.Width = 80;
            // 
            // manguera
            // 
            this.manguera.DataPropertyName = "MANGUERA";
            this.manguera.HeaderText = "Mang.";
            this.manguera.Name = "manguera";
            this.manguera.ReadOnly = true;
            this.manguera.Width = 65;
            // 
            // fecsistema
            // 
            this.fecsistema.DataPropertyName = "FECSISTEMA";
            this.fecsistema.HeaderText = "Fecha Sistema";
            this.fecsistema.Name = "fecsistema";
            this.fecsistema.ReadOnly = true;
            // 
            // volumenfinal
            // 
            this.volumenfinal.DataPropertyName = "VolumenFinal";
            this.volumenfinal.HeaderText = "Volumen Final";
            this.volumenfinal.Name = "volumenfinal";
            this.volumenfinal.ReadOnly = true;
            this.volumenfinal.Width = 80;
            // 
            // montofinal
            // 
            this.montofinal.DataPropertyName = "MontoFinal";
            this.montofinal.HeaderText = "Monto Final";
            this.montofinal.Name = "montofinal";
            this.montofinal.ReadOnly = true;
            this.montofinal.Width = 80;
            // 
            // tmrFusionProcesses
            // 
            this.tmrFusionProcesses.Tick += new System.EventHandler(this.tmrFusionProcesses_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP Fusion";
            // 
            // txtFusionIp
            // 
            this.txtFusionIp.Location = new System.Drawing.Point(71, 15);
            this.txtFusionIp.Name = "txtFusionIp";
            this.txtFusionIp.ReadOnly = true;
            this.txtFusionIp.Size = new System.Drawing.Size(141, 23);
            this.txtFusionIp.TabIndex = 2;
            // 
            // btnConectar
            // 
            this.btnConectar.Location = new System.Drawing.Point(139, 547);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(123, 23);
            this.btnConectar.TabIndex = 3;
            this.btnConectar.Text = "Probar Conexión";
            this.btnConectar.UseVisualStyleBackColor = true;
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(268, 548);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.Size = new System.Drawing.Size(170, 23);
            this.txtMessage.TabIndex = 4;
            // 
            // btnRefreshData
            // 
            this.btnRefreshData.Location = new System.Drawing.Point(10, 547);
            this.btnRefreshData.Name = "btnRefreshData";
            this.btnRefreshData.Size = new System.Drawing.Size(123, 23);
            this.btnRefreshData.TabIndex = 5;
            this.btnRefreshData.Text = "Refrescar Datos";
            this.btnRefreshData.UseVisualStyleBackColor = true;
            this.btnRefreshData.Click += new System.EventHandler(this.btnRefreshData_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // frmFusion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 582);
            this.Controls.Add(this.btnRefreshData);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.btnConectar);
            this.Controls.Add(this.txtFusionIp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvTransactions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmFusion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Interface Wayne Fusion";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmFusion_FormClosing);
            this.Load += new System.EventHandler(this.frmFusion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView dgvTransactions;
        private System.Windows.Forms.Timer tmrFusionProcesses;
        private Label label1;
        private TextBox txtFusionIp;
        private Button btnConectar;
        private TextBox txtMessage;
        private Button btnRefreshData;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private DataGridViewTextBoxColumn numero;
        private DataGridViewTextBoxColumn soles;
        private DataGridViewTextBoxColumn producto;
        private DataGridViewTextBoxColumn precio;
        private DataGridViewTextBoxColumn cantidad;
        private DataGridViewTextBoxColumn cara;
        private DataGridViewTextBoxColumn hora;
        private DataGridViewTextBoxColumn documento;
        private DataGridViewTextBoxColumn dateproce;
        private DataGridViewTextBoxColumn cdtipodoc;
        private DataGridViewTextBoxColumn manguera;
        private DataGridViewTextBoxColumn fecsistema;
        private DataGridViewTextBoxColumn volumenfinal;
        private DataGridViewTextBoxColumn montofinal;
    }
}