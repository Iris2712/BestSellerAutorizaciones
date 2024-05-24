using BestSellerAutorizaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BestSellerAutorizaciones
{
    public partial class Inicial : Form
    {
        private static Funciones funciones = new Funciones();
        Boolean ComponenetesIniciales;
        Boolean VariablesIniciales;
        VConexionBD conexionBD = new VConexionBD();
        public Boolean r;
        

        public Inicial(string param)
        {
            
            InitializeComponent();
            funciones.EscribirLog("INICIO", "Inicio aplicación", false, 0);
            funciones.EscribirLog("",funciones.path,false,0);


            ComponenetesIniciales = funciones.Inicializar();
            if (ComponenetesIniciales == false)
            {
                //ocultar pantalla Form1
                this.Hide();
                VConexionBD FormVConexionBD = new VConexionBD(funciones);   // Se invoca el formulario y se le pasa la instancia de la Funciones
                FormVConexionBD.ShowDialog();
                this.Show();
                //System.Environment.Exit(0);
            }
            VariablesIniciales = funciones.InicializarVar();
            if (VariablesIniciales == true)
            {
                funciones.ConexionBD();
                if (param == "")
                {
                    //sin parametros
                    this.Hide();
                    VLogin vlogin = new VLogin(funciones);
                    vlogin.ShowDialog();
                    this.Show();
                }
                else {
                    //si hay parametro
                    if (param == "Plugin")
                    {
                        if (funciones.UsingXmlReader(funciones.path) == true)
                        {
                            this.Hide();
                            SolicitudAprobacion vsolApr = new SolicitudAprobacion(funciones);
                            vsolApr.ShowDialog();
                            this.ShowDialog();
                        }
                        else
                        {
                            funciones.EscribirLog("info", "Se cerro la aplicacion por que no es un documento de compra", false, 0);
                            System.Environment.Exit(0);
                        }
                    }
                }
            }
            
        }

        private void Form1_Load(object sender, EventArgs e){
            label2.Text = funciones.NameUsuLogin;

            if (funciones.codUsLogin != funciones.codAutorizador && funciones.codUsLogin != funciones.codAut2 &&
                funciones.codUsLogin != funciones.codAut3)
            {
                ObtenListDoc(funciones.autEnProceso);
                segdoAut.Visible = true;
                label8.Visible = true;
                funciones.estadoDocAut = funciones.autEnProceso;
                if (funciones.estadoDocAut == funciones.autEnProceso)
                { label10.Text = "PENDIENTES"; }
            }
            if (funciones.codUsLogin == funciones.codAutorizador || funciones.codUsLogin == funciones.codAut2)
            {
                ObtenListDocSeg(funciones.autNivel1);
                segdoAut.Visible = false;
                label8.Visible = false;
                funciones.estadoDocAut = funciones.autNivel1;
                if (funciones.estadoDocAut == funciones.autNivel1)
                { label10.Text = "PENDIENTES"; }

            }

            if (funciones.codUsLogin == funciones.codAut3)
            {
                ObtenListDocTercer(funciones.autNivel2);
                segdoAut.Visible = false;
                label8.Visible = false;
                funciones.estadoDocAut = funciones.autNivel2;
                if (funciones.estadoDocAut == funciones.autNivel2)
                { label10.Text = "PENDIENTES"; }
            }
        }

        public void ObtenListDoc(int estado)//Obtener listado de documentos AUTORIZADOR POR CUENTA CONTABLE
        {
            try
            {
                SqlCommand ObtenerListDoc = new SqlCommand("SELECT DISTINCT PCC.NUMPEDIDO,PCC.NUMSERIE,PCC.TOTBRUTO,PCC.TOTIMPUESTOS, PCC.IDESTADO," +
                    "PCC.TOTNETO, PCC.CODPROVEEDOR, PCC.FECHACREACION, PROV.NOMPROVEEDOR, PROV.CODCONTABLE, CCL.CODIGO, CCL.TITULO_ESP," +
                    "CCL.AUT_AUTORIZADOR_CC, US.USUARIO, PCAML.AUT_FECHA_SOLICIAUT, PCAML.AUT_COM_SOLICIAUT,PCAML.AUT_FECHA_AUTORIZA, US.EMAIL, US.DEPARTAMENTO," +
                    $"US.PASSWORDMAIL,ART.CODARTICULO, ART.DESCRIPCION FROM {funciones.BDGestion}..PEDCOMPRACAB PCC " +
                    $"LEFT JOIN {funciones.BDGestion}..PEDCOMPRACAMPOSLIBRES PCAML  ON(PCC.NUMPEDIDO = PCAML.NUMPEDIDO) " +
                    $"LEFT JOIN {funciones.BDGestion}..PROVEEDORES PROV ON(PCC.CODPROVEEDOR = PROV.CODPROVEEDOR) " +
                    $"LEFT JOIN {funciones.BDGestion}..PEDCOMPRALIN PCL ON(PCC.NUMPEDIDO = PCL.NUMPEDIDO) " +
                    $"LEFT JOIN {funciones.BDGestion}..ARTICULOS ART ON(PCL.CODARTICULO = ART.CODARTICULO) " +
                    $"LEFT JOIN {funciones.BDContabilidad}..CUENTASCAMPOSLIBRES CCL ON(ART.CONTRAPARTIDACOMPRA COLLATE Latin1_General_CS_AI = CCL.CODIGO) " +
                    $"LEFT JOIN {funciones.BDGeneral}..USUARIOS US ON(CCL.AUT_AUTORIZADOR_CC = US.CODUSUARIO) " +
                    "WHERE AUT_AUTORIZADOR_CC = @codUs and PCC.IDESTADO = @estado", funciones.CnnxICGMx);
                ObtenerListDoc.Parameters.AddWithValue("@codUs", funciones.codUsLogin);
                ObtenerListDoc.Parameters.AddWithValue("@estado", estado);
                SqlDataReader ReaderObteneListDoc = ObtenerListDoc.ExecuteReader();
                while (ReaderObteneListDoc.Read())
                {
                    dataGridView1.Rows.Add(ReaderObteneListDoc["NUMSERIE"].ToString(), 
                        ReaderObteneListDoc["NUMPEDIDO"].ToString(),
                        ReaderObteneListDoc["TOTNETO"].ToString(),
                        ReaderObteneListDoc["NOMPROVEEDOR"].ToString(),
                        ReaderObteneListDoc["CODIGO"].ToString(),
                        ReaderObteneListDoc["TITULO_ESP"].ToString(),
                        ReaderObteneListDoc["AUT_FECHA_SOLICIAUT"].ToString(), 
                        ReaderObteneListDoc["FECHACREACION"].ToString(),
                        ReaderObteneListDoc["AUT_FECHA_AUTORIZA"].ToString(),
                        ReaderObteneListDoc["IDESTADO"].ToString());

                    funciones.totNeto = ReaderObteneListDoc["TOTNETO"].ToString();

                    if (estado == funciones.autNivel1 || estado == funciones.autNivel2)
                    {dataGridView1.Columns["fchAut"].Visible = true;
                    }else
                    {dataGridView1.Columns["fchAut"].Visible = false;}
                    //MessageBox.Show($"Correo:{(ReaderObtenerListDoc["EMAIL"].ToString())}\nPass:{ReaderObtenerListDoc["PASSWORDMAIL"].ToString()}");
                    ////Boton de segundo autorizador visible
                    //if (Convert.ToDouble(ReaderObtenerListDoc["TOTNETO"].ToString()) >= Convert.ToDouble(funciones.Monto) &&
                    //    funciones.codUsLogin == funciones.codAutorizador)
                    //{
                    //    segdoAut.Visible = false;
                    //    label8.Visible = false;
                    //}
                    //else
                    //{
                    //    if (Convert.ToDouble(ReaderObtenerListDoc["TOTNETO"].ToString()) < Convert.ToDouble(funciones.Monto) &&
                    //    funciones.codUsLogin != funciones.codAutorizador)
                    //    {
                    //        segdoAut.Visible = true;
                    //        label8.Visible = true;
                    //    }
                    //}
                }
                ReaderObteneListDoc.Close();
                funciones.EscribirLog("info", "Se obtuvo listado de documentos a autorizar de manera correcta", false, 0);
            }
            catch (Exception e)
            {
                funciones.EscribirLog("info", $"No es posible obtener el listado de documentos a autorizar . ({e.Message})", true, 1);
            }

        }
        public void ObtenListDocSeg(int estado)//Obtener listado de documento 2do Aut
        {
            try
            {
                SqlCommand ObtenerListDocSeg = new SqlCommand("SELECT DISTINCT PCC.NUMPEDIDO,PCC.NUMSERIE,PCC.TOTBRUTO,PCC.TOTIMPUESTOS, PCC.IDESTADO," +
                    "PCC.TOTNETO,PCC.CODPROVEEDOR,PCC.FECHACREACION,PROV.NOMPROVEEDOR,PROV.CODCONTABLE,CCL.CODIGO,CCL.TITULO_ESP," +
                    "CCL.AUT_AUTORIZADOR_CC,US.USUARIO,PCAML.AUT_FECHA_SOLICIAUT,PCAML.AUT_COM_SOLICIAUT,US.EMAIL,US.DEPARTAMENTO," +
                    "US.PASSWORDMAIL,ART.CODARTICULO,ART.DESCRIPCION, PCAML.AUT_FECHA_AUTORIZA " +
                    $"FROM {funciones.BDGestion}..PEDCOMPRACAB PCC " +
                    $"LEFT JOIN {funciones.BDGestion}..PEDCOMPRACAMPOSLIBRES PCAML  ON (PCC.NUMPEDIDO=PCAML.NUMPEDIDO) " +
                    $"LEFT JOIN {funciones.BDGestion}..PROVEEDORES PROV ON (PCC.CODPROVEEDOR = PROV.CODPROVEEDOR) " +
                    $"LEFT JOIN {funciones.BDGestion}..PEDCOMPRALIN PCL ON(PCC.NUMPEDIDO = PCL.NUMPEDIDO ) " +
                    $"LEFT JOIN {funciones.BDGestion}..ARTICULOS ART ON (PCL.CODARTICULO = ART.CODARTICULO) " +
                    $"LEFT JOIN {funciones.BDContabilidad}..CUENTASCAMPOSLIBRES CCL ON ( ART.CONTRAPARTIDACOMPRA COLLATE Latin1_General_CS_AI = CCL.CODIGO) " +
                    $"LEFT JOIN {funciones.BDGeneral}..USUARIOS US ON (CCL.AUT_AUTORIZADOR_CC = US.CODUSUARIO) " +
                    "WHERE  PCC.IDESTADO = @estado", funciones.CnnxICGMx);
                ObtenerListDocSeg.Parameters.AddWithValue("@estado", estado);
                SqlDataReader ReaderObtenerListDocSeg = ObtenerListDocSeg.ExecuteReader();
                while (ReaderObtenerListDocSeg.Read())
                {
                    dataGridView1.Rows.Add(ReaderObtenerListDocSeg["NUMSERIE"].ToString(),
                        ReaderObtenerListDocSeg["NUMPEDIDO"].ToString(),
                        ReaderObtenerListDocSeg["TOTNETO"].ToString(),
                        ReaderObtenerListDocSeg["NOMPROVEEDOR"].ToString(),
                        ReaderObtenerListDocSeg["CODIGO"].ToString(),
                        ReaderObtenerListDocSeg["TITULO_ESP"].ToString(),
                        ReaderObtenerListDocSeg["AUT_FECHA_SOLICIAUT"].ToString(),
                        ReaderObtenerListDocSeg["FECHACREACION"].ToString(),
                        ReaderObtenerListDocSeg["AUT_FECHA_AUTORIZA"].ToString(),
                        ReaderObtenerListDocSeg["IDESTADO"].ToString());

                    if (estado == funciones.autNivel1 || estado == funciones.autNivel2)
                    { dataGridView1.Columns["fchAut"].Visible = true;}
                    else
                    { dataGridView1.Columns["fchAut"].Visible = false; }
                }
                ReaderObtenerListDocSeg.Close();
                funciones.EscribirLog("info", "Se obtuvo listado de documentos a autorizar de manera correcta", false, 0);
            }
            catch (Exception e)
            {
                funciones.EscribirLog("info", $"No es posible obtener el listado de documentos a autorizar para el segundo nivel . ({e.Message})", true, 1);
            }
        }
        public void ObtenListDocTercer(int estado)//Obtener listado de documento AUTORIZADOR POR MONTO
        {
            try
            {
                SqlCommand ObtenerListDocTercer = new SqlCommand("SELECT DISTINCT PCC.NUMPEDIDO,PCC.NUMSERIE,PCC.TOTBRUTO,PCC.TOTIMPUESTOS, PCC.IDESTADO," +
                    "PCC.TOTNETO,PCC.CODPROVEEDOR,PCC.FECHACREACION,PROV.NOMPROVEEDOR,PROV.CODCONTABLE,CCL.CODIGO,CCL.TITULO_ESP," +
                    "CCL.AUT_AUTORIZADOR_CC,US.USUARIO,PCAML.AUT_FECHA_SOLICIAUT,PCAML.AUT_COM_SOLICIAUT,US.EMAIL,US.DEPARTAMENTO," +
                    "US.PASSWORDMAIL,ART.CODARTICULO,ART.DESCRIPCION, PCAML.AUT_FECHA_AUTORIZA " +
                    $"FROM {funciones.BDGestion}..PEDCOMPRACAB PCC " +
                    $"LEFT JOIN {funciones.BDGestion}..PEDCOMPRACAMPOSLIBRES PCAML  ON (PCC.NUMPEDIDO=PCAML.NUMPEDIDO) " +
                    $"LEFT JOIN {funciones.BDGestion}..PROVEEDORES PROV ON (PCC.CODPROVEEDOR = PROV.CODPROVEEDOR) " +
                    $"LEFT JOIN {funciones.BDGestion}..PEDCOMPRALIN PCL ON(PCC.NUMPEDIDO = PCL.NUMPEDIDO ) " +
                    $"LEFT JOIN {funciones.BDGestion}..ARTICULOS ART ON (PCL.CODARTICULO = ART.CODARTICULO) " +
                    $"LEFT JOIN {funciones.BDContabilidad}..CUENTASCAMPOSLIBRES CCL ON ( ART.CONTRAPARTIDACOMPRA COLLATE Latin1_General_CS_AI = CCL.CODIGO) " +
                    $"LEFT JOIN {funciones.BDGeneral}..USUARIOS US ON (CCL.AUT_AUTORIZADOR_CC = US.CODUSUARIO) " +
                    "WHERE  PCC.IDESTADO = @estado and PCC.TOTNETO > @monto", funciones.CnnxICGMx);
                ObtenerListDocTercer.Parameters.AddWithValue("@monto", Convert.ToDouble(funciones.Monto));
                ObtenerListDocTercer.Parameters.AddWithValue("@estado", estado);
                SqlDataReader ReaderObtenerListDocTercer = ObtenerListDocTercer.ExecuteReader();
                while (ReaderObtenerListDocTercer.Read())
                {
                    dataGridView1.Rows.Add(ReaderObtenerListDocTercer["NUMSERIE"].ToString(),
                        ReaderObtenerListDocTercer["NUMPEDIDO"].ToString(),
                        ReaderObtenerListDocTercer["TOTNETO"].ToString(),
                        ReaderObtenerListDocTercer["NOMPROVEEDOR"].ToString(),
                        ReaderObtenerListDocTercer["CODIGO"].ToString(),
                        ReaderObtenerListDocTercer["TITULO_ESP"].ToString(),
                        ReaderObtenerListDocTercer["AUT_FECHA_SOLICIAUT"].ToString(),
                        ReaderObtenerListDocTercer["FECHACREACION"].ToString(),
                        ReaderObtenerListDocTercer["AUT_FECHA_AUTORIZA"].ToString(),
                        ReaderObtenerListDocTercer["IDESTADO"].ToString());

                    if (estado == funciones.autNivel1 || estado == funciones.autNivel2)
                    { dataGridView1.Columns["fchAut"].Visible = true; }
                    else
                    { dataGridView1.Columns["fchAut"].Visible = false; }
                }
                ReaderObtenerListDocTercer.Close();
                funciones.EscribirLog("info", "Se obtuvo listado de documentos a autorizar de manera correcta del tercr nivel", false, 0);
            }
            catch (Exception e)
            {
                funciones.EscribirLog("info", $"No es posible obtener el listado de documentos a autorizar para el segundo nivel . ({e.Message})", true, 1);
            }
        }


        private void salir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Estas seguro de cerrar la aplicación?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                funciones.EscribirLog("info", "Se cerro la aplicación", false, 0);
                //Application.Exit();
                System.Environment.Exit(0);
            }
        }
        private void config_Click(object sender, EventArgs e)
        {
            AccesConfi vpasconf = new AccesConfi(funciones);
            vpasconf.ShowDialog();
        }
        private void refresh_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
        }

        //Filtro estado Doc
        private void segdoAut_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            funciones.estadoDocAut = funciones.autNivel1;
            label10.Text = "REQUIEREN SEGUNDO AUTORIZADOR";
            ObtenListDoc(funciones.autNivel1);

        }
        private void docRecha_Click(object sender, EventArgs e)
        {
            if (funciones.codUsLogin != funciones.codAutorizador && funciones.codUsLogin != funciones.codAut2 &&
              funciones.codUsLogin != funciones.codAut3)
            {
                segdoAut.Visible = true;
                label8.Visible = true;
                dataGridView1.Rows.Clear();
                funciones.estadoDocAut = funciones.rechazadoNivel1;
                label10.Text = "RECHAZADOS";
                ObtenListDoc(funciones.estadoDocAut);
            }
            if (funciones.codUsLogin == funciones.codAutorizador || funciones.codUsLogin == funciones.codAut2)
            {
                segdoAut.Visible = false;
                label8.Visible = false;
                dataGridView1.Rows.Clear();
                funciones.estadoDocAut = funciones.rechazadoNivel2;
                label10.Text = "RECHAZADOS";
                ObtenListDocSeg(funciones.rechazadoNivel2);
            }

            if (funciones.codUsLogin == funciones.codAut3)
            {
                segdoAut.Visible = false;
                label8.Visible = false;
                dataGridView1.Rows.Clear();
                funciones.estadoDocAut = funciones.rechazadoNivel3;
                label10.Text = "RECHAZADOS";
                ObtenListDocSeg(funciones.rechazadoNivel3);
            }
        }
        private void docAut_Click(object sender, EventArgs e)
        {
            if (funciones.codUsLogin != funciones.codAutorizador && funciones.codUsLogin != funciones.codAut2 &&
              funciones.codUsLogin != funciones.codAut3)
            {
                segdoAut.Visible = true;
                label8.Visible = true;
                dataGridView1.Rows.Clear();
                funciones.estadoDocAut = funciones.autNivel1;
                label10.Text = "AUTORIZADOS";
                ObtenListDoc(funciones.estadoDocAut);
            }
            if (funciones.codUsLogin == funciones.codAutorizador || funciones.codUsLogin == funciones.codAut2 )
            {
                segdoAut.Visible = false;
                label8.Visible = false;
                dataGridView1.Rows.Clear();
                funciones.estadoDocAut = funciones.autNivel2;
                label10.Text = "AUTORIZADOS";
                ObtenListDocSeg(funciones.autNivel2);
            }
           
            if (funciones.codUsLogin == funciones.codAut3)
            {
                segdoAut.Visible = false;
                label8.Visible = false;
                dataGridView1.Rows.Clear();
                funciones.estadoDocAut = funciones.autNivel3;
                label10.Text = "AUTORIZADOS";
                ObtenListDocSeg(funciones.autNivel3);
            }
        }
        private void docPenAut_Click(object sender, EventArgs e)
        {
            if (funciones.codUsLogin != funciones.codAutorizador && funciones.codUsLogin != funciones.codAut2 &&
               funciones.codUsLogin != funciones.codAut3)
            {
                segdoAut.Visible = true;
                label8.Visible = true;
                dataGridView1.Rows.Clear();
                funciones.estadoDocAut = funciones.autEnProceso;
                label10.Text = "PENDIENTES";
                ObtenListDoc(funciones.estadoDocAut);
            }
            if (funciones.codUsLogin == funciones.codAutorizador || funciones.codUsLogin == funciones.codAut2)
            {
                segdoAut.Visible = false;
                label8.Visible = false;
                dataGridView1.Rows.Clear();
                funciones.estadoDocAut = funciones.autNivel1;
                label10.Text = "PENDIENTES";
                ObtenListDocSeg(funciones.estadoDocAut);
            }

            if (funciones.codUsLogin == funciones.codAut3)
            {
                segdoAut.Visible = false;
                label8.Visible = false;
                dataGridView1.Rows.Clear();
                funciones.estadoDocAut = funciones.autNivel2;
                label10.Text = "PENDIENTES";
                ObtenListDocTercer(funciones.estadoDocAut);
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)//Seleccion de fila
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                // Obtém o valor da célda selecionada
                object selectedValue = dataGridView1.SelectedCells[0].Value;

                DataGridView dtg = new DataGridView();
                dtg = dataGridView1;
                funciones.numPedSelect = dtg.CurrentRow.Cells[1].Value.ToString();
                funciones.numSerieSelect = dtg.CurrentRow.Cells[2].Value.ToString();

                if (funciones.codUsLogin != funciones.codAutorizador && funciones.codUsLogin != funciones.codAut2 &&
               funciones.codUsLogin != funciones.codAut3)
                {
                    if (funciones.estadoDocAut == funciones.autEnProceso)
                    {
                        VAprovación va = new VAprovación(funciones);
                        va.ShowDialog(); dataGridView1.Refresh();
                    }
                    else
                    {
                        if (funciones.estadoDocAut == funciones.autNivel1)
                        {
                            MessageBox.Show("Este documento ya esta autorizado");
                        }
                    }
                }
                if (funciones.codUsLogin == funciones.codAutorizador || funciones.codUsLogin == funciones.codAut2)
                {
                    if (funciones.estadoDocAut == funciones.autNivel1)
                    {
                        VAprovación va = new VAprovación(funciones);
                        va.ShowDialog(); dataGridView1.Refresh();
                    }
                    else
                    {
                        if (funciones.estadoDocAut == funciones.autNivel2)
                        {
                            MessageBox.Show("Este documento ya esta autorizado");
                        }
                    }
                }
                if (funciones.codUsLogin == funciones.codAut3)
                {
                    if (funciones.estadoDocAut == funciones.autNivel2)
                    {
                        VAprovación va = new VAprovación(funciones);
                        va.ShowDialog(); dataGridView1.Refresh();
                    }
                    else
                    {
                        if (funciones.estadoDocAut == funciones.autNivel3)
                        {
                            MessageBox.Show("Este documento ya esta autorizado");
                        }
                    }
                }


                //if (funciones.codUsLogin == funciones.codAutorizador || funciones.UsuLogin == funciones.codAut2)
                //{
                //    if (funciones.estadoDocAut == funciones.autNivel1)
                //    {
                //        VAprovación va = new VAprovación(funciones);
                //        va.ShowDialog(); dataGridView1.Refresh();
                //    }
                //}
                //else
                //{
                //    if (funciones.codUsLogin != funciones.codAutorizador)
                //    {
                //        if (funciones.estadoDocAut == funciones.autEnProceso)
                //        {
                //            VAprovación va = new VAprovación(funciones);
                //            va.ShowDialog(); dataGridView1.Refresh();
                //        }
                //        else
                //        {
                //            if (funciones.estadoDocAut == funciones.autNivel1)
                //            {
                //                MessageBox.Show("Este documento ya esta autorizado");
                //            }
                //        }
                //    }
                //}
            }
        }

        
        //hover imgs
        private void segdoAut_MouseEnter(object sender, EventArgs e)
        { segdoAut.Size = new Size(segdoAut.Width + 10, segdoAut.Height + 10); }
        private void segdoAut_MouseLeave(object sender, EventArgs e)
        { segdoAut.Size = new Size(segdoAut.Width - 10, segdoAut.Height - 10); }

        private void docRecha_MouseEnter(object sender, EventArgs e)
        { docRecha.Size = new Size(docRecha.Width + 10, docRecha.Height + 10); }
        private void docRecha_MouseLeave(object sender, EventArgs e)
        { docRecha.Size = new Size(docRecha.Width - 10, docRecha.Height - 10); }

        private void docAut_MouseEnter(object sender, EventArgs e)
        { docAut.Size = new Size(docAut.Width + 10, docAut.Height + 10); }
        private void docAut_MouseLeave(object sender, EventArgs e)
        { docAut.Size = new Size(docAut.Width - 10, docAut.Height - 10); }

        private void docPenAut_MouseEnter(object sender, EventArgs e)
        { docPenAut.Size = new Size(docPenAut.Width + 10, docPenAut.Height + 10); }
        private void docPenAut_MouseLeave(object sender, EventArgs e)
        { docPenAut.Size = new Size(docPenAut.Width - 10, docPenAut.Height - 10); }

        private void refresh_MouseEnter(object sender, EventArgs e)
        { refresh.Size = new Size(refresh.Width + 10, refresh.Height + 10); }
        private void refresh_MouseLeave(object sender, EventArgs e)
        { refresh.Size = new Size(refresh.Width - 10, refresh.Height - 10); }

        private void config_MouseEnter(object sender, EventArgs e)
        {config.Size = new Size(config.Width + 10, config.Height + 10); }
        private void config_MouseLeave(object sender, EventArgs e)
        { config.Size = new Size(config.Width - 10, config.Height - 10); }

        private void salir_MouseEnter(object sender, EventArgs e)
        { salir.Size = new Size(salir.Width + 10, salir.Height + 10); }
        private void salir_MouseLeave(object sender, EventArgs e)
        { salir.Size = new Size(salir.Width - 10, salir.Height - 10); }

        private void Inicial_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                funciones.EscribirLog("info", "La aplicacion se cerro desde la barra de tareas de windows(PantallaPrincipal)", false, 0);
                System.Environment.Exit(0);
            }
        }
    }
}
