namespace Sistema_AQLocal
{
    partial class Importar_Stock
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Importar_Stock));
            this.btnmov = new System.Windows.Forms.Button();
            this.btnstock = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblmensaje1 = new System.Windows.Forms.Label();
            this.prg1 = new System.Windows.Forms.ProgressBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.prg2 = new System.Windows.Forms.ProgressBar();
            this.lblmensaje2 = new System.Windows.Forms.Label();
            this.trabajo = new System.ComponentModel.BackgroundWorker();
            this.btnsalir = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.cmbAlmacen = new System.Windows.Forms.ComboBox();
            this.lbl1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnmov
            // 
            this.btnmov.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnmov.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnmov.Image = global::Sistema_AQLocal.Properties.Resources._16__Db_refresh_;
            this.btnmov.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnmov.Location = new System.Drawing.Point(157, 15);
            this.btnmov.Name = "btnmov";
            this.btnmov.Size = new System.Drawing.Size(148, 46);
            this.btnmov.TabIndex = 0;
            this.btnmov.Text = "Importar Movimiento (Bata)";
            this.btnmov.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolTip2.SetToolTip(this.btnmov, "Importar Movimiento");
            this.btnmov.UseVisualStyleBackColor = true;
            this.btnmov.Click += new System.EventHandler(this.btnmov_Click);
            // 
            // btnstock
            // 
            this.btnstock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnstock.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnstock.Image = global::Sistema_AQLocal.Properties.Resources._16__Grid_row_delete_2_;
            this.btnstock.Location = new System.Drawing.Point(157, 14);
            this.btnstock.Name = "btnstock";
            this.btnstock.Size = new System.Drawing.Size(148, 46);
            this.btnstock.TabIndex = 1;
            this.btnstock.Text = "Importar Stock (Bata)";
            this.btnstock.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolTip2.SetToolTip(this.btnstock, "Importar Stock");
            this.btnstock.UseVisualStyleBackColor = true;
            this.btnstock.Click += new System.EventHandler(this.btnstock_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblmensaje1);
            this.groupBox1.Controls.Add(this.prg1);
            this.groupBox1.Controls.Add(this.btnmov);
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(10, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(478, 126);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Movimiento de Almacen";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // lblmensaje1
            // 
            this.lblmensaje1.BackColor = System.Drawing.Color.Coral;
            this.lblmensaje1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblmensaje1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmensaje1.Location = new System.Drawing.Point(18, 98);
            this.lblmensaje1.Name = "lblmensaje1";
            this.lblmensaje1.Size = new System.Drawing.Size(444, 25);
            this.lblmensaje1.TabIndex = 2;
            this.lblmensaje1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // prg1
            // 
            this.prg1.Location = new System.Drawing.Point(18, 69);
            this.prg1.MarqueeAnimationSpeed = 15;
            this.prg1.Name = "prg1";
            this.prg1.Size = new System.Drawing.Size(443, 23);
            this.prg1.Step = 100;
            this.prg1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.prg1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.prg2);
            this.groupBox2.Controls.Add(this.lblmensaje2);
            this.groupBox2.Controls.Add(this.btnstock);
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(12, 186);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(478, 136);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Stock de Almacen";
            // 
            // prg2
            // 
            this.prg2.Location = new System.Drawing.Point(18, 69);
            this.prg2.MarqueeAnimationSpeed = 15;
            this.prg2.Name = "prg2";
            this.prg2.Size = new System.Drawing.Size(443, 23);
            this.prg2.Step = 100;
            this.prg2.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.prg2.TabIndex = 4;
            // 
            // lblmensaje2
            // 
            this.lblmensaje2.BackColor = System.Drawing.Color.Coral;
            this.lblmensaje2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblmensaje2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmensaje2.Location = new System.Drawing.Point(17, 99);
            this.lblmensaje2.Name = "lblmensaje2";
            this.lblmensaje2.Size = new System.Drawing.Size(444, 25);
            this.lblmensaje2.TabIndex = 3;
            this.lblmensaje2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trabajo
            // 
            this.trabajo.DoWork += new System.ComponentModel.DoWorkEventHandler(this.trabajo_DoWork);
            this.trabajo.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.trabajo_ProgressChanged);
            this.trabajo.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.trabajo_RunWorkerCompleted);
            // 
            // btnsalir
            // 
            this.btnsalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnsalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsalir.Image = global::Sistema_AQLocal.Properties.Resources.bt_delete;
            this.btnsalir.Location = new System.Drawing.Point(439, 3);
            this.btnsalir.Name = "btnsalir";
            this.btnsalir.Size = new System.Drawing.Size(49, 43);
            this.btnsalir.TabIndex = 6;
            this.btnsalir.Text = "Salir";
            this.btnsalir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolTip1.SetToolTip(this.btnsalir, "Cerrar el formulario");
            this.btnsalir.UseVisualStyleBackColor = true;
            this.btnsalir.Click += new System.EventHandler(this.btnsalir_Click);
            // 
            // toolTip2
            // 
            this.toolTip2.ToolTipTitle = "Almacen[Bata]";
            // 
            // cmbAlmacen
            // 
            this.cmbAlmacen.FormattingEnabled = true;
            this.cmbAlmacen.Location = new System.Drawing.Point(194, 12);
            this.cmbAlmacen.Name = "cmbAlmacen";
            this.cmbAlmacen.Size = new System.Drawing.Size(239, 21);
            this.cmbAlmacen.TabIndex = 7;
            this.cmbAlmacen.SelectedIndexChanged += new System.EventHandler(this.cmbAlmacen_SelectedIndexChanged);
            this.cmbAlmacen.SelectedValueChanged += new System.EventHandler(this.cmbAlmacen_SelectedValueChanged);
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(74, 19);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(48, 13);
            this.lbl1.TabIndex = 8;
            this.lbl1.Text = "Almacén";
            this.lbl1.Click += new System.EventHandler(this.lbl1_Click);
            // 
            // Importar_Stock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 326);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.cmbAlmacen);
            this.Controls.Add(this.btnsalir);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Importar_Stock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Importar Stock [BATA]";
            this.Load += new System.EventHandler(this.Importar_Stock_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnmov;
        private System.Windows.Forms.Button btnstock;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.ProgressBar prg1;
        internal System.ComponentModel.BackgroundWorker trabajo;
        private System.Windows.Forms.Label lblmensaje1;
        internal System.Windows.Forms.ProgressBar prg2;
        private System.Windows.Forms.Label lblmensaje2;
        private System.Windows.Forms.Button btnsalir;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.ComboBox cmbAlmacen;
        private System.Windows.Forms.Label lbl1;
    }
}