namespace BestSellerAutorizaciones
{
    partial class SolicitudAprobacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SolicitudAprobacion));
            this.label1 = new System.Windows.Forms.Label();
            this.btnEnviarSol = new System.Windows.Forms.Button();
            this.btnCancelarSol = new System.Windows.Forms.Button();
            this.txtBxTotal = new System.Windows.Forms.TextBox();
            this.txtBxCC = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBxComentario = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboBxAut = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnBuscarPDF = new System.Windows.Forms.Button();
            this.txtRutaPDF = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft JhengHei", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(148, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(412, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Solicitud de Aprobación de Gastos";
            // 
            // btnEnviarSol
            // 
            this.btnEnviarSol.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnEnviarSol.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviarSol.Location = new System.Drawing.Point(498, 440);
            this.btnEnviarSol.Name = "btnEnviarSol";
            this.btnEnviarSol.Size = new System.Drawing.Size(164, 33);
            this.btnEnviarSol.TabIndex = 1;
            this.btnEnviarSol.Text = "Enviar Solicitud Aprobación";
            this.btnEnviarSol.UseVisualStyleBackColor = true;
            this.btnEnviarSol.Click += new System.EventHandler(this.btnEnviarSol_Click);
            // 
            // btnCancelarSol
            // 
            this.btnCancelarSol.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancelarSol.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarSol.Location = new System.Drawing.Point(47, 440);
            this.btnCancelarSol.Name = "btnCancelarSol";
            this.btnCancelarSol.Size = new System.Drawing.Size(164, 33);
            this.btnCancelarSol.TabIndex = 2;
            this.btnCancelarSol.Text = "Cacelar Envio de Solicitud";
            this.btnCancelarSol.UseVisualStyleBackColor = true;
            this.btnCancelarSol.Click += new System.EventHandler(this.btnCancelarSol_Click);
            // 
            // txtBxTotal
            // 
            this.txtBxTotal.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtBxTotal.BackColor = System.Drawing.SystemColors.Control;
            this.txtBxTotal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBxTotal.Font = new System.Drawing.Font("Nirmala UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxTotal.ForeColor = System.Drawing.Color.Black;
            this.txtBxTotal.Location = new System.Drawing.Point(116, 196);
            this.txtBxTotal.Name = "txtBxTotal";
            this.txtBxTotal.ReadOnly = true;
            this.txtBxTotal.Size = new System.Drawing.Size(102, 20);
            this.txtBxTotal.TabIndex = 3;
            // 
            // txtBxCC
            // 
            this.txtBxCC.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtBxCC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBxCC.Font = new System.Drawing.Font("Nirmala UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxCC.Location = new System.Drawing.Point(199, 97);
            this.txtBxCC.Name = "txtBxCC";
            this.txtBxCC.ReadOnly = true;
            this.txtBxCC.Size = new System.Drawing.Size(267, 20);
            this.txtBxCC.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(52, 195);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 21);
            this.label2.TabIndex = 6;
            this.label2.Text = "Total: $";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Location = new System.Drawing.Point(52, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 21);
            this.label3.TabIndex = 7;
            this.label3.Text = "Cuenta Contable:";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label4.Location = new System.Drawing.Point(52, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 21);
            this.label4.TabIndex = 8;
            this.label4.Text = "Autorizador:";
            // 
            // txtBxComentario
            // 
            this.txtBxComentario.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtBxComentario.Font = new System.Drawing.Font("Nirmala UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxComentario.Location = new System.Drawing.Point(47, 275);
            this.txtBxComentario.MaxLength = 250;
            this.txtBxComentario.Multiline = true;
            this.txtBxComentario.Name = "txtBxComentario";
            this.txtBxComentario.Size = new System.Drawing.Size(615, 105);
            this.txtBxComentario.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label5.Location = new System.Drawing.Point(43, 251);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 21);
            this.label5.TabIndex = 10;
            this.label5.Text = "Comentarios:";
            // 
            // cboBxAut
            // 
            this.cboBxAut.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboBxAut.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboBxAut.Font = new System.Drawing.Font("Nirmala UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBxAut.FormattingEnabled = true;
            this.cboBxAut.Location = new System.Drawing.Point(164, 146);
            this.cboBxAut.MaxDropDownItems = 3;
            this.cboBxAut.Name = "cboBxAut";
            this.cboBxAut.Size = new System.Drawing.Size(302, 28);
            this.cboBxAut.TabIndex = 11;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Nirmala UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(164, 146);
            this.textBox1.MaxLength = 50;
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(302, 28);
            this.textBox1.TabIndex = 12;
            // 
            // btnBuscarPDF
            // 
            this.btnBuscarPDF.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnBuscarPDF.Font = new System.Drawing.Font("Nirmala UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarPDF.Location = new System.Drawing.Point(379, 198);
            this.btnBuscarPDF.Name = "btnBuscarPDF";
            this.btnBuscarPDF.Size = new System.Drawing.Size(66, 27);
            this.btnBuscarPDF.TabIndex = 13;
            this.btnBuscarPDF.Text = "Buscar";
            this.btnBuscarPDF.UseVisualStyleBackColor = true;
            this.btnBuscarPDF.Click += new System.EventHandler(this.btnBuscarPDF_Click);
            // 
            // txtRutaPDF
            // 
            this.txtRutaPDF.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtRutaPDF.Enabled = false;
            this.txtRutaPDF.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRutaPDF.ForeColor = System.Drawing.Color.Black;
            this.txtRutaPDF.Location = new System.Drawing.Point(452, 198);
            this.txtRutaPDF.Name = "txtRutaPDF";
            this.txtRutaPDF.Size = new System.Drawing.Size(212, 25);
            this.txtRutaPDF.TabIndex = 14;
            this.txtRutaPDF.Text = "Seleccione la ruta del archivo PDF";
            // 
            // SolicitudAprobacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 517);
            this.ControlBox = false;
            this.Controls.Add(this.txtRutaPDF);
            this.Controls.Add(this.btnBuscarPDF);
            this.Controls.Add(this.txtBxComentario);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.cboBxAut);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBxCC);
            this.Controls.Add(this.txtBxTotal);
            this.Controls.Add(this.btnCancelarSol);
            this.Controls.Add(this.btnEnviarSol);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SolicitudAprobacion";
            this.Opacity = 0.96D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Solicitud de Aprovación";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SolicitudAprobacion_FormClosing);
            this.Load += new System.EventHandler(this.SolicitudAprobacion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEnviarSol;
        private System.Windows.Forms.Button btnCancelarSol;
        private System.Windows.Forms.TextBox txtBxTotal;
        private System.Windows.Forms.TextBox txtBxCC;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBxComentario;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboBxAut;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnBuscarPDF;
        private System.Windows.Forms.TextBox txtRutaPDF;
    }
}