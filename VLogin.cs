using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BestSellerAutorizaciones
{
    public partial class VLogin : Form
    {
        /* Se crea una nueva instancia de la clase para ocupar de manera local en este formulario */
        Funciones funciones;
        IniFile ArchivoConfig = new IniFile(Funciones.ArchivoConfig);
        public VLogin()
        {
            InitializeComponent();
        }
        public VLogin(Funciones funciones2)
        {
            InitializeComponent();
            this.funciones = funciones2; // Se asigna el valor recibido a la instancia local de Funciones
            
        }
        private void VLogin_Load(object sender, EventArgs e){}

        private void button2_Click(object sender, EventArgs e)
        {
            funciones.EscribirLog("info", "Cancelacion de proceso", false, 0);
            Application.Exit();
        }

        //imagen de ocultar y mostrar contraseña
        private void pictureBoxMostrar_Click(object sender, EventArgs e)
        {
            pictureBoxOcultar.BringToFront();
            textBoxPassLogin.PasswordChar = '\0';//mostramos la contraseña
        }
        private void pictureBoxOcultar_Click(object sender, EventArgs e)
        {
            pictureBoxMostrar.BringToFront();
            textBoxPassLogin.PasswordChar = '*';//ocultamos la contraseña

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            //Asignar variables
            funciones.UsuLogin = textBoxUsuLogin.Text;
            funciones.PassLogin = textBoxPassLogin.Text;

            //Validar que los campos no esten vacios
            if (funciones.CampVacio(textBoxUsuLogin.Text, "Usuario") == false &&
                funciones.CampVacio(textBoxPassLogin.Text, "Contraseña") == false)
            {
                SqlCommand valUsuarios = new SqlCommand($"SELECT USUARIO, EMAIL FROM USUARIOS WHERE EMAIL = @UsuLogin ", funciones.CnnxICGMx);
                valUsuarios.Parameters.AddWithValue("@UsuLogin", textBoxUsuLogin.Text);
                SqlDataReader ReadervalUsuarios = valUsuarios.ExecuteReader();
                
              
                    if (ReadervalUsuarios.HasRows)//Valida exixtencia de usuario
                    {
                        SqlCommand valUsuPass = new SqlCommand($"SELECT CODUSUARIO, USUARIO, EMAIL, PASSWORDMAIL FROM USUARIOS WHERE EMAIL = @UsuLogin AND PASSWORDMAIL = @PassLogin AND CONEXION = @grupAut", funciones.CnnxICGMx);
                        valUsuPass.Parameters.AddWithValue("@UsuLogin", textBoxUsuLogin.Text);
                        valUsuPass.Parameters.AddWithValue("@PassLogin", textBoxPassLogin.Text);
                        valUsuPass.Parameters.AddWithValue("@grupAut", funciones.palClaveGrupEmp);
                    SqlDataReader ReadervalUsuPass = valUsuPass.ExecuteReader();

                        if (ReadervalUsuPass.Read())//Valida usuario y pass correctos
                        {
                            funciones.EscribirLog("info", "Login correcto", false, 0);
                            this.Hide();
                        
                            funciones.NameUsuLogin = ReadervalUsuPass["USUARIO"].ToString();
                            funciones.codUsLogin = ReadervalUsuPass["CODUSUARIO"].ToString();
                        }
                        else
                        {
                            funciones.EscribirLog("info", "Usuario y/o contraseña erroneo", true, 4);
                        }
                    }
                    else
                    {
                        funciones.EscribirLog("info", "El usuario no existe o no pertenece al grupo de autorizadores.\n Rectifique y vuelva a intentarlo.", true, 4);
                        return;
                    }
                
            }
        }

        //Controlar el cierre desde la barra de tareas de windows
        private void VLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                funciones.EscribirLog("info", "La aplicacion se cerro desde la barra de tareas de windows(Login)", false, 0);
                System.Environment.Exit(0);
            }
        }

       
    }   
}
