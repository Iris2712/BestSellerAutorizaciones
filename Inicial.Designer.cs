namespace BestSellerAutorizaciones
{
    partial class Inicial
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inicial));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.refresh = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.segdoAut = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.config = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.salir = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.docPenAut = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.docRecha = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.docAut = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Serie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totNeto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prov = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codCC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nomCC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fchSoli = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fchDoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fchAut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label10 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.refresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.segdoAut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.config)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.salir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.docPenAut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.docRecha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.docAut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Nirmala UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(242, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(273, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "Listado de Documentos ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(32, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "label2";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.refresh);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.segdoAut);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.config);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.salir);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.docPenAut);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.docRecha);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.docAut);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(23, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(711, 65);
            this.panel1.TabIndex = 5;
            // 
            // refresh
            // 
            this.refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.refresh.Image = global::BestSellerAutorizaciones.Properties.Resources.boton_actualizar;
            this.refresh.Location = new System.Drawing.Point(558, 5);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(35, 41);
            this.refresh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.refresh.TabIndex = 8;
            this.refresh.TabStop = false;
            this.refresh.Click += new System.EventHandler(this.refresh_Click);
            this.refresh.MouseEnter += new System.EventHandler(this.refresh_MouseEnter);
            this.refresh.MouseLeave += new System.EventHandler(this.refresh_MouseLeave);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(548, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Refrescar";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(462, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Requiere 2° Aut.";
            // 
            // segdoAut
            // 
            this.segdoAut.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.segdoAut.Image = global::BestSellerAutorizaciones.Properties.Resources.documento;
            this.segdoAut.Location = new System.Drawing.Point(481, 4);
            this.segdoAut.Name = "segdoAut";
            this.segdoAut.Size = new System.Drawing.Size(35, 41);
            this.segdoAut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.segdoAut.TabIndex = 13;
            this.segdoAut.TabStop = false;
            this.segdoAut.Click += new System.EventHandler(this.segdoAut_Click);
            this.segdoAut.MouseEnter += new System.EventHandler(this.segdoAut_MouseEnter);
            this.segdoAut.MouseLeave += new System.EventHandler(this.segdoAut_MouseLeave);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(606, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Confi.";
            // 
            // config
            // 
            this.config.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.config.Image = global::BestSellerAutorizaciones.Properties.Resources.configuraciones;
            this.config.Location = new System.Drawing.Point(609, 5);
            this.config.Name = "config";
            this.config.Size = new System.Drawing.Size(35, 41);
            this.config.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.config.TabIndex = 11;
            this.config.TabStop = false;
            this.config.Click += new System.EventHandler(this.config_Click);
            this.config.MouseEnter += new System.EventHandler(this.config_MouseEnter);
            this.config.MouseLeave += new System.EventHandler(this.config_MouseLeave);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(669, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Salir";
            // 
            // salir
            // 
            this.salir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.salir.Image = global::BestSellerAutorizaciones.Properties.Resources.salida;
            this.salir.Location = new System.Drawing.Point(663, 5);
            this.salir.Name = "salir";
            this.salir.Size = new System.Drawing.Size(35, 41);
            this.salir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.salir.TabIndex = 9;
            this.salir.TabStop = false;
            this.salir.Click += new System.EventHandler(this.salir_Click);
            this.salir.MouseEnter += new System.EventHandler(this.salir_MouseEnter);
            this.salir.MouseLeave += new System.EventHandler(this.salir_MouseLeave);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(174, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Doc.Pendientes";
            // 
            // docPenAut
            // 
            this.docPenAut.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.docPenAut.Image = global::BestSellerAutorizaciones.Properties.Resources.AutPendiente;
            this.docPenAut.Location = new System.Drawing.Point(203, 4);
            this.docPenAut.Name = "docPenAut";
            this.docPenAut.Size = new System.Drawing.Size(35, 41);
            this.docPenAut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.docPenAut.TabIndex = 8;
            this.docPenAut.TabStop = false;
            this.docPenAut.Click += new System.EventHandler(this.docPenAut_Click);
            this.docPenAut.MouseEnter += new System.EventHandler(this.docPenAut_MouseEnter);
            this.docPenAut.MouseLeave += new System.EventHandler(this.docPenAut_MouseLeave);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(367, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Doc.Rechazados";
            // 
            // docRecha
            // 
            this.docRecha.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.docRecha.Image = global::BestSellerAutorizaciones.Properties.Resources.cancelado;
            this.docRecha.Location = new System.Drawing.Point(393, 4);
            this.docRecha.Name = "docRecha";
            this.docRecha.Size = new System.Drawing.Size(35, 41);
            this.docRecha.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.docRecha.TabIndex = 7;
            this.docRecha.TabStop = false;
            this.docRecha.Click += new System.EventHandler(this.docRecha_Click);
            this.docRecha.MouseEnter += new System.EventHandler(this.docRecha_MouseEnter);
            this.docRecha.MouseLeave += new System.EventHandler(this.docRecha_MouseLeave);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(267, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Doc.Autorizados";
            // 
            // docAut
            // 
            this.docAut.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.docAut.Image = global::BestSellerAutorizaciones.Properties.Resources.aut;
            this.docAut.Location = new System.Drawing.Point(294, 4);
            this.docAut.Name = "docAut";
            this.docAut.Size = new System.Drawing.Size(35, 41);
            this.docAut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.docAut.TabIndex = 6;
            this.docAut.TabStop = false;
            this.docAut.Click += new System.EventHandler(this.docAut_Click);
            this.docAut.MouseEnter += new System.EventHandler(this.docAut_MouseEnter);
            this.docAut.MouseLeave += new System.EventHandler(this.docAut_MouseLeave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::BestSellerAutorizaciones.Properties.Resources.usuario;
            this.pictureBox1.Location = new System.Drawing.Point(4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(22, 20);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Serie,
            this.numero,
            this.totNeto,
            this.prov,
            this.codCC,
            this.nomCC,
            this.fchSoli,
            this.fchDoc,
            this.fchAut,
            this.status});
            this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridView1.Location = new System.Drawing.Point(27, 149);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(717, 267);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Serie
            // 
            this.Serie.HeaderText = "Serie";
            this.Serie.Name = "Serie";
            this.Serie.ReadOnly = true;
            this.Serie.Width = 80;
            // 
            // numero
            // 
            this.numero.HeaderText = "numero";
            this.numero.Name = "numero";
            this.numero.ReadOnly = true;
            this.numero.Width = 50;
            // 
            // totNeto
            // 
            this.totNeto.HeaderText = "Total";
            this.totNeto.Name = "totNeto";
            this.totNeto.ReadOnly = true;
            this.totNeto.Width = 90;
            // 
            // prov
            // 
            this.prov.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.prov.HeaderText = "Proveedor";
            this.prov.Name = "prov";
            this.prov.ReadOnly = true;
            // 
            // codCC
            // 
            this.codCC.HeaderText = "Cod.CC";
            this.codCC.Name = "codCC";
            this.codCC.ReadOnly = true;
            this.codCC.Width = 152;
            // 
            // nomCC
            // 
            this.nomCC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nomCC.HeaderText = "Nombre Cuenta Contable";
            this.nomCC.Name = "nomCC";
            this.nomCC.ReadOnly = true;
            // 
            // fchSoli
            // 
            this.fchSoli.HeaderText = "Fecha Solicitud";
            this.fchSoli.Name = "fchSoli";
            this.fchSoli.ReadOnly = true;
            // 
            // fchDoc
            // 
            this.fchDoc.HeaderText = "Fecha Emisión";
            this.fchDoc.Name = "fchDoc";
            this.fchDoc.ReadOnly = true;
            // 
            // fchAut
            // 
            this.fchAut.HeaderText = "Fecha Autorización";
            this.fchAut.Name = "fchAut";
            this.fchAut.ReadOnly = true;
            this.fchAut.Visible = false;
            // 
            // status
            // 
            this.status.HeaderText = "Estado";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Visible = false;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Nirmala UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(331, 111);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 20);
            this.label10.TabIndex = 7;
            this.label10.Text = "label10";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Inicial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(756, 457);
            this.ControlBox = false;
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Inicial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Autorizaciones";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Inicial_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.refresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.segdoAut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.config)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.salir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.docPenAut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.docRecha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.docAut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox docAut;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox docRecha;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox salir;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox docPenAut;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox config;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox segdoAut;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Serie;
        private System.Windows.Forms.DataGridViewTextBoxColumn numero;
        private System.Windows.Forms.DataGridViewTextBoxColumn totNeto;
        private System.Windows.Forms.DataGridViewTextBoxColumn prov;
        private System.Windows.Forms.DataGridViewTextBoxColumn codCC;
        private System.Windows.Forms.DataGridViewTextBoxColumn nomCC;
        private System.Windows.Forms.DataGridViewTextBoxColumn fchSoli;
        private System.Windows.Forms.DataGridViewTextBoxColumn fchDoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn fchAut;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox refresh;
        private System.Windows.Forms.Label label9;
    }
}

