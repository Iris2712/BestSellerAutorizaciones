using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BestSellerAutorizaciones
{
    public partial class AccesConfi : Form
    {
        Funciones funciones;
        IniFile ArchivoConfig = new IniFile(Funciones.ArchivoConfig);
        public AccesConfi()
        {
            InitializeComponent();
        }
        public AccesConfi(Funciones funciones2)
        {
            InitializeComponent();
            this.funciones = funciones2;      // Se asigna el valor recibido a la instancia local de Funciones
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox2.BringToFront();
            txtPassconfi.PasswordChar = '\0';//mostramos la contraseña
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox1.BringToFront();
            txtPassconfi.PasswordChar = '*';//ocultamos la contraseña

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if(funciones.InicializarVar()==true)
            {
                funciones.ConexionBD();
                if (txtPassconfi.Text == funciones.PassConfig)
                {
                    //MessageBox.Show("Es la contraseña" + funciones.PassConfig);
                    this.Hide();
                    VConexionBD confi = new VConexionBD(funciones);
                    confi.ShowDialog();
                    this.Show();

                }
                else
                {
                    funciones.EscribirLog("Accesos a configuración", "La contraseña es Incorrecta", true, 4);
                }
            }
            
        }
    }
}
