namespace Sistema_Aquarella
{
    partial class ConfiguraGuia
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfiguraGuia));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblcliente = new System.Windows.Forms.Label();
            this.lblliq = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtguia = new System.Windows.Forms.TextBox();
            this.dwtransportador = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdaceptar = new System.Windows.Forms.Button();
            this.cmdcancelar = new System.Windows.Forms.Button();
            this.lblmensaje = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(466, 246);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(458, 220);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Configuracion de guia y transportadora";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblmensaje);
            this.groupBox1.Controls.Add(this.lblcliente);
            this.groupBox1.Controls.Add(this.lblliq);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtguia);
            this.groupBox1.Controls.Add(this.dwtransportador);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(446, 195);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Guia y transportadora";
            // 
            // lblcliente
            // 
            this.lblcliente.AutoSize = true;
            this.lblcliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcliente.Location = new System.Drawing.Point(143, 132);
            this.lblcliente.Name = "lblcliente";
            this.lblcliente.Size = new System.Drawing.Size(0, 15);
            this.lblcliente.TabIndex = 7;
            // 
            // lblliq
            // 
            this.lblliq.AutoSize = true;
            this.lblliq.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblliq.Location = new System.Drawing.Point(143, 100);
            this.lblliq.Name = "lblliq";
            this.lblliq.Size = new System.Drawing.Size(0, 15);
            this.lblliq.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(18, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "Nombre Cliente:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Liquidacion Número:";
            // 
            // txtguia
            // 
            this.txtguia.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtguia.Location = new System.Drawing.Point(204, 66);
            this.txtguia.MaxLength = 10;
            this.txtguia.Name = "txtguia";
            this.txtguia.Size = new System.Drawing.Size(203, 20);
            this.txtguia.TabIndex = 3;
            this.txtguia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtguia_KeyPress);
            // 
            // dwtransportador
            // 
            this.dwtransportador.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dwtransportador.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dwtransportador.FormattingEnabled = true;
            this.dwtransportador.Location = new System.Drawing.Point(204, 29);
            this.dwtransportador.Name = "dwtransportador";
            this.dwtransportador.Size = new System.Drawing.Size(203, 21);
            this.dwtransportador.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "2.  Numero de Guia";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "1.  Seleccione transportadora";
            // 
            // cmdaceptar
            // 
            this.cmdaceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdaceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdaceptar.Location = new System.Drawing.Point(27, 265);
            this.cmdaceptar.Name = "cmdaceptar";
            this.cmdaceptar.Size = new System.Drawing.Size(170, 23);
            this.cmdaceptar.TabIndex = 1;
            this.cmdaceptar.Text = "Aceptar y Guardar Guia";
            this.cmdaceptar.UseVisualStyleBackColor = true;
            this.cmdaceptar.Click += new System.EventHandler(this.cmdaceptar_Click);
            // 
            // cmdcancelar
            // 
            this.cmdcancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdcancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdcancelar.Location = new System.Drawing.Point(293, 265);
            this.cmdcancelar.Name = "cmdcancelar";
            this.cmdcancelar.Size = new System.Drawing.Size(170, 23);
            this.cmdcancelar.TabIndex = 2;
            this.cmdcancelar.Text = "Cancelar y NO Guardar Guia";
            this.cmdcancelar.UseVisualStyleBackColor = true;
            this.cmdcancelar.Click += new System.EventHandler(this.cmdcancelar_Click);
            // 
            // lblmensaje
            // 
            this.lblmensaje.AutoSize = true;
            this.lblmensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmensaje.ForeColor = System.Drawing.Color.Red;
            this.lblmensaje.Location = new System.Drawing.Point(18, 160);
            this.lblmensaje.Name = "lblmensaje";
            this.lblmensaje.Size = new System.Drawing.Size(0, 15);
            this.lblmensaje.TabIndex = 8;
            // 
            // ConfiguraGuia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(487, 296);
            this.Controls.Add(this.cmdcancelar);
            this.Controls.Add(this.cmdaceptar);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfiguraGuia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuracion de Guia y Transporte";
            this.Load += new System.EventHandler(this.ConfiguraGuia_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ConfiguraGuia_KeyDown);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtguia;
        private System.Windows.Forms.ComboBox dwtransportador;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdaceptar;
        private System.Windows.Forms.Button cmdcancelar;
        private System.Windows.Forms.Label lblcliente;
        private System.Windows.Forms.Label lblliq;
        private System.Windows.Forms.Label lblmensaje;
    }
}