using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace BestSellerAutorizaciones
{
    public class Funciones
    {
        //Variables Generales
        public static string DirectorioInicial = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}";
        public static string LogNombre = $"Log_Autorizaciones_{DateTime.Now.ToString("dd/MM/yyyy").Replace('/', '-')}.txt";
        public static string ArchivoConfig = $"Config.icg";
        //Xml ruta                                                                                                       
        public string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData)+ @"\VirtualStore\Program Files (x86)\ICG\ICGManager\AutGastos.XML");
        
        public SqlConnection CnnxICGMx = null;

        //Variables de VentanaConexionBD
        public string ServidorSQL { get; set; }
        public string UsuarioSQL { get; set; }
        public string PassSQL { get; set; }
        public string BDGeneral { get; set; }

        //Variables de VentanaBD_gestion
        public string BDGestion { get; set; }
        public string BDContabilidad { get; set; }
        public string PassConfig { get; set; }
        public string PassConfigRep { get; set; }

        //Variables de Ventana Parametros
        public string Monto { get; set; }
        public string Autorizador { get; set; }
        public string codAutorizador { get; set; }
        public string palClaveGrupEmp = "GAUT";
        public string Aut2 { get; set; }
        public string codAut2 { get; set; }
        public string Aut3 { get; set; }
        public string codAut3 { get; set; }


        //Variables de Login
        public string NameUsuLogin { get; set; }
        public string codUsLogin { get; set; }
        public string UsuLogin { get; set; }
        public string PassLogin { get; set; }

        //Variables Documento
        public string usGeneroDoc { get; set; }
        public string codUsGeneroDoc { get; set; }
        public string tipoDoc { get; set; } 
        public string numPedido { get; set; }
        public string seriePedido { get; set; }
        public string codCC { get; set; }
        public string nomCC { get; set; }
        public string autDeCC { get; set; }
        public string codAutDeCC { get; set; }
        public string comenAut { get; set; }
        public string estaDoc { get; set; }
        

        //Correo
        public string Usuario { get; set; }
        public string Pass { get; set; }

        public string De { get; set; }
        public string Para { get; set; }
        public string Asunto { get; set; }
        public string Host { get; set; }
        public string Puerto { get; set; }
        //public Boolean Protocolo { get; set; }
      

        //VAutorización
        public string numPedSelect { get; set; }
        public string numSerieSelect { get; set; }
        public int estadoDocAut { get; set; }
        public string comenVAut { get; set; }
        public string rutaPDF { get; set; }

        public string nomArchivo { get; set; }

        //Estatus Documentos
        public string totNeto { get; set; }
        public int autNivel1 = 9;
        public int autNivel2 = 10;
        public int rechazadoNivel1 = 11;
        public int rechazadoNivel2 = 12;
        public int autEnProceso = 13;
        public int autNivel3 = 14;
        public int rechazadoNivel3 = 15;




        //Escribir LOG de la Aplicación.
        public void EscribirLog(string Evento, string Mensaje, Boolean CajaTexto, int TipoCaja)
        {
            try
            {
                String CarpetaDoc = DirectorioInicial + $@"\Facturas Adjuntas";

                if (Directory.Exists(CarpetaDoc) == false)
                {
                    DirectoryInfo doc = Directory.CreateDirectory(CarpetaDoc);
                }

                String CarpetaLog = DirectorioInicial + $@"\Log";

                if (Directory.Exists(CarpetaLog) == false)
                {
                    DirectoryInfo di = Directory.CreateDirectory(CarpetaLog);
                }
                string Fecha = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                using (StreamWriter outputFile = new StreamWriter(DirectorioInicial + $@"\log\{LogNombre}", true, Encoding.Unicode))
                {
                    outputFile.WriteLine($"[{Fecha}]\t[{Evento}]\t{Mensaje}");
                    outputFile.Close();
                }
                if (CajaTexto == true)
                {
                    if (TipoCaja == 1)
                    { MessageBox.Show(Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                    if (TipoCaja == 2)
                    { MessageBox.Show(Mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                    if (TipoCaja == 3)
                    { MessageBox.Show(Mensaje, "Confirmación", MessageBoxButtons.OKCancel, MessageBoxIcon.Question); }

                    if (TipoCaja == 4)
                    { MessageBox.Show(Mensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }

                }
            }
            catch (Exception ex)
            {
                string MensajeEX = string.Empty;
                Mensaje = ex.Message.ToString().Replace("'", "''");
                MessageBox.Show($"Surgió una excepción al intentar escribir en el Log. ({MensajeEX})");
            }

        }

        //inicializar aplicación
        public bool Inicializar()
        {
            try
            {
                //Validar Directorio LOG
                if (File.Exists(DirectorioInicial + $@"\{ArchivoConfig}"))
                {
                    return true;
                }
                else
                {
                    EscribirLog("info", "Falta Archivo de configuración.", true, 1);
                    return false;
                }
            }
            catch (Exception e)
            {
                EscribirLog("info", $"No es posible inicializar la aplicación, faltan componentes. ({e.Message})", true, 1);
                return false;
            }
        }

        //inicializar variables
        public bool InicializarVar()
        {
            try
            {
                //Validar Directorio LOG
                if (File.Exists(DirectorioInicial + $@"\{ArchivoConfig}"))

                {
                    IniFile ArchivoConfig2 = new IniFile(DirectorioInicial + $@"\{ArchivoConfig}");

                    ServidorSQL = ArchivoConfig2.Read("ServidorSQL", "DatoSQL");//SQL
                    UsuarioSQL = ArchivoConfig2.Read("UsuarioSQL", "DatoSQL");
                    PassSQL = ArchivoConfig2.Read("PassSQL", "DatoSQL");
                    BDGeneral = ArchivoConfig2.Read("BDGeneral", "DatoSQL");
                    BDGestion = ArchivoConfig2.Read("BDGestion", "BDGestion");//BD gestion
                    BDContabilidad = ArchivoConfig2.Read("BDContabilidad", "BDGestion");
                    PassConfig = ArchivoConfig2.Read("BDContraseña config.", "BDGestion");
                    Usuario = ArchivoConfig2.Read("Correo general", "SMTP");//Correo
                    Pass = ArchivoConfig2.Read("Contraseña", "SMTP");
                    Host = ArchivoConfig2.Read("Host", "SMTP");
                    Puerto = ArchivoConfig2.Read("Puerto", "SMTP");
                    Monto = ArchivoConfig2.Read("Monto", "Monto");//Autorizadores
                    Autorizador = ArchivoConfig2.Read("Autorizador", "Monto");
                    codAutorizador = ArchivoConfig2.Read("Cod.Autorizador", "Monto");
                    Aut2 = ArchivoConfig2.Read("Autorizador2", "Monto");
                    codAut2 = ArchivoConfig2.Read("Cod.Autorizador2", "Monto");
                    Aut3 = ArchivoConfig2.Read("Autorizador3", "Monto");
                    codAut3 = ArchivoConfig2.Read("Cod.Autorizador3", "Monto");

                    return true;
                }
                else
                {
                    EscribirLog("info", "No se encontro el archivo de configuración", true, 1);
                    return false;
                }
            }
            catch (Exception e)
            {
                EscribirLog("info", $"No es posible inicializar la aplicación. ({e.Message})", true, 1);

                return false;
            }
        }

        //Conexion a la BD SQL
        public bool ConexionBD()
        {
            try
            {
                //Validar Conexion a la BD
                CnnxICGMx = new SqlConnection($"Server={ServidorSQL}; Database={BDGeneral}; " +
                    $"User Id={UsuarioSQL}; Password={PassSQL}; MultipleActiveResultSets=true; Connection Timeout=30");

                /* Se abre la conexión */
                CnnxICGMx.Open();

                if (CnnxICGMx.State == ConnectionState.Open)
                {
                    EscribirLog("info", "Conexion a la BD con Exito.", false, 0);
                    return true;
                }
                else
                {
                    EscribirLog("info", "No se pudo conectar a la BD.", true, 1);
                    return false;
                }
            }
            catch (Exception e)
            {
                EscribirLog("info", $"No es posible conectarse a la Base de Datos. ({e.Message})", true, 1);
                return false;
            }
        }

        //Validacion de campos vacios
        public bool CampVacio(string NomCampo, string SubNomCamp)
        {
            if (!String.IsNullOrEmpty(NomCampo))
            { return false; }
            else
            {
                MessageBox.Show($"El campo {SubNomCamp} no puede estar vacio.", "Campo Vacio",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
        }

        //Leer XML
        public bool UsingXmlReader(string path)
        {
            XDocument nodoRaiz = XDocument.Load(path, LoadOptions.None);
            IEnumerable<XElement> nodo = nodoRaiz.Descendants("doc");

            foreach (XElement ele in nodo)
            {
                //Console.WriteLine("Atributo del nodo oficina: " + ele.Attribute("id").Value);
                //Console.WriteLine("Valor del nodo ubicacion: " + ele.Element("tipodoc").Value);
               // MessageBox.Show($"Tipo documento:{ele.Element("tipodoc").Value} " +
                   // $"\n Serie:{ele.Element("serie").Value}" +
                   // $"\n Numero:{ele.Element("numero")}", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                tipoDoc = ele.Element("tipodoc").Value;
                seriePedido = ele.Element("serie").Value;
                numPedido = ele.Element("numero").Value;
                

                EscribirLog("info",$"XML leido de manera correcta, Tipo Doc: {tipoDoc}, Num pedido:{numPedido}", false, 0);
            }
            if (tipoDoc == "PEDCOMPRA")
            {
                return true;
            }
            else
            {
                return false;
            } 
        }
        
        //Envio de correo
        public  void EnviarCorreo(string comentario,string seriePedido, string numPedido, int nivel)
        {
            string Error = "";
            string AutN = "";

            if(nivel == 0){AutN = "";}
            else if (nivel == 1){AutN = " a sido autorizado por el autorizador del primer nivel, por lo que se le ha asignado para que pueda autorizarlo"; }
            else if (nivel == 11) { AutN = " a sido rechazado por el autorizador del primer nivel, por lo que se sugiere revisar el documento"; }
            else if (nivel == 2) { AutN = " a sido autorizado por el segundo nivel, por lo que se le ha asignado para que pueda autorizarlo"; }
            else if (nivel == 22) { AutN = " a sido rechazado por el segundo nivel, por lo que se sugiere revisar el documento"; }
            else if (nivel == 3) { AutN = " a sidoautorizado por los niveles correspondientes, el documento esta listo para utilizar"; }

            try
            {
                // Crear un nuevo objeto MailMessage
                MailMessage correo = new MailMessage();
                // Agregar el remitente
                correo.From = new MailAddress(Usuario);
                // Agregar el destinatario
                correo.To.Add(Para);
                // Agregar el asunto
                correo.Subject = Asunto;
                // Agregar el cuerpo del mensaje
                //correo.Body = comenAut;
                correo.IsBodyHtml = true;
                correo.HeadersEncoding = Encoding.UTF8;
                correo.BodyEncoding = Encoding.UTF8;
                
                string mensaje = "<html><body>";
                mensaje += $"<h3 style='color: #4682B4;'font-size: 18px;'>Buen día...<br>El documento: {seriePedido} / {numPedido} {AutN}.</h3>";
                mensaje += $"<p style='font-size: 18px;'>{comentario}</p>";
                mensaje += "<br>";
                mensaje += "<p style='font-size: 14px; color: #777;'>Favor de no responder a esta cuenta.</p>";
                mensaje += "<hr>";
                mensaje += "<h6 style='font-size: 14px; color: #4682B4;'>ICGSoftware México</h6>";
                mensaje += "<h6 style='font-size: 14px; color: #777;'> comercial@icg-mexico.com.mx • www.icg-mexico.com.mx<h6/>";
                mensaje += "</body></html>";
                correo.Body= mensaje;

                // Configurar el servidor SMTP
                SmtpClient smtp = new SmtpClient();
                smtp.Host = Host;
                smtp.Port = int.Parse(Puerto);
                smtp.EnableSsl = true;//colocar ssl | true habilita TLS
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new System.Net.NetworkCredential(Usuario,Pass);

                // Enviar el correo electrónico
                smtp.Send(correo);
                Error = "Correo enviado de manera exitosa";
                EscribirLog("info",Error , true, 2);

            }
            catch (Exception ex)
            {
                Error = "Error" + ex.Message;
                EscribirLog("info",ex.Message, true, 1);
                //MessageBox.Show(Error);
                return;
            }

        }
        
        
    }

    //Leer y Escribir Archivos Ini
    class IniFile   // revision 11
    {
        string Path;
        string EXE = Assembly.GetExecutingAssembly().GetName().Name;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public IniFile(string IniPath = null)
        {
            Path = new FileInfo(IniPath ?? EXE + ".ini").FullName;
        }

        public string Read(string Key, string Section = null)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }

        public void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
        }

        public void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? EXE);
        }

        public void DeleteSection(string Section = null)
        {
            Write(null, null, Section ?? EXE);
        }

        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }

    }

}

