using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

using System.Configuration;
//using System.Web.Configuration;
using System.Net.Configuration;
using BestSellerAutorizaciones;


namespace BestSellerAutorizaciones
{
    public partial class SolicitudAprobacion : Form
    {
        Funciones funciones;
        public SolicitudAprobacion()
        {
            InitializeComponent();
        }

        public SolicitudAprobacion(Funciones funciones2)
        {
            InitializeComponent();
            this.funciones = funciones2;
            ObtenerDatosDoc();
        }
        private void btnCancelarSol_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Estas seguro de cancelar el envio de Solicitud de Autorización?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
               MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                funciones.EscribirLog("info", "Se canceló el envio de Solicitud de Autorización.(Se cerro la aplicación)", false, 0);
                //Application.Exit();
                System.Environment.Exit(0);
            }
        }

        private void btnEnviarSol_Click(object sender, EventArgs e)
        {
            //Asignar variables
            funciones.comenAut=txtBxComentario.Text;//ComentarioSoliAut
            funciones.autDeCC = cboBxAut.Text;//Nombre autorizador
            if (funciones.autDeCC.Length >= 3)
            {
                funciones.codAutDeCC = funciones.autDeCC.Substring(0, 2);//Cod Autorizador
            }
            //funciones.numPedido: numpedido
            //funciones.codCC:Codigo cuneta
            //funciones.nomCC:Nombre cuenta


            if (String.IsNullOrEmpty(cboBxAut.Text) == true && String.IsNullOrEmpty(textBox1.Text) == true)
            {
                funciones.EscribirLog("", "La cuenta asociada al documento no cuenta con un autorizador asignado.\nPuede seleccionar un autorizador de manera manual o cancelar el proceso\"", true, 2);
                SeleAutorizador();
            }
            if (String.IsNullOrEmpty(cboBxAut.Text) == false && String.IsNullOrEmpty(textBox1.Text) == true)
            {
                if (String.IsNullOrEmpty(txtBxComentario.Text) == true)
                {
                    if (MessageBox.Show($"¿Esta seguro de no enviar un comentario?", "", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (MessageBox.Show($"El usuario: {funciones.autDeCC} sera el autorizador de la cuenta:{funciones.nomCC}",
                            "Asignar Autorizador a Cuenta Contable", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            updateAutCC(funciones.codAutDeCC, funciones.codCC);
                            funciones.EscribirLog("info", "Actualizacion de Autorizador realizada con exito", true, 2);
                           
                            txtBxComentario.Text = "'Sin comentarios desolicitud de autorización.'";
                            funciones.EnviarCorreo(txtBxComentario.Text,funciones.seriePedido,funciones.numPedido,0);
                            updatEstYCom();
                            System.Environment.Exit(0);
                        }
                    }
                }
                else
                {
                    if (MessageBox.Show($"El usuario: {funciones.autDeCC} sera el autorizador de la cuenta:{funciones.nomCC}",
                        "Asignar Autorizador a Cuenta Contable", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        updateAutCC(funciones.codAutDeCC, funciones.codCC);
                        funciones.EscribirLog("info", "Actualizacion de Autorizador realizada con exito", true, 2);

                        funciones.EnviarCorreo(funciones.comenAut,funciones.seriePedido,funciones.numPedido,0);
                        updatEstYCom();
                        System.Environment.Exit(0);
                    }
                }
            }
            else
            {
                if (String.IsNullOrEmpty(cboBxAut.Text) == true && String.IsNullOrEmpty(textBox1.Text) == false)
                {
                    if (String.IsNullOrEmpty(txtBxComentario.Text) == true)
                    {
                        if (MessageBox.Show($"¿Esta seguro de no enviar un comentario?", "", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            txtBxComentario.Text = "Sin comentarios desolicitud de autorización.";
                            funciones.EnviarCorreo(txtBxComentario.Text, funciones.seriePedido, funciones.numPedido, 0);
                            saveRutaPDF();
                            updatEstYCom();
                            System.Environment.Exit(0);
                        }   
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(txtBxComentario.Text) == false)
                        {
                            funciones.EnviarCorreo(funciones.comenAut, funciones.seriePedido, funciones.numPedido, 0);
                            saveRutaPDF();
                            updatEstYCom();
                            System.Environment.Exit(0);
                        }
                    }
                }
            }
        }

        
//M E T O D O S
        public void ObtenerDatosDoc() //Obtener datos del documento
        {
            try
            {
                SqlCommand ObtenerDtsDoc = new SqlCommand($"SELECT PCC.NUMSERIE,PCC.NUMPEDIDO,PCC.TOTNETO,PCC.CODPROVEEDOR," +
                    $"PCL.CODARTICULO,PROV.CODCONTABLE,ART.CONTRAPARTIDACOMPRA,CCL.CODIGO,CCL.TITULO_ESP,CCL.AUT_AUTORIZADOR_CC, US.USUARIO " +
                    $"FROM {funciones.BDGestion}..PEDCOMPRACAB PCC " +
                    $"LEFT JOIN {funciones.BDGestion}..PROVEEDORES PROV ON (PCC.CODPROVEEDOR = PROV.CODPROVEEDOR) " +
                    $"LEFT JOIN {funciones.BDGestion}..PEDCOMPRALIN PCL ON(PCC.NUMPEDIDO = PCL.NUMPEDIDO ) " +
                    $"LEFT JOIN {funciones.BDGestion}..ARTICULOS ART ON (PCL.CODARTICULO = ART.CODARTICULO) " +
                    $"LEFT JOIN {funciones.BDContabilidad}..CUENTASCAMPOSLIBRES CCL ON ( ART.CONTRAPARTIDACOMPRA COLLATE Latin1_General_CS_AI = CCL.CODIGO) " +
                    $"LEFT JOIN {funciones.BDGeneral}..USUARIOS US ON (CCL.AUT_AUTORIZADOR_CC = US.CODUSUARIO)" +
                    $"WHERE PCC.NUMPEDIDO = @numPedi AND PCC.NUMSERIE = @numSerie", funciones.CnnxICGMx);
                ObtenerDtsDoc.Parameters.AddWithValue("@numPedi", funciones.numPedido);
                ObtenerDtsDoc.Parameters.AddWithValue("@numSerie", funciones.seriePedido);
                SqlDataReader ReaderObtenerDtsDoc = ObtenerDtsDoc.ExecuteReader();

                while (ReaderObtenerDtsDoc.Read())
                {

                    //txtBxTotal.Text = ReaderObtenerDtsDoc["TOTNETO"].ToString("0.00");
                    //Convertir a decimal
                    decimal valorDecimal;
                    if (decimal.TryParse(ReaderObtenerDtsDoc["TOTNETO"].ToString(), out valorDecimal))
                    {
                        txtBxTotal.Text = valorDecimal.ToString("0.00");
                    }
                    if (String.IsNullOrEmpty(ReaderObtenerDtsDoc["TITULO_ESP"].ToString()))
                    {
                        txtBxCC.Text = ReaderObtenerDtsDoc["CODIGO"].ToString();
                    }
                    else
                    {
                        txtBxCC.Text = ReaderObtenerDtsDoc["TITULO_ESP"].ToString();
                    }
                    //txtBxCC.Text = ReaderObtenerDtsDoc["TITULO_ESP"].ToString();
                    //txtBxCC.Text = ReaderObtenerDtsDoc["CODIGO"].ToString();
                    //cboBxAut.Items.Add(ReaderObtenerDtsDoc["USUARIO"].ToString());
                    //cboBxAut.SelectedIndex = 0;
                    //cboBxAut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
                    textBox1.Text = ReaderObtenerDtsDoc["USUARIO"].ToString();
                    funciones.codCC = ReaderObtenerDtsDoc["CODIGO"].ToString();
                    funciones.nomCC = ReaderObtenerDtsDoc["TITULO_ESP"].ToString();
                    funciones.codAutDeCC = ReaderObtenerDtsDoc["AUT_AUTORIZADOR_CC"].ToString();

                    string contrapart = ReaderObtenerDtsDoc["CONTRAPARTIDACOMPRA"].ToString();
                    if (String.IsNullOrEmpty(contrapart) == true)
                    {
                        funciones.EscribirLog("info", "El articulo no tienen contrapartida.\nRevice la configuración del Articulo", true, 1);
                        System.Environment.Exit(0);
                    }

                }
                ReaderObtenerDtsDoc.Close();
                funciones.EscribirLog("info", "Obtencion de datos del documento de manera correcta", false, 0);
            }
            catch (Exception e)
            {
                funciones.EscribirLog("info", $"No es posible Obtener los datos del Documento. ({e.Message})", true, 1);
            }
        }
        
        public void SeleAutorizador()//Seleccionar el autorizador de la cuenta
        {
            try
            {
                SqlCommand ObtenerEmpleado = new SqlCommand("SELECT CODUSUARIO,USUARIO,DEPARTAMENTO,CONEXION " +
                    $"FROM {funciones.BDGeneral}..USUARIOS WHERE CONEXION = @GrupEmp", funciones.CnnxICGMx);
                ObtenerEmpleado.Parameters.AddWithValue("@GrupEmp", funciones.palClaveGrupEmp);
                SqlDataReader ReaderObtenerEmpleado = ObtenerEmpleado.ExecuteReader();
                while (ReaderObtenerEmpleado.Read())
                {
                    cboBxAut.BringToFront();
                    cboBxAut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                    funciones.autDeCC = ReaderObtenerEmpleado["USUARIO"].ToString();
                    string[] datos = { ReaderObtenerEmpleado["CODUSUARIO"].ToString() + "  " + funciones.autDeCC };
                    cboBxAut.Items.AddRange(datos);
                }
                ReaderObtenerEmpleado.Close();
                funciones.EscribirLog("info", "Obtencion de empleados para autorizador de manera correcta", false, 0);
            }
            catch (Exception e)
            {
                funciones.EscribirLog("info", $"No es posible obtener los usuarios para autorizador. ({e.Message})", true, 1);
            }
        }

        public void updateAutCC(string codautcc, string codcc)//Actualizar Autorizador de Cuenta
        {
            try
            {
                SqlCommand UpdateAutCC = new SqlCommand($"UPDATE {funciones.BDContabilidad}..CUENTASCAMPOSLIBRES SET AUT_AUTORIZADOR_CC = @CodUs WHERE CODIGO = @CodCC", funciones.CnnxICGMx);
                UpdateAutCC.Parameters.AddWithValue("@CodUs", codautcc);
                UpdateAutCC.Parameters.AddWithValue("@CodCC", codcc);
                SqlDataReader ReaderUpdateAutCC = UpdateAutCC.ExecuteReader();
                while (ReaderUpdateAutCC.Read())
                {

                }
                ReaderUpdateAutCC.Close();
                funciones.EscribirLog("info", $"Actualización de Autorizador en la cuenta contable {funciones.codCC}", false, 0);
            }
            catch (Exception e)
            {
                funciones.EscribirLog("info", $"No es posible actualizar el autorizador de la cuenta. ({e.Message})", true, 1);
            }
        }

        public void updatEstYCom()//Actualizar estado y comentario
        {
            try
            {
                SqlCommand UpdatEstYCom = new SqlCommand($"UPDATE {funciones.BDGestion}..PEDCOMPRACAMPOSLIBRES SET AUT_COM_SOLICIAUT = @comen, " +
                    $"AUT_FECHA_SOLICIAUT=GETDATE() WHERE NUMPEDIDO= @numPedi AND NUMSERIE= @numSerie; UPDATE {funciones.BDGestion}..PEDCOMPRACAB SET IDESTADO = @estaDoc WHERE NUMPEDIDO = @numPedi AND NUMSERIE= @numSerie;", funciones.CnnxICGMx);
                ////UpdatEstYCom.Parameters.AddWithValue('@',)
                UpdatEstYCom.Parameters.AddWithValue("@numPedi", funciones.numPedido);
                UpdatEstYCom.Parameters.AddWithValue("@numSerie", funciones.seriePedido);
                UpdatEstYCom.Parameters.AddWithValue("@comen", funciones.comenAut);
                UpdatEstYCom.Parameters.AddWithValue("@estaDoc", funciones.autEnProceso);
                SqlDataReader ReaderUpdatEstYCom = UpdatEstYCom.ExecuteReader();
                while (ReaderUpdatEstYCom.Read())
                {
                    
                }
                ReaderUpdatEstYCom.Close();
                funciones.EscribirLog("info", $"Actualización de Estado y Comentario de manera correcta ", false, 0);
            }
            catch (Exception e)
            {
                funciones.EscribirLog("info", $"No es posible actualizar Estado y Comentario. ({e.Message})", true, 1);
            }
        }

        //Dats Correo
        public void CodEmpGeneraDoc()//Asunto, para mandarlo con serie y numero 
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
                    funciones.Asunto = "SOLICITUD DE AUTORIZACIÓN PARA EL DOCUMENTO: "+ funciones.seriePedido +"/ "+ funciones.numPedido;
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
        }
        //public void dtsCorreoRemitente()
        //{
        //    try
        //    {
        //        SqlCommand ObtenerDtsDoc = new SqlCommand($"SELECT CODUSUARIO,USUARIO,EMAIL,CONEXION,SERVIDOR," +
        //            $"CC,USUARIOMAIL,PASSWORDMAIL,PUERTO FROM {funciones.BDGeneral}..USUARIOS " +
        //            $"WHERE CODUSUARIO = @usGeneroDoc", funciones.CnnxICGMx);
        //        ObtenerDtsDoc.Parameters.AddWithValue("@usGeneroDoc", funciones.codUsGeneroDoc);

        //        SqlDataReader ReaderObtenerDtsDoc = ObtenerDtsDoc.ExecuteReader();
        //        while (ReaderObtenerDtsDoc.Read())
        //        {
        //            //Remitente
        //            ////funciones.Usuario = ReaderObtenerDtsDoc["EMAIL"].ToString();
        //            ////funciones.Pass = ReaderObtenerDtsDoc["PASSWORDMAIL"].ToString();
        //            ////funciones.De = ReaderObtenerDtsDoc["EMAIL"].ToString();
        //            ////funciones.Host = ReaderObtenerDtsDoc["SERVIDOR"].ToString();
        //            ///funciones.Puerto = int.Parse(ReaderObtenerDtsDoc["PUERTO"].ToString());

        //        }
        //        ReaderObtenerDtsDoc.Close();
        //        funciones.EscribirLog("info", "Se obtuvo de manera orecta los datos para el envio de correo (Remitente)", false, 0);
        //    }
        //    catch (Exception e)
        //    {
        //        funciones.EscribirLog("info", $"No es posible Obtener los datos para el envio de correo (Remitente). ({e.Message})", true, 1);
        //    }
        //}

        public void dtsCorreoDestino()
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

        private void SolicitudAprobacion_Load(object sender, EventArgs e)
        {
            CodEmpGeneraDoc();
            //dtsCorreoRemitente();
            dtsCorreoDestino();
            
            if (String.IsNullOrEmpty(txtBxCC.Text) == true)
            {
                funciones.EscribirLog("Info", "El documento no esta asociado a una cuenta contable", true, 1);
                System.Environment.Exit(0);
            }
            if (String.IsNullOrEmpty(cboBxAut.Text) == true && String.IsNullOrEmpty(textBox1.Text) == true)
            {
                cboBxAut.BringToFront();
                SeleAutorizador();
            }
        }

        //PDF
        private void btnBuscarPDF_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Seleccionar archivo";
            openFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf|Todos los archivos (*.*)|*.*";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string rutaArchivo = openFileDialog.FileName;
                string carpetaDestino = @"C:\ICGPlugins\AutorizadorGastos\Facturas Adjuntas\";
                string rutaDestino = "";
                try
                {
                    txtRutaPDF.Text = rutaArchivo; //Mostrar ruta en un TextBox

                    funciones.nomArchivo = Path.GetFileName(rutaArchivo);  //Obtener solo el nombre del archivo

                    rutaDestino = Path.Combine(carpetaDestino, funciones.nomArchivo);// Combina la ruta de destino con el nombre del archivo
                    
                    File.Move(rutaArchivo, rutaDestino);  // Mueve el archivo a la carpeta de destino
                    
                    funciones.EscribirLog("info", "Archivo movido correctamente", false, 0);
                }
                catch (Exception ex)
                {
                    funciones.EscribirLog("info", $"Error al mover el archivo:  { ex.Message}", false, 0);
                }

                //funciones.EscribirLog("info", $"Ruta archivo:{rutaArchivo}\n " +
                //    $"nombre archivo:{funciones.nomArchivo}\n Ruta destino: {rutaDestino} ", true, 1);
            }
        }

        public void saveRutaPDF()//Guardar ruta de PDF en BD
        {
            try
            {
                SqlCommand saveRutaPDF = new SqlCommand($"UPDATE {funciones.BDGestion}..PEDCOMPRACAMPOSLIBRES " +
                    $"SET AUT_RUTA_PDF =@rutaPDF WHERE NUMSERIE=@numSerie AND NUMPEDIDO= @numPedi;", funciones.CnnxICGMx);
                ////UpdatEstYCom.Parameters.AddWithValue('@',)
                saveRutaPDF.Parameters.AddWithValue("@rutaPDF", funciones.nomArchivo);
                saveRutaPDF.Parameters.AddWithValue("@numPedi", funciones.numPedido);
                saveRutaPDF.Parameters.AddWithValue("@numSerie", funciones.seriePedido);
                SqlDataReader ReadersaveRutaPDF = saveRutaPDF.ExecuteReader();
                while (ReadersaveRutaPDF.Read())
                {

                }
                ReadersaveRutaPDF.Close();
                funciones.EscribirLog("info", $"Se guardo de manera correcta la ruta del documento PDF: {funciones.rutaPDF} ", false, 0);
            }
            catch (Exception e)
            {
                funciones.EscribirLog("info", $"No se a podido guardar la ruta del documento PDF ({e.Message})", true, 1);
            }
        }

        private void SolicitudAprobacion_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                funciones.EscribirLog("info", "La aplicacion se cerro desde la barra de tareas de windows(Solicitud de Aprovación)", false, 0);
                System.Environment.Exit(0);
            }
        }

        //private void button1_Click(object sender, EventArgs e) //Mostrar datos de correo
        //{
        //    MessageBox.Show($"usuario:{funciones.Usuario}\n" +
        //        $"contraseña:{funciones.Pass}\n" +
        //        $"host:{funciones.Host}\n" +
        //        $"puerto:{funciones.Puerto}\n" +
        //        $"destinataario:{funciones.Para}", "datos correo");

        //}

        //***************************

        //private void EnviarCorreoConBarraProgreso()
        //{
        //    // Configura la barra de progreso
        //    progressBar1.Visible = false;
        //    progressBar1.Minimum = 0;
        //    progressBar1.Maximum = 100;
        //    progressBar1.Value = 0;

        //    // Variables para el progreso
        //    int progreso = 0;
        //    int incremento = 15; // Cantidad de progreso que se incrementa en cada iteración

        //    // Temporizador para actualizar la barra de progreso
        //    System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        //    progressBar1.Visible = true;
        //    timer.Interval = 1000; // Intervalo en milisegundos
        //    timer.Tick += (sender, e) =>
        //    {
        //        progreso += incremento;
        //        if (progreso <= progressBar1.Maximum)
        //        {
        //            progressBar1.Value = progreso;
        //        }
        //        else
        //        {
        //            timer.Stop();
        //            progressBar1.Visible=false;
        //            MessageBox.Show("Correo enviado con éxito.");
        //        }
        //    };

        //    // Inicia el temporizador
        //    progressBar1.Visible = true;
        //    timer.Start();

        //    // Realiza el envío del correo (tu lógica de envío aquí)
        //    funciones.EnviarCorreo();


        //    // Detiene el temporizador en caso de que el envío termine antes de alcanzar el progreso máximo
        //    if (progreso < progressBar1.Maximum)
        //    {
        //        timer.Stop();
        //        progressBar1.Visible = false;
        //        MessageBox.Show("Envío de correo completado antes del progreso máximo.");
        //    }
        //}


        //private void button1_Click(object sender, EventArgs e)
        //{
        //    CodEmpGeneraDoc();
        //    dtsCorreoRemitente();
        //    dtsCorreoDestino();
        //    MessageBox.Show($"codus:{funciones.codUsGeneroDoc}\nUsuario:{funciones.Usuario}\nPass:{funciones.Pass}\nDe:{funciones.De}\nHots:{funciones.Host}\nPueto:{funciones.Puerto}\n\nAutorizaCC:{funciones.codAutDeCC}\nPara:{funciones.Para}");
        //    EnviarCorreoConBarraProgreso();

        //}
    }

}

    

