namespace Sistema_Aquarella
{
    partial class Anular_Documento
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.txtdocumento = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tab1 = new System.Windows.Forms.TabPage();
            this.dg1 = new System.Windows.Forms.DataGridView();
            this.tabpedido = new System.Windows.Forms.TabControl();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpinicio = new System.Windows.Forms.DateTimePicker();
            this.dtpfinal = new System.Windows.Forms.DateTimePicker();
            this.chkactivar = new System.Windows.Forms.CheckBox();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnconsultar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tipodoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numdoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechadoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.anulado = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Anular = new System.Windows.Forms.DataGridViewImageColumn();
            this.Reimprimir = new System.Windows.Forms.DataGridViewImageColumn();
            this.ven_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.docu_vencido = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.doc_valida = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cod_hash = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tab1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).BeginInit();
            this.tabpedido.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(156, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Buscar por documento";
            // 
            // txtdocumento
            // 
            this.txtdocumento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtdocumento.Location = new System.Drawing.Point(293, 111);
            this.txtdocumento.Name = "txtdocumento";
            this.txtdocumento.Size = new System.Drawing.Size(102, 20);
            this.txtdocumento.TabIndex = 3;
            this.txtdocumento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtdocumento_KeyPress);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipTitle = "Boton de refrescado del panel de liquidaciones";
            // 
            // tab1
            // 
            this.tab1.Controls.Add(this.dg1);
            this.tab1.Location = new System.Drawing.Point(4, 24);
            this.tab1.Name = "tab1";
            this.tab1.Padding = new System.Windows.Forms.Padding(3);
            this.tab1.Size = new System.Drawing.Size(1317, 418);
            this.tab1.TabIndex = 0;
            this.tab1.Text = "Lista de facturas y boletas";
            this.tab1.UseVisualStyleBackColor = true;
            // 
            // dg1
            // 
            this.dg1.AllowUserToAddRows = false;
            this.dg1.AllowUserToDeleteRows = false;
            this.dg1.AllowUserToResizeColumns = false;
            this.dg1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dg1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dg1.BackgroundColor = System.Drawing.Color.White;
            this.dg1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tipodoc,
            this.numdoc,
            this.fechadoc,
            this.Cliente,
            this.total,
            this.anulado,
            this.Anular,
            this.Reimprimir,
            this.ven_id,
            this.docu_vencido,
            this.doc_valida,
            this.cod_hash});
            this.dg1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dg1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg1.Location = new System.Drawing.Point(3, 3);
            this.dg1.Name = "dg1";
            this.dg1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg1.Size = new System.Drawing.Size(1311, 412);
            this.dg1.TabIndex = 1;
            this.dg1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg1_CellContentClick);
            // 
            // tabpedido
            // 
            this.tabpedido.Controls.Add(this.tab1);
            this.tabpedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabpedido.Location = new System.Drawing.Point(2, 135);
            this.tabpedido.Name = "tabpedido";
            this.tabpedido.SelectedIndex = 0;
            this.tabpedido.Size = new System.Drawing.Size(1325, 446);
            this.tabpedido.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(79, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Fecha Inicio";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(79, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Fecha Final";
            // 
            // dtpinicio
            // 
            this.dtpinicio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpinicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpinicio.Location = new System.Drawing.Point(158, 17);
            this.dtpinicio.Name = "dtpinicio";
            this.dtpinicio.Size = new System.Drawing.Size(119, 20);
            this.dtpinicio.TabIndex = 7;
            // 
            // dtpfinal
            // 
            this.dtpfinal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpfinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpfinal.Location = new System.Drawing.Point(159, 49);
            this.dtpfinal.Name = "dtpfinal";
            this.dtpfinal.Size = new System.Drawing.Size(119, 20);
            this.dtpfinal.TabIndex = 8;
            // 
            // chkactivar
            // 
            this.chkactivar.AutoSize = true;
            this.chkactivar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkactivar.Location = new System.Drawing.Point(159, 86);
            this.chkactivar.Name = "chkactivar";
            this.chkactivar.Size = new System.Drawing.Size(229, 17);
            this.chkactivar.TabIndex = 9;
            this.chkactivar.Text = "Activar consulta por numero de documento";
            this.chkactivar.UseVisualStyleBackColor = true;
            this.chkactivar.CheckedChanged += new System.EventHandler(this.chkactivar_CheckedChanged);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.Description = "Configuracion de Guia y Transportadora";
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Image = global::Sistema_Aquarella.Properties.Resources.bt_destiny;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ToolTipText = "Configuracion de Guia y Transportadora";
            this.dataGridViewImageColumn1.Width = 5;
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.HeaderText = "Inicio";
            this.dataGridViewImageColumn2.Image = global::Sistema_Aquarella.Properties.Resources.bt_pack_order;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.Width = 38;
            // 
            // btnconsultar
            // 
            this.btnconsultar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnconsultar.Image = global::Sistema_Aquarella.Properties.Resources.lupa;
            this.btnconsultar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnconsultar.Location = new System.Drawing.Point(293, 35);
            this.btnconsultar.Name = "btnconsultar";
            this.btnconsultar.Size = new System.Drawing.Size(83, 34);
            this.btnconsultar.TabIndex = 10;
            this.btnconsultar.Text = "Consultar";
            this.btnconsultar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnconsultar.UseVisualStyleBackColor = true;
            this.btnconsultar.Click += new System.EventHandler(this.btnconsultar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(757, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(317, 25);
            this.label4.TabIndex = 11;
            this.label4.Text = "Consultar Facturas y Boletas";
            // 
            // tipodoc
            // 
            this.tipodoc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.tipodoc.DataPropertyName = "tipodoc";
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.SkyBlue;
            this.tipodoc.DefaultCellStyle = dataGridViewCellStyle1;
            this.tipodoc.HeaderText = "Tipo";
            this.tipodoc.Name = "tipodoc";
            this.tipodoc.ReadOnly = true;
            this.tipodoc.Width = 80;
            // 
            // numdoc
            // 
            this.numdoc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.numdoc.DataPropertyName = "numdoc";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.numdoc.DefaultCellStyle = dataGridViewCellStyle2;
            this.numdoc.HeaderText = "Numero";
            this.numdoc.Name = "numdoc";
            this.numdoc.ReadOnly = true;
            this.numdoc.Width = 140;
            // 
            // fechadoc
            // 
            this.fechadoc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.fechadoc.DataPropertyName = "fechadoc";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.fechadoc.DefaultCellStyle = dataGridViewCellStyle3;
            this.fechadoc.HeaderText = "Fecha";
            this.fechadoc.Name = "fechadoc";
            this.fechadoc.ReadOnly = true;
            this.fechadoc.Width = 210;
            // 
            // Cliente
            // 
            this.Cliente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Cliente.DataPropertyName = "Cliente";
            this.Cliente.HeaderText = "Cliente";
            this.Cliente.Name = "Cliente";
            this.Cliente.ReadOnly = true;
            this.Cliente.Width = 550;
            // 
            // total
            // 
            this.total.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.total.DataPropertyName = "total";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle4.Format = "C2";
            dataGridViewCellStyle4.NullValue = null;
            this.total.DefaultCellStyle = dataGridViewCellStyle4;
            this.total.HeaderText = "Total";
            this.total.Name = "total";
            this.total.ReadOnly = true;
            // 
            // anulado
            // 
            this.anulado.DataPropertyName = "anulado";
            this.anulado.HeaderText = "Anulado";
            this.anulado.Name = "anulado";
            this.anulado.ReadOnly = true;
            this.anulado.Width = 58;
            // 
            // Anular
            // 
            this.Anular.HeaderText = "Anular";
            this.Anular.Image = global::Sistema_Aquarella.Properties.Resources.bt_delete;
            this.Anular.Name = "Anular";
            this.Anular.ReadOnly = true;
            this.Anular.ToolTipText = "Anular documento";
            this.Anular.Width = 48;
            // 
            // Reimprimir
            // 
            this.Reimprimir.HeaderText = "Reimprimir";
            this.Reimprimir.Image = global::Sistema_Aquarella.Properties.Resources.printer;
            this.Reimprimir.Name = "Reimprimir";
            this.Reimprimir.ToolTipText = "Reimprimir documento";
            this.Reimprimir.Width = 75;
            // 
            // ven_id
            // 
            this.ven_id.DataPropertyName = "ven_id";
            this.ven_id.HeaderText = "ven_id";
            this.ven_id.Name = "ven_id";
            this.ven_id.Visible = false;
            this.ven_id.Width = 68;
            // 
            // docu_vencido
            // 
            this.docu_vencido.DataPropertyName = "docu_vencido";
            this.docu_vencido.HeaderText = "docu_vencido";
            this.docu_vencido.Name = "docu_vencido";
            this.docu_vencido.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.docu_vencido.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.docu_vencido.Visible = false;
            this.docu_vencido.Width = 108;
            // 
            // doc_valida
            // 
            this.doc_valida.DataPropertyName = "doc_valida";
            this.doc_valida.HeaderText = "doc_valida";
            this.doc_valida.Name = "doc_valida";
            this.doc_valida.Visible = false;
            this.doc_valida.Width = 72;
            // 
            // cod_hash
            // 
            this.cod_hash.DataPropertyName = "cod_hash";
            this.cod_hash.HeaderText = "cod_hash";
            this.cod_hash.Name = "cod_hash";
            this.cod_hash.Visible = false;
            this.cod_hash.Width = 86;
            // 
            // Anular_Documento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1328, 581);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnconsultar);
            this.Controls.Add(this.chkactivar);
            this.Controls.Add(this.dtpfinal);
            this.Controls.Add(this.dtpinicio);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtdocumento);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabpedido);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 86);
            this.Name = "Anular_Documento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "PanelPedidos";
            this.Load += new System.EventHandler(this.Anular_Documento_Load);
            this.tab1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).EndInit();
            this.tabpedido.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtdocumento;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.TabPage tab1;
        private System.Windows.Forms.DataGridView dg1;
        private System.Windows.Forms.TabControl tabpedido;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpinicio;
        private System.Windows.Forms.DateTimePicker dtpfinal;
        private System.Windows.Forms.CheckBox chkactivar;
        private System.Windows.Forms.Button btnconsultar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipodoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn numdoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechadoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn total;
        private System.Windows.Forms.DataGridViewCheckBoxColumn anulado;
        private System.Windows.Forms.DataGridViewImageColumn Anular;
        private System.Windows.Forms.DataGridViewImageColumn Reimprimir;
        private System.Windows.Forms.DataGridViewTextBoxColumn ven_id;
        private System.Windows.Forms.DataGridViewCheckBoxColumn docu_vencido;
        private System.Windows.Forms.DataGridViewCheckBoxColumn doc_valida;
        private System.Windows.Forms.DataGridViewTextBoxColumn cod_hash;
    }
}