using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BestSellerAutorizaciones
{
    public partial class VConexionBD : Form
    {
        /* Se crea una nueva instancia de la clase para ocupar de manera local en este formulario */
        Funciones funciones;

        public VConexionBD()
        {
            InitializeComponent();
        }

        // Se crea un nuevo constructor que recibe un parámetro del tipo Funciones
        public VConexionBD(Funciones funciones2)
        {
            InitializeComponent();
            this.funciones = funciones2; // Se asigna el valor recibido a la instancia local de Funciones
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Asignar Variables
            funciones.ServidorSQL = ServidorSQL.Text;
            funciones.UsuarioSQL = UsuarioSQL.Text;
            funciones.PassSQL = ContraseñaSQL.Text;
            funciones.BDGeneral = BDGeneral.Text;

            //Validar que el campo no esta vacio

            if (funciones.CampVacio(ServidorSQL.Text, "Servidor") == false &&
                funciones.CampVacio(UsuarioSQL.Text, "Usuario") == false &&
                funciones.CampVacio(ContraseñaSQL.Text, "Contraseña") == false &&
                funciones.CampVacio(BDGeneral.Text, "Base de datos General") == false)
            {
                //Llamar conexiónBD
                Boolean Conexion;
                Conexion = funciones.ConexionBD();
                if (Conexion == true)
                {
                    //Llamar siguiente ventana
                    this.Hide();
                    BD_gestion VBDG = new BD_gestion(funciones);
                    VBDG.ShowDialog();
                    this.Show();
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            funciones.EscribirLog("info", "Cancelacion de proceso", false, 0);
            //Application.Exit();
            System.Environment.Exit(0);
        }
        private void ConexionBD_Load(object sender, EventArgs e)
        {
            
                ServidorSQL.Text = funciones.ServidorSQL;
                UsuarioSQL.Text = funciones.UsuarioSQL;
                ContraseñaSQL.Text = funciones.PassSQL;
                BDGeneral.Text = funciones.BDGeneral;
                      
        }

        private void VConexionBD_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (e.CloseReason == CloseReason.UserClosing)
            //{
                funciones.EscribirLog("info", "La aplicacion se cerro desde la barra de tareas de windows.(pantalla de conexion a la BD)", false, 0);
                System.Environment.Exit(0);
            //}
        }
    }
}
