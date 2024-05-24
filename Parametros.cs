using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BestSellerAutorizaciones
{
    public partial class Parametros : Form
    {
        /* Se crea una nueva instancia de la clase para ocupar de manera local en este formulario */
        Funciones funciones;
        IniFile ArchivoConfig = new IniFile(Funciones.ArchivoConfig);

        public Parametros()
        {
            InitializeComponent();
        }

        // Se crea un nuevo constructor que recibe un parámetro del tipo Funciones
        public Parametros(Funciones funciones2)
        {
            InitializeComponent();
            this.funciones = funciones2;      // Se asigna el valor recibido a la instancia local de Funciones
            ObtenerEmpleados();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //Asignar variables
            funciones.Monto = txBxMonto.Text;
            funciones.Autorizador = comboAutorizador.Text;//Nombre autorizador
            funciones.Aut2 = comboAut2.Text;
            funciones.Aut3 = comboAut3.Text;

            if (funciones.Autorizador.Length >= 3 && funciones.Aut2.Length >= 3 && funciones.Aut3.Length >= 3)
            {
                funciones.codAutorizador = funciones.Autorizador.Substring(0, 3);//Cod Autorizador
                funciones.codAut2 = funciones.Aut2.Substring(0, 3);
                funciones.codAut3 = funciones.Aut3.Substring(0, 3);
                
            }


            //Validar que los campos no esten vacios
            if (funciones.CampVacio(txBxMonto.Text, "Monto") == false && 
                funciones.CampVacio(comboAutorizador.Text, "Autorizador") == false && 
                funciones.CampVacio(comboAut2.Text,"Autorizador") == false &&
                funciones.CampVacio(comboAut3.Text,"Tercer Autorizador") == false)
            {
                if(MessageBox.Show("Al continuar se guardara el archivo de configuracion", "Confirmación", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)== DialogResult.OK)
                {
                    //Guardar Config. (ArchivoConfiguración)
                    ArchivoConfig.Write("ServidorSQL", funciones.ServidorSQL, "DatoSQL");
                    ArchivoConfig.Write("UsuarioSQL", funciones.UsuarioSQL, "DatoSQL");
                    ArchivoConfig.Write("PassSQL", funciones.PassSQL, "DatoSQL");
                    ArchivoConfig.Write("BDGeneral", funciones.BDGeneral, "DatoSQL");
                    ArchivoConfig.Write("BDGestion", funciones.BDGestion, "BDGestion");
                    ArchivoConfig.Write("BDContabilidad", funciones.BDContabilidad, "BDGestion");
                    ArchivoConfig.Write("BDContraseña config.", funciones.PassConfig, "BDGestion");
                    ArchivoConfig.Write("Correo general",funciones.Usuario, "SMTP");
                    ArchivoConfig.Write("Contraseña",funciones.Pass,"SMTP");
                    ArchivoConfig.Write("Host", funciones.Host, "SMTP");
                    ArchivoConfig.Write("Puerto",funciones.Puerto, "SMTP");
                    ArchivoConfig.Write("Monto", funciones.Monto, "Monto");
                    ArchivoConfig.Write("Autorizador", funciones.Autorizador, "Monto");
                    ArchivoConfig.Write("Cod.Autorizador", funciones.codAutorizador, "Monto");
                    ArchivoConfig.Write("Autorizador2", funciones.Aut2, "Monto");
                    ArchivoConfig.Write("Cod.Autorizador2", funciones.codAut2, "Monto");
                    ArchivoConfig.Write("Autorizador3", funciones.Aut3, "Monto");
                    ArchivoConfig.Write("Cod.Autorizador3", funciones.codAut3, "Monto");

                    funciones.EscribirLog("info", $"Se guardo el Archivo de configuracion '{Funciones.ArchivoConfig}' de manera correcta", true, 2);

                    this.Close();
                    System.Environment.Exit(0);
                    this.Hide();
                    VLogin vLogin = new VLogin(funciones);
                    vLogin.ShowDialog();
                    this.Show();
                }
                else
                {
                    this.Show();
                }
            }
        }

        private void Parametros_Load(object sender, EventArgs e)
        {
            comboAutorizador.Text = funciones.Autorizador;
            comboAut2.Text = funciones.Aut2;
            comboAut3.Text = funciones.Aut3;
            txBxMonto.Text = funciones.Monto;

        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            BD_gestion vgestio = new BD_gestion(funciones);
            vgestio.ShowDialog();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            funciones.EscribirLog("info", "Cancelacion de proceso", false, 0);
            //Application.Exit();
            System.Environment.Exit(0);
        }

        public void ObtenerEmpleados()
        {
            try
            {
                SqlCommand ObtenerEmpleado = new SqlCommand("SELECT CODUSUARIO,USUARIO,DEPARTAMENTO " +
                    $"FROM {funciones.BDGeneral}..USUARIOS WHERE CONEXION = @GrupEmp", funciones.CnnxICGMx);
                ObtenerEmpleado.Parameters.AddWithValue("@GrupEmp", funciones.palClaveGrupEmp);
                SqlDataReader ReaderObtenerEmpleado = ObtenerEmpleado.ExecuteReader();
                while (ReaderObtenerEmpleado.Read())
                {
                    string[] datos = { ReaderObtenerEmpleado["CODUSUARIO"].ToString() + "  " + ReaderObtenerEmpleado["USUARIO"].ToString() };
                    comboAutorizador.Items.AddRange(datos);
                    comboAut2.Items.AddRange(datos);
                    comboAut3.Items.AddRange(datos);
                    
                    //comboAutorizador.Items.Add(ReaderObtenerEmpleado["USUARIO"].ToString());
                }
                ReaderObtenerEmpleado.Close();
                funciones.EscribirLog("info", "Obtencion de empleados para autorizador de manera correcta", false, 0);
            }
            catch (Exception e)
            {
                funciones.EscribirLog("info", $"No es posible obtener los empleados autorizadores. ({e.Message})", true, 1);
            }
        }

        private void Parametros_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                funciones.EscribirLog("info", "La aplicacion se cerro desde la barra de tareas de windows(Parametros", false, 0);
                System.Environment.Exit(0);
            }
        }

        private void txBxMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter no numérico
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboAutorizador_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
