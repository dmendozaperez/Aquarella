namespace Sistema_Aquarella
{
    partial class Duplicado_Tk
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Duplicado_Tk));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtcomprobante = new System.Windows.Forms.TextBox();
            this.btnimprimir = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblmensaje = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Duplicado de Comprobante";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Numero Comprobante";
            // 
            // txtcomprobante
            // 
            this.txtcomprobante.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtcomprobante.Location = new System.Drawing.Point(15, 101);
            this.txtcomprobante.Name = "txtcomprobante";
            this.txtcomprobante.Size = new System.Drawing.Size(126, 20);
            this.txtcomprobante.TabIndex = 2;
            // 
            // btnimprimir
            // 
            this.btnimprimir.BackColor = System.Drawing.Color.White;
            this.btnimprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnimprimir.Image = global::Sistema_Aquarella.Properties.Resources.impresora;
            this.btnimprimir.Location = new System.Drawing.Point(184, 60);
            this.btnimprimir.Name = "btnimprimir";
            this.btnimprimir.Size = new System.Drawing.Size(122, 100);
            this.btnimprimir.TabIndex = 3;
            this.btnimprimir.UseVisualStyleBackColor = false;
            this.btnimprimir.Click += new System.EventHandler(this.btnimprimir_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(223, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Imprimir";
            // 
            // lblmensaje
            // 
            this.lblmensaje.AutoSize = true;
            this.lblmensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmensaje.ForeColor = System.Drawing.Color.Maroon;
            this.lblmensaje.Location = new System.Drawing.Point(13, 182);
            this.lblmensaje.Name = "lblmensaje";
            this.lblmensaje.Size = new System.Drawing.Size(0, 15);
            this.lblmensaje.TabIndex = 5;
            // 
            // Duplicado_Tk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(350, 221);
            this.Controls.Add(this.lblmensaje);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnimprimir);
            this.Controls.Add(this.txtcomprobante);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Duplicado_Tk";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Duplicado de Comprobante";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtcomprobante;
        private System.Windows.Forms.Button btnimprimir;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblmensaje;
    }
}