using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BestSellerAutorizaciones
{
    public partial class VAprovación : Form
    {
        Funciones funciones;
        public VAprovación()
        {
            InitializeComponent();
        }
        public VAprovación(Funciones funciones2)
        {
            InitializeComponent();
            this.funciones = funciones2; // Se asigna el valor recibido a la instancia local de Funciones
        }

        private void VAprovación_Load(object sender, EventArgs e)
        {
            funciones.comenVAut = comenAut.Text;

            if (funciones.codUsLogin != funciones.codAutorizador && funciones.codUsLogin != funciones.codAut2 &&
               funciones.codUsLogin != funciones.codAut3)
            {
                ObtenDocAprov(funciones.autEnProceso);

            }
            if (funciones.codUsLogin == funciones.codAutorizador || funciones.codUsLogin == funciones.codAut2)
            {
                ObtenDocAprovSeg(funciones.autNivel1);

            }

            if (funciones.codUsLogin == funciones.codAut3)
            {
                ObtenDocAprovTercer(funciones.autNivel2);
            }

        }

        public void ObtenDocAprov(int estado)
        {
            try
            {
                SqlCommand ObtenerListDoc = new SqlCommand("SELECT PCC.NUMSERIE,PCC.NUMPEDIDO,PCC.TOTBRUTO,PCC.TOTIMPUESTOS," +
                    "PCC.TOTNETO,PCC.CODPROVEEDOR,PCC.FECHACREACION,PROV.NOMPROVEEDOR,PROV.CODCONTABLE,CCL.CODIGO,CCL.TITULO_ESP," +
                    "CCL.AUT_AUTORIZADOR_CC,US.USUARIO,PCAML.AUT_FECHA_SOLICIAUT,PCAML.AUT_RUTA_PDF,PCAML.AUT_COM_SOLICIAUT,US.EMAIL,US.DEPARTAMENTO," +
                    $"US.PASSWORDMAIL,ART.CODARTICULO,ART.DESCRIPCION FROM {funciones.BDGestion}..PEDCOMPRACAB PCC " +
                    $"LEFT JOIN {funciones.BDGestion}..PEDCOMPRACAMPOSLIBRES PCAML  ON (PCC.NUMPEDIDO=PCAML.NUMPEDIDO) " +
                    $"LEFT JOIN {funciones.BDGestion}..PROVEEDORES PROV ON (PCC.CODPROVEEDOR = PROV.CODPROVEEDOR) " +
                    $"LEFT JOIN {funciones.BDGestion}..PEDCOMPRALIN PCL ON(PCC.NUMPEDIDO = PCL.NUMPEDIDO ) " +
                    $"LEFT JOIN {funciones.BDGestion}..ARTICULOS ART ON (PCL.CODARTICULO = ART.CODARTICULO) " +
                    $"LEFT JOIN {funciones.BDContabilidad}..CUENTASCAMPOSLIBRES CCL ON ( ART.CONTRAPARTIDACOMPRA COLLATE Latin1_General_CS_AI = CCL.CODIGO) " +
                    $"LEFT JOIN {funciones.BDGeneral}..USUARIOS US ON (CCL.AUT_AUTORIZADOR_CC = US.CODUSUARIO) " +
                    "WHERE CCL.AUT_AUTORIZADOR_CC= @codUs and PCC.NUMPEDIDO= @NumPed;", funciones.CnnxICGMx);
                ObtenerListDoc.Parameters.AddWithValue("@codUs", funciones.codUsLogin);
                ObtenerListDoc.Parameters.AddWithValue("@NumPed", funciones.numPedSelect);
                ObtenerListDoc.Parameters.AddWithValue("@estado", estado);
                SqlDataReader ReaderObtenerListDoc = ObtenerListDoc.ExecuteReader();
                while (ReaderObtenerListDoc.Read())
                {
                    //Aut1
                    txtcomAut1.Visible = false;
                    label7.Visible = false;
                    //Aut2
                    txtcomAut2.Visible = false;
                    label8.Visible = false;

                    dataGridViewArt.Rows.Add(ReaderObtenerListDoc["CODIGO"].ToString(),
                        ReaderObtenerListDoc["DESCRIPCION"].ToString());
                    fechCreaDoc.Text = ReaderObtenerListDoc["FECHACREACION"].ToString();
                    fechSolAutDoc.Text = ReaderObtenerListDoc["AUT_FECHA_SOLICIAUT"].ToString();
                    serie.Text = ReaderObtenerListDoc["NUMSERIE"].ToString();
                    numPed.Text = ReaderObtenerListDoc["NUMPEDIDO"].ToString();
                    codCC.Text = ReaderObtenerListDoc["CODIGO"].ToString();
                    nomCC.Text = ReaderObtenerListDoc["TITULO_ESP"].ToString();

                    decimal valorBruto;
                    decimal valorImpuestos;
                    decimal valorNeto;

                    if (decimal.TryParse(ReaderObtenerListDoc["TOTBRUTO"].ToString(), out valorBruto))
                    {
                        totalB.Text = valorBruto.ToString("0.00");
                    }
                    if (decimal.TryParse(ReaderObtenerListDoc["TOTIMPUESTOS"].ToString(), out valorImpuestos))
                    {
                        totalIm.Text = valorImpuestos.ToString("0.00");
                    }

                    if (decimal.TryParse(ReaderObtenerListDoc["TOTNETO"].ToString(), out valorNeto))
                    {
                        totalN.Text = valorNeto.ToString("0.00");
                    }

                    //totalB.Text = ReaderObtenerListDoc["TOTBRUTO"].ToString();
                    //totalIm.Text = ReaderObtenerListDoc["TOTIMPUESTOS"].ToString();
                    //totalN.Text = ReaderObtenerListDoc["TOTNETO"].ToString();
                    txtcomSolAut.Text = ReaderObtenerListDoc["AUT_COM_SOLICIAUT"].ToString();
                    prov.Text = ReaderObtenerListDoc["NOMPROVEEDOR"].ToString();
                    funciones.nomArchivo = ReaderObtenerListDoc["AUT_RUTA_PDF"].ToString();
                }
                ReaderObtenerListDoc.Close();
                funciones.EscribirLog("info", "Se obtuvo los datos del documento a autorizar de manera correcta", false, 0);
            }
            catch (Exception e)
            {
                funciones.EscribirLog("info", $"No es posible obtener los datos del documento a autorizar a autorizar . ({e.Message})", true, 1);
            }
        }
        public void ObtenDocAprovSeg(int estado)
        {
            try
            {
                SqlCommand ObtenDocAprovAseg = new SqlCommand("SELECT PCC.NUMSERIE,PCC.NUMPEDIDO,PCC.TOTBRUTO,PCC.TOTIMPUESTOS," +
                    "PCC.TOTNETO,PCC.CODPROVEEDOR,PCC.FECHACREACION,PROV.NOMPROVEEDOR,PROV.CODCONTABLE,CCL.CODIGO,CCL.TITULO_ESP," +
                    "CCL.AUT_AUTORIZADOR_CC,US.USUARIO,PCAML.AUT_FECHA_SOLICIAUT,PCAML.AUT_RUTA_PDF,PCAML.AUT_COM_AUT1,PCAML.AUT_COM_SOLICIAUT,US.EMAIL,US.DEPARTAMENTO," +
                    $"US.PASSWORDMAIL,ART.CODARTICULO,ART.DESCRIPCION FROM {funciones.BDGestion}..PEDCOMPRACAB PCC " +
                    $"LEFT JOIN {funciones.BDGestion}..PEDCOMPRACAMPOSLIBRES PCAML  ON (PCC.NUMPEDIDO=PCAML.NUMPEDIDO) " +
                    $"LEFT JOIN {funciones.BDGestion}..PROVEEDORES PROV ON (PCC.CODPROVEEDOR = PROV.CODPROVEEDOR) " +
                    $"LEFT JOIN {funciones.BDGestion}..PEDCOMPRALIN PCL ON(PCC.NUMPEDIDO = PCL.NUMPEDIDO ) " +
                    $"LEFT JOIN {funciones.BDGestion}..ARTICULOS ART ON (PCL.CODARTICULO = ART.CODARTICULO) " +
                    $"LEFT JOIN {funciones.BDContabilidad}..CUENTASCAMPOSLIBRES CCL ON ( ART.CONTRAPARTIDACOMPRA COLLATE Latin1_General_CS_AI = CCL.CODIGO) " +
                    $"LEFT JOIN {funciones.BDGeneral}..USUARIOS US ON (CCL.AUT_AUTORIZADOR_CC = US.CODUSUARIO) " +
                    "WHERE PCC.IDESTADO = @estado and PCC.NUMPEDIDO= @NumPed ", funciones.CnnxICGMx);
                ObtenDocAprovAseg.Parameters.AddWithValue("@NumPed", funciones.numPedSelect);
                ObtenDocAprovAseg.Parameters.AddWithValue("@estado", estado);
                SqlDataReader ReaderObtenerListDoc = ObtenDocAprovAseg.ExecuteReader();
                while (ReaderObtenerListDoc.Read())
                {
                    //************ DISEÑO (para mostrarcomentarios autorizadores)*****************
                    dataGridViewArt.Visible = false;
                    //solicitud
                    label6.Location = new System.Drawing.Point(25, 112);
                    txtcomSolAut.Location = new System.Drawing.Point(25, 132);
                    //Aut1
                    txtcomAut1.Visible = true;
                    label7.Visible = true;
                    label7.Location = new System.Drawing.Point(28, 257);
                    txtcomAut1.Location = new System.Drawing.Point(24, 281);
                    //Aut2
                    txtcomAut2.Visible = false;
                    label8.Visible = false;

                    //dataGridViewArt.Visible = false;
                    //label6.Location = new System.Drawing.Point(25, 112);
                    //txtcomSolAut.Location = new System.Drawing.Point(25, 132);
                    //txtcomAut1.Visible = true;
                    //label7.Visible = true;
                    //label7.Location = new System.Drawing.Point(28, 257);
                    //txtcomAut1.Location = new System.Drawing.Point(24, 281);


                    //*************** DATOS ***************
                    fechCreaDoc.Text = ReaderObtenerListDoc["FECHACREACION"].ToString();
                    fechSolAutDoc.Text = ReaderObtenerListDoc["AUT_FECHA_SOLICIAUT"].ToString();
                    serie.Text = ReaderObtenerListDoc["NUMSERIE"].ToString();
                    numPed.Text = ReaderObtenerListDoc["NUMPEDIDO"].ToString();
                    codCC.Text = ReaderObtenerListDoc["CODIGO"].ToString();
                    nomCC.Text = ReaderObtenerListDoc["TITULO_ESP"].ToString();

                    decimal valorBruto;
                    decimal valorImpuestos;
                    decimal valorNeto;

                    if (decimal.TryParse(ReaderObtenerListDoc["TOTBRUTO"].ToString(), out valorBruto))
                    {
                        totalB.Text = valorBruto.ToString("0.00");
                    }
                    if (decimal.TryParse(ReaderObtenerListDoc["TOTIMPUESTOS"].ToString(), out valorImpuestos))
                    {
                        totalIm.Text = valorImpuestos.ToString("0.00");
                    }

                    if (decimal.TryParse(ReaderObtenerListDoc["TOTNETO"].ToString(), out valorNeto))
                    {
                        totalN.Text = valorNeto.ToString("0.00");
                    }

                    //totalB.Text = ReaderObtenerListDoc["TOTBRUTO"].ToString();
                    //totalIm.Text = ReaderObtenerListDoc["TOTIMPUESTOS"].ToString();
                    //totalN.Text = ReaderObtenerListDoc["TOTNETO"].ToString();
                    txtcomSolAut.Text = ReaderObtenerListDoc["AUT_COM_SOLICIAUT"].ToString();
                    txtcomAut1.Text = ReaderObtenerListDoc["AUT_COM_AUT1"].ToString();
                    prov.Text = ReaderObtenerListDoc["NOMPROVEEDOR"].ToString();
                    funciones.nomArchivo = ReaderObtenerListDoc["AUT_RUTA_PDF"].ToString();
                }
                ReaderObtenerListDoc.Close();
                funciones.EscribirLog("info", "Se obtuvo los datos del documento a autorizar de manera correcta", false, 0);
            }
            catch (Exception e)
            {
                funciones.EscribirLog("info", $"No es posible obtener los datos del documento a autorizar a autorizar . ({e.Message})", true, 1);
            }
        }
        public void ObtenDocAprovTercer(int estado)
        {
            try
            {
                SqlCommand ObtenDocAprovATer = new SqlCommand("SELECT PCC.NUMSERIE,PCC.NUMPEDIDO,PCC.TOTBRUTO,PCC.TOTIMPUESTOS," +
                    "PCC.TOTNETO,PCC.CODPROVEEDOR,PCC.FECHACREACION,PROV.NOMPROVEEDOR,PROV.CODCONTABLE,CCL.CODIGO,CCL.TITULO_ESP," +
                    "CCL.AUT_AUTORIZADOR_CC,US.USUARIO,PCAML.AUT_FECHA_SOLICIAUT,PCAML.AUT_RUTA_PDF,PCAML.AUT_COM_AUT1,PCAML.AUT_COM_AUT2,PCAML.AUT_COM_SOLICIAUT,US.EMAIL,US.DEPARTAMENTO," +
                    $"US.PASSWORDMAIL,ART.CODARTICULO,ART.DESCRIPCION FROM {funciones.BDGestion}..PEDCOMPRACAB PCC " +
                    $"LEFT JOIN {funciones.BDGestion}..PEDCOMPRACAMPOSLIBRES PCAML  ON (PCC.NUMPEDIDO=PCAML.NUMPEDIDO) " +
                    $"LEFT JOIN {funciones.BDGestion}..PROVEEDORES PROV ON (PCC.CODPROVEEDOR = PROV.CODPROVEEDOR) " +
                    $"LEFT JOIN {funciones.BDGestion}..PEDCOMPRALIN PCL ON(PCC.NUMPEDIDO = PCL.NUMPEDIDO ) " +
                    $"LEFT JOIN {funciones.BDGestion}..ARTICULOS ART ON (PCL.CODARTICULO = ART.CODARTICULO) " +
                    $"LEFT JOIN {funciones.BDContabilidad}..CUENTASCAMPOSLIBRES CCL ON ( ART.CONTRAPARTIDACOMPRA COLLATE Latin1_General_CS_AI = CCL.CODIGO) " +
                    $"LEFT JOIN {funciones.BDGeneral}..USUARIOS US ON (CCL.AUT_AUTORIZADOR_CC = US.CODUSUARIO) " +
                    "WHERE PCC.IDESTADO = @estado and PCC.NUMPEDIDO= @NumPed ", funciones.CnnxICGMx);
                ObtenDocAprovATer.Parameters.AddWithValue("@NumPed", funciones.numPedSelect);
                ObtenDocAprovATer.Parameters.AddWithValue("@estado", estado);
                SqlDataReader ReaderObtenerListDoc = ObtenDocAprovATer.ExecuteReader();
                while (ReaderObtenerListDoc.Read())
                {
                    //************ DISEÑO (para mostrarcomentarios autorizadores)*****************
                    dataGridViewArt.Visible = false;
                    //solicitud
                    label6.Location = new System.Drawing.Point(24, 81);
                    txtcomSolAut.Location = new System.Drawing.Point(24, 101);
                    //Aut1
                    txtcomAut1.Visible = true;
                    label7.Visible = true;
                    label7.Location = new System.Drawing.Point(24, 182);
                    txtcomAut1.Location = new System.Drawing.Point(24, 202);
                    //Aut2
                    txtcomAut2.Visible = true;
                    label8.Visible = true;
                    label8.Location = new System.Drawing.Point(24,275);
                    txtcomAut2.Location = new System.Drawing.Point(24, 295);

                    //*************** DATOS ***************
                    fechCreaDoc.Text = ReaderObtenerListDoc["FECHACREACION"].ToString();
                    fechSolAutDoc.Text = ReaderObtenerListDoc["AUT_FECHA_SOLICIAUT"].ToString();
                    serie.Text = ReaderObtenerListDoc["NUMSERIE"].ToString();
                    numPed.Text = ReaderObtenerListDoc["NUMPEDIDO"].ToString();
                    codCC.Text = ReaderObtenerListDoc["CODIGO"].ToString();
                    nomCC.Text = ReaderObtenerListDoc["TITULO_ESP"].ToString();

                    decimal valorBruto;
                    decimal valorImpuestos;
                    decimal valorNeto;

                    if (decimal.TryParse(ReaderObtenerListDoc["TOTBRUTO"].ToString(), out valorBruto))
                    {
                        totalB.Text = valorBruto.ToString("0.00");
                    }
                    if (decimal.TryParse(ReaderObtenerListDoc["TOTIMPUESTOS"].ToString(), out valorImpuestos))
                    {
                        totalIm.Text = valorImpuestos.ToString("0.00");
                    }

                    if (decimal.TryParse(ReaderObtenerListDoc["TOTNETO"].ToString(), out valorNeto))
                    {
                        totalN.Text = valorNeto.ToString("0.00");
                    }

                    txtcomSolAut.Text = ReaderObtenerListDoc["AUT_COM_SOLICIAUT"].ToString();
                    txtcomAut1.Text = ReaderObtenerListDoc["AUT_COM_AUT1"].ToString();
                    txtcomAut2.Text = ReaderObtenerListDoc["AUT_COM_AUT2"].ToString();
                    prov.Text = ReaderObtenerListDoc["NOMPROVEEDOR"].ToString();
                    funciones.nomArchivo = ReaderObtenerListDoc["AUT_RUTA_PDF"].ToString();
                }
                ReaderObtenerListDoc.Close();
                funciones.EscribirLog("info", "Se obtuvo los datos del documento a autorizar de manera correcta", false, 0);
            }
            catch (Exception e)
            {
                funciones.EscribirLog("info", $"No es posible obtener los datos del documento a autorizar a autorizar . ({e.Message})", true, 1);
            }
        }


        public void CancelAutDoc()//Actualizar estado y comentario de rechazo aut CC.
        {
            try
            {
                SqlCommand UpdatEstComAut = new SqlCommand($"UPDATE {funciones.BDGestion}..PEDCOMPRACAMPOSLIBRES SET AUT_COM_AUT1 = @ComenAut1, AUT_AUTORIZADOR1 = @nomUsLog " +
                    $"WHERE NUMPEDIDO = @NumPed ; UPDATE {funciones.BDGestion}..PEDCOMPRACAB SET IDESTADO = @estado " +
                    $"WHERE NUMPEDIDO = @NumPed ", funciones.CnnxICGMx);
                UpdatEstComAut.Parameters.AddWithValue("@nomUsLog", funciones.NameUsuLogin);
                UpdatEstComAut.Parameters.AddWithValue("@NumPed", funciones.numPedSelect);
                UpdatEstComAut.Parameters.AddWithValue("@ComenAut1", comenAut.Text);
                UpdatEstComAut.Parameters.AddWithValue("@estado", funciones.rechazadoNivel1);
                SqlDataReader ReaderUpdatEstComAut = UpdatEstComAut.ExecuteReader();
                while (ReaderUpdatEstComAut.Read())
                {

                }
                ReaderUpdatEstComAut.Close();
                this.Close();
                funciones.EscribirLog("info", $"Para el documento {serie.Text}/{funciones.numPedSelect} a sido rechazada la autorización", true, 2);
            }
            catch (Exception e)
            {
                funciones.EscribirLog("info", $"No es posible actualizar estado y comentario de rechazo de autorización de documento. ({e.Message})", true, 1);
            }
        }
        public void CancelAutDocSeg()//Actualizar estado y comentario de rechazo aut2.
        {
            try
            {
                SqlCommand UpdatEstComAut = new SqlCommand($"UPDATE {funciones.BDGestion}..PEDCOMPRACAMPOSLIBRES SET AUT_COM_AUT2 = @ComenAut2, AUT_AUTORIZADOR1 = @nomUsLog " +
                    $"WHERE NUMPEDIDO = @NumPed; UPDATE {funciones.BDGestion}..PEDCOMPRACAB SET IDESTADO = @estado " +
                    $"WHERE NUMPEDIDO = @NumPed ", funciones.CnnxICGMx);
                UpdatEstComAut.Parameters.AddWithValue("@nomUsLog", funciones.NameUsuLogin);
                UpdatEstComAut.Parameters.AddWithValue("@NumPed", funciones.numPedSelect);
                UpdatEstComAut.Parameters.AddWithValue("@ComenAut2", comenAut.Text);
                UpdatEstComAut.Parameters.AddWithValue("@estado", funciones.rechazadoNivel2);
                SqlDataReader ReaderUpdatEstComAut = UpdatEstComAut.ExecuteReader();
                while (ReaderUpdatEstComAut.Read())
                {

                }
                ReaderUpdatEstComAut.Close();
                this.Close();
                funciones.EscribirLog("info", $"El documento {serie.Text}/{funciones.numPedSelect} a sido rechazada la autorización", true, 2);
            }
            catch (Exception e)
            {
                funciones.EscribirLog("info", $"No es posible actualizar estado y comentario de rechazo de autorización de documento. ({e.Message})", true, 1);
            }
        }
        public void CancelAutDocTercer()//Actualizar estado y comentario de rechazo aut3.
        {
            try
            {
                SqlCommand UpdatEstComAut = new SqlCommand($"UPDATE {funciones.BDGestion}..PEDCOMPRACAMPOSLIBRES SET AUT_COM_AUT3 = @ComenAut3, AUT_AUTORIZADOR3 = @nomUsLog " +
                    $"WHERE NUMPEDIDO = @NumPed; UPDATE {funciones.BDGestion}..PEDCOMPRACAB SET IDESTADO = @estado " +
                    $"WHERE NUMPEDIDO = @NumPed ", funciones.CnnxICGMx);
                UpdatEstComAut.Parameters.AddWithValue("@nomUsLog", funciones.NameUsuLogin);
                UpdatEstComAut.Parameters.AddWithValue("@NumPed", funciones.numPedSelect);
                UpdatEstComAut.Parameters.AddWithValue("@ComenAut3", comenAut.Text);
                UpdatEstComAut.Parameters.AddWithValue("@estado", funciones.rechazadoNivel3);
                SqlDataReader ReaderUpdatEstComAut = UpdatEstComAut.ExecuteReader();
                while (ReaderUpdatEstComAut.Read())
                {

                }
                ReaderUpdatEstComAut.Close();
                this.Close();
                funciones.EscribirLog("info", $"El documento {serie.Text}/{funciones.numPedSelect} a sido rechazada la autorización", true, 2);
            }
            catch (Exception e)
            {
                funciones.EscribirLog("info", $"No es posible actualizar estado y comentario de rechazo de autorización de documento. ({e.Message})", true, 1);
            }
        }


        public void updatEstYCom()//Actualizar estado y comentario
        {
            try
            {
                SqlCommand UpdatEstComAut = new SqlCommand($"UPDATE {funciones.BDGestion}..PEDCOMPRACAMPOSLIBRES " +
                    $"SET AUT_COM_AUT1 = @ComenAut1, AUT_FECHA_AUTORIZA = GETDATE() , AUT_AUTORIZADOR1 = @nomUsLog WHERE NUMPEDIDO= @NumPed;" +
                    $"UPDATE {funciones.BDGestion}..PEDCOMPRACAB SET IDESTADO = @estado WHERE NUMPEDIDO = @NumPed ", funciones.CnnxICGMx);
                UpdatEstComAut.Parameters.AddWithValue("@nomUsLog", funciones.NameUsuLogin);
                UpdatEstComAut.Parameters.AddWithValue("@NumPed", funciones.numPedSelect);
                UpdatEstComAut.Parameters.AddWithValue("@ComenAut1", comenAut.Text);
                UpdatEstComAut.Parameters.AddWithValue("@estado", funciones.autNivel1);
                SqlDataReader ReaderUpdatEstComAut = UpdatEstComAut.ExecuteReader();
                while (ReaderUpdatEstComAut.Read())
                {

                }
                ReaderUpdatEstComAut.Close();
                this.Close();
                funciones.EscribirLog("info", $"El documento {serie.Text}/{funciones.numPedSelect} a sido autorizado de manera correcta ", true, 2);
            }
            catch (Exception e)
            {
                funciones.EscribirLog("info", $"No es posible actualizar Estado y Comentario del documento Autorizado. ({e.Message})", true, 1);
            }
        }
        public void updatEstYComSeg()//Actualizar estado y comentario aut2
        {
            try
            {
                SqlCommand UpdatEstComAutSeg = new SqlCommand($"UPDATE {funciones.BDGestion}..PEDCOMPRACAMPOSLIBRES " +
                    $"SET AUT_COM_AUT2 = @ComenAut2, AUT_FECHA_AUTORIZA = GETDATE() , AUT_AUTORIZADOR2 = @nomUsLog WHERE NUMPEDIDO= @NumPed;" +
                    $"UPDATE {funciones.BDGestion}..PEDCOMPRACAB SET IDESTADO = @estado WHERE NUMPEDIDO = @NumPed ", funciones.CnnxICGMx);
                UpdatEstComAutSeg.Parameters.AddWithValue("@nomUsLog", funciones.NameUsuLogin);
                UpdatEstComAutSeg.Parameters.AddWithValue("@NumPed", funciones.numPedSelect);
                UpdatEstComAutSeg.Parameters.AddWithValue("@ComenAut2", comenAut.Text);
                UpdatEstComAutSeg.Parameters.AddWithValue("@estado", funciones.autNivel2);
                SqlDataReader ReaderUpdatEstComAut = UpdatEstComAutSeg.ExecuteReader();
                while (ReaderUpdatEstComAut.Read())
                {

                }
                ReaderUpdatEstComAut.Close();
                this.Close();
                funciones.EscribirLog("info", $"El documento {serie.Text}/{funciones.numPedSelect} a sido autorizado de manera correcta por el autorizador del segundo nivel", true, 2);
            }
            catch (Exception e)
            {
                funciones.EscribirLog("info", $"No es posible actualizar Estado y Comentario del documento Autorizado. ({e.Message})", true, 1);
            }
        }
        public void updatEstYComTercer()//Actualizar estado y comentario aut2
        {
            try
            {
                SqlCommand UpdatEstComAutSeg = new SqlCommand($"UPDATE {funciones.BDGestion}..PEDCOMPRACAMPOSLIBRES " +
                    $"SET AUT_COM_AUT3 = @ComenAut3, AUT_FECHA_AUTORIZA = GETDATE() , AUT_AUTORIZADOR3 = @nomUsLog " +
                    $"WHERE NUMPEDIDO= @NumPed; UPDATE {funciones.BDGestion}..PEDCOMPRACAB " +
                    $"SET IDESTADO = @estado WHERE NUMPEDIDO = @NumPed", funciones.CnnxICGMx);

                UpdatEstComAutSeg.Parameters.AddWithValue("@nomUsLog", funciones.NameUsuLogin);
                UpdatEstComAutSeg.Parameters.AddWithValue("@NumPed", funciones.numPedSelect);
                UpdatEstComAutSeg.Parameters.AddWithValue("@ComenAut3", comenAut.Text);
                UpdatEstComAutSeg.Parameters.AddWithValue("@estado", funciones.autNivel3);
                SqlDataReader ReaderUpdatEstComAut = UpdatEstComAutSeg.ExecuteReader();
                while (ReaderUpdatEstComAut.Read())
                {

                }
                ReaderUpdatEstComAut.Close();
                this.Close();
                funciones.EscribirLog("info", $"El documento {serie.Text}/{funciones.numPedSelect} a sido autorizado de manera correcta por el autorizador del segundo nivel", true, 2);
            }
            catch (Exception e)
            {
                funciones.EscribirLog("info", $"No es posible actualizar Estado y Comentario del documento Autorizado. ({e.Message})", true, 1);
            }
        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de autorizar este documento?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (funciones.codUsLogin != funciones.codAutorizador && funciones.codUsLogin != funciones.codAut2 &&
               funciones.codUsLogin != funciones.codAut3)
                {
                    updatEstYCom();
                    dtsCorreoDestinoAut2();//Enviar correo a segundos autorizadores
                    funciones.EnviarCorreo(comenAut.Text, funciones.numSerieSelect, funciones.numPedSelect, 1);
                }
                if (funciones.codUsLogin == funciones.codAutorizador || funciones.codUsLogin == funciones.codAut2)
                {
                    updatEstYComSeg();

                    string totalNeto = totalN.Text;
                    Double totNetDo = Double.Parse(totalNeto);
                    string monto = funciones.Monto;
                    Double montoDo = Double.Parse(monto);

                    if (totNetDo > montoDo)
                    {
                        dtsCorreoDestinoAut3();//Enviar correo a tercer autorizador
                        funciones.EnviarCorreo(comenAut.Text, funciones.numSerieSelect, funciones.numPedSelect, 2);
                    }
                    else
                    {
                        //Enviar correo a solicitante
                    }
                    
                }

                if (funciones.codUsLogin == funciones.codAut3)
                {
                    updatEstYComTercer();
                    funciones.EnviarCorreo(comenAut.Text, funciones.numSerieSelect, funciones.numPedSelect, 3);
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Estáseguro de negar la autorización de este documento?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (funciones.codUsLogin != funciones.codAutorizador && funciones.codUsLogin != funciones.codAut2 &&
                funciones.codUsLogin != funciones.codAut3)
                    {
                        CancelAutDoc();
                    }
                if (funciones.codUsLogin == funciones.codAutorizador || funciones.codUsLogin == funciones.codAut2)
                    {
                        CancelAutDocSeg();
                    dtsCorreoDestinoAut1();
                    funciones.EnviarCorreo(txtcomAut2.Text,funciones.numSerieSelect,funciones.numPedSelect,22);
                    }

                if (funciones.codUsLogin == funciones.codAut3)
                    {
                        CancelAutDocTercer();
                    }

                //updatEstYCom();
                //if (funciones.codUsLogin == funciones.codAutorizador)
                //{
                //    CancelAutDocSeg();
                //}
                //else
                //{
                //    if (funciones.codUsLogin != funciones.codAutorizador)
                //    {
                //        CancelAutDoc();
                //        //ENVIAR CORREO NOTIFICACION DE RECHAZO PRIMER NIVEL
                //        //string totalNeto = totalN.Text;
                //        //Double totNetDo = Double.Parse(totalNeto);
                //        //string monto = funciones.Monto;
                //        //Double montoDo = Double.Parse(monto);

                //        //if (totNetDo > montoDo)
                //        //{
                //        //    funciones.EnviarCorreo(comenAut.Text, serie.Text, funciones.numPedSelect, 11);
                //        //}
                //    }
                //}


            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Ruta del archivo que deseas abrir
            funciones.rutaPDF = $@"C:\ICGPlugins\AutorizadorGastos\Facturas Adjuntas\{funciones.nomArchivo}";
            //funciones.rutaPDF = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + $@"\{funciones.nomArchivo}.pdf");

            // Verificar si el archivo existe
            if (System.IO.File.Exists(funciones.rutaPDF))
            {
                // Abrir el archivo con el programa predeterminado
                Process.Start(funciones.rutaPDF);
                funciones.EscribirLog("Info", "Se obtuvo la ruta del documento PDF de manera correcta", false, 0);
            }
            else
            {
                funciones.EscribirLog("Info", "El documento no cuenta con un PDF asociado o el archivo asociado no existe.", true, 1);

            }
        }



        //Dats Correo
        /*public void CodEmpGeneraDoc()
        {
            try
            {
                SqlCommand CodEmpGeneraDoc = new SqlCommand($"SELECT TOP 1 ID, FECHA,HORA,TIPO,CODEMPLEADO,SERIE,NUMERO,NUMEROREGISTRO " +
                    $"FROM {funciones.BDGestion}..REGISTROAUDITORIA " +
                    $"WHERE SERIE = @numSerie AND NUMERO = @numPedi ORDER BY ID DESC", funciones.CnnxICGMx);

                CodEmpGeneraDoc.Parameters.AddWithValue("@numPedi", funciones.numPedido);
                CodEmpGeneraDoc.Parameters.AddWithValue("@numSerie", funciones.seriePedido);

                SqlDataReader ReaderCodEmpGeneraDoc = CodEmpGeneraDoc.ExecuteReader();
                while (ReaderCodEmpGeneraDoc.Read())
                {
                    //Remitente
                    funciones.codUsGeneroDoc = ReaderCodEmpGeneraDoc["CODEMPLEADO"].ToString();
                    funciones.Asunto = "SOLICITUD DE AUTORIZACIÓN PARA EL DOCUMENTO: " + funciones.seriePedido + "/ " + funciones.numPedido;
                    //funciones.codUsGeneroDoc = ReaderObtenerDtsDoc["CODEMPLEADO"].ToString();
                    //funciones.Puerto = int.Parse(ReaderObtenerDtsDoc["PUERTO"].ToString());

                }
                ReaderCodEmpGeneraDoc.Close();
                funciones.EscribirLog("info", "Se obtuvo de manera correctael codigo del empleado que genero el documento", false, 0);
            }
            catch (Exception e)
            {
                funciones.EscribirLog("info", $"No es posible Obtener los datos del empleado que genero el documento. ({e.Message})", true, 1);
            }
        }*/
        
        /*public void dtsCorreoRemitente()
        {
            try
            {
                SqlCommand ObtenerDtsDoc = new SqlCommand($"SELECT CODUSUARIO,USUARIO,EMAIL,CONEXION,SERVIDOR," +
                    $"CC,USUARIOMAIL,PASSWORDMAIL,PUERTO FROM {funciones.BDGeneral}..USUARIOS " +
                    $"WHERE CODUSUARIO = @CodUsLogin", funciones.CnnxICGMx);
                ObtenerDtsDoc.Parameters.AddWithValue("@CodUsLogin", funciones.codUsLogin);

                SqlDataReader ReaderObtenerDtsDoc = ObtenerDtsDoc.ExecuteReader();
                while (ReaderObtenerDtsDoc.Read())
                {
                    //Remitente
                    funciones.Usuario = ReaderObtenerDtsDoc["EMAIL"].ToString();
                    funciones.Pass = ReaderObtenerDtsDoc["PASSWORDMAIL"].ToString();
                    funciones.De = ReaderObtenerDtsDoc["EMAIL"].ToString();
                    funciones.Host = ReaderObtenerDtsDoc["SERVIDOR"].ToString();
                    //funciones.Puerto = int.Parse(ReaderObtenerDtsDoc["PUERTO"].ToString());

                }
                ReaderObtenerDtsDoc.Close();
                funciones.EscribirLog("info", "Se obtuvo de manera orecta los datos para el envio de correo (Remitente)()", false, 0);
            }
            catch (Exception e)
            {
                funciones.EscribirLog("info", $"No es posible Obtener los datos para el envio de correo (Remitente)({e.Message})()", true, 1);
            }
        }*/

        public void dtsCorreoDestinoAut1()
        {
            try
            {
                SqlCommand ObtenerDtsDoc = new SqlCommand($"SELECT CODUSUARIO,USUARIO,EMAIL,CONEXION,SERVIDOR," +
                    $"CC,USUARIOMAIL,PASSWORDMAIL,PUERTO FROM {funciones.BDGeneral}..USUARIOS " +
                    $"WHERE CODUSUARIO = @codAutCC", funciones.CnnxICGMx);
                ObtenerDtsDoc.Parameters.AddWithValue("@codAutCC", funciones.codAutDeCC);

                SqlDataReader ReaderObtenerDtsDoc = ObtenerDtsDoc.ExecuteReader();
                while (ReaderObtenerDtsDoc.Read())
                {
                    //Destinatario
                    funciones.Para = ReaderObtenerDtsDoc["EMAIL"].ToString();
                }
                ReaderObtenerDtsDoc.Close();
                funciones.EscribirLog("info", "Se obtuvo de manera orecta los datos para el envio de correo (Detinatario)", false, 0);
            }
            catch (Exception e)
            {
                funciones.EscribirLog("info", $"No es posible Obtener los datos para el envio de correo.(Detinatario) ({e.Message})", true, 1);
            }
        }

        public void dtsCorreoDestinoAut2() //Enviar correo a segundos autorizadores
        {
            String correo1;
            String correo2;
            try
            {
                SqlCommand ObtenerDtsDoc = new SqlCommand($"SELECT CODUSUARIO,USUARIO,EMAIL,CONEXION,SERVIDOR," +
                    $"CC,USUARIOMAIL,PASSWORDMAIL,PUERTO FROM {funciones.BDGeneral}..USUARIOS " +
                    $"WHERE CODUSUARIO = @codAut OR CODUSUARIO = @codAut2", funciones.CnnxICGMx);
                ObtenerDtsDoc.Parameters.AddWithValue("@codAut", funciones.codAutorizador);
                ObtenerDtsDoc.Parameters.AddWithValue("@codAut2", funciones.codAut2);

                SqlDataReader ReaderObtenerDtsDoc = ObtenerDtsDoc.ExecuteReader();
                while (ReaderObtenerDtsDoc.Read())
                {
                    //Destinatario
                    correo1 = ReaderObtenerDtsDoc["EMAIL"].ToString();
                    correo2 = ReaderObtenerDtsDoc["EMAIL"].ToString();

                    funciones.Para = correo1+"; "+correo2;
                    //MessageBox.Show($"este es el correo{funciones.Para}");

                    funciones.Asunto = "SOLICITUD DE AUTORIZACIÓN PARA EL DOCUMENTO: " + funciones.numSerieSelect + "/ " + funciones.numPedSelect;
                }
                ReaderObtenerDtsDoc.Close();
                funciones.EscribirLog("info", "Se obtuvo de manera orecta los datos para el envio de correo (Detinatario)()", false, 0);
            }
            catch (Exception e)
            {
                funciones.EscribirLog("info", $"No es posible Obtener los datos para el envio de correo.(Detinatario) ({e.Message})()", true, 1);
            }
        }

        public void dtsCorreoDestinoAut3() //Enviar correo a tercer autorizador
        {
            try
            {
                SqlCommand ObtenerDtsDoc = new SqlCommand($"SELECT CODUSUARIO,USUARIO,EMAIL,CONEXION,SERVIDOR," +
                    $"CC,USUARIOMAIL,PASSWORDMAIL,PUERTO FROM {funciones.BDGeneral}..USUARIOS " +
                    $"WHERE CODUSUARIO = @codAut3", funciones.CnnxICGMx);
                ObtenerDtsDoc.Parameters.AddWithValue("@codAut3", funciones.codAut3);
                
                SqlDataReader ReaderObtenerDtsDoc = ObtenerDtsDoc.ExecuteReader();
                while (ReaderObtenerDtsDoc.Read())
                {
                    //Destinatario
                    funciones.Para = ReaderObtenerDtsDoc["EMAIL"].ToString();

                    funciones.Asunto = "SOLICITUD DE AUTORIZACIÓN PARA EL DOCUMENTO: " + funciones.numSerieSelect + "/ " + funciones.numPedSelect;
                }
                ReaderObtenerDtsDoc.Close();
                funciones.EscribirLog("info", "Se obtuvo de manera orecta los datos para el envio de correo (Detinatario)()", false, 0);
            }
            catch (Exception e)
            {
                funciones.EscribirLog("info", $"No es posible Obtener los datos para el envio de correo.(Detinatario) ({e.Message})()", true, 1);
            }
        }


        public void dtsCorreoDestinoSolicito()
        {
            try
            {
                SqlCommand ObtenerDtsDoc = new SqlCommand($"SELECT CODUSUARIO,USUARIO,EMAIL,CONEXION,SERVIDOR," +
                    $"CC,USUARIOMAIL,PASSWORDMAIL,PUERTO FROM {funciones.BDGeneral}..USUARIOS " +
                    $"WHERE CODUSUARIO = @codAut3", funciones.CnnxICGMx);
                ObtenerDtsDoc.Parameters.AddWithValue("@codAut", funciones.codAut3);

                SqlDataReader ReaderObtenerDtsDoc = ObtenerDtsDoc.ExecuteReader();
                while (ReaderObtenerDtsDoc.Read())
                {
                    //Destinatario
                    funciones.Para = ReaderObtenerDtsDoc["EMAIL"].ToString();

                    funciones.Asunto = "SOLICITUD DE AUTORIZACIÓN PARA EL DOCUMENTO: " + funciones.numSerieSelect + "/ " + funciones.numPedSelect;
                }
                ReaderObtenerDtsDoc.Close();
                funciones.EscribirLog("info", "Se obtuvo de manera orecta los datos para el envio de correo (Detinatario)()", false, 0);
            }
            catch (Exception e)
            {
                funciones.EscribirLog("info", $"No es posible Obtener los datos para el envio de correo.(Detinatario) ({e.Message})()", true, 1);
            }
        }

    }
}
