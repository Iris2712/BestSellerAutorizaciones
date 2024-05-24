using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BestSellerAutorizaciones
{
    public partial class ConfCorreo : Form
    {
        /* Se crea una nueva instancia de la clase para ocupar de manera local en este formulario */
        Funciones funciones;
        public ConfCorreo()
        {
            InitializeComponent();
        }
        public ConfCorreo(Funciones funciones2)
        {
            InitializeComponent();
            this.funciones = funciones2;      // Se asigna el valor recibido a la instancia local de Funciones
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //if (radioButton1.Checked)
            //{
            //    // Código a ejecutar si radioButton1 está activo
            //    funciones.Protocolo = false;
            //}
            //else if(radioButton2.Checked)
            //{
            //    funciones.Protocolo = true;
            //}
            //Asignar variables
            funciones.Usuario = txtBoxCorreoG.Text;
            funciones.Pass = txtBoxPassCorreoG.Text;
            funciones.Host = txtBoxHost.Text;
            funciones.Puerto = txtBoxPuerto.Text;
            
            //Vlidar campos vacios
            if(funciones.CampVacio(txtBoxCorreoG.Text, "correo") == false &&
                funciones.CampVacio(txtBoxPassCorreoG.Text,"contraseña de correo") == false &&
                funciones.CampVacio(txtBoxHost.Text,"host") == false &&
                funciones.CampVacio(txtBoxPuerto.Text,"puerto") == false)
            {
                this.Hide();
                Parametros vparam = new Parametros(funciones);
                vparam.ShowDialog();
                this.Show();
            }
            else
            {
                this.Show();
            }

        }


        private void ConfCorreo_Load(object sender, EventArgs e)
        {
            txtBoxCorreoG.Text = funciones.Usuario;
            txtBoxPassCorreoG.Text = funciones.Pass;
            txtBoxHost.Text = funciones.Host;
            txtBoxPuerto.Text = funciones.Puerto;
        }
        private void ConfCorreo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                funciones.EscribirLog("info", "La aplicacion se cerro desde la barra de tareas de windows(Parametros", false, 0);
                System.Environment.Exit(0);
            }
        }

        private void txtBoxPuerto_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtBoxPuerto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar el carácter no numérico
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            funciones.EscribirLog("info", "Cancelacion de proceso", false, 0);
            //Application.Exit();
            System.Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            BD_gestion vcone = new BD_gestion(funciones);
            vcone.ShowDialog();
            this.Show();
        }

        //imagen de ocultar y mostrar contraseña
        private void pictureBoxMostrar_Click(object sender, EventArgs e)
        {
            pictureBoxOcultar.BringToFront();
            txtBoxPassCorreoG.PasswordChar = '\0';//mostramos la contraseña
        }

        private void pictureBoxOcultar_Click(object sender, EventArgs e)
        {
            pictureBoxMostrar.BringToFront();
            txtBoxPassCorreoG.PasswordChar = '*';//ocultamos la contraseña
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtBoxCorreoG_Validating(object sender, CancelEventArgs e) //Validar que el campo de correo es de tipo correo
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            TextBox textBox = (TextBox)sender;

            if (!Regex.IsMatch(textBox.Text, emailPattern))
            {
                //esEmail = false;
                //e.Cancel = true;
                funciones.EscribirLog("info", "Formato de correo electrónico incorrecto.", true, 4);
                //ErrorProvider.SetError(textBox, "Formato de correo electrónico incorrecto.");
            }
            else { 

                //esEmail = true;
                //e.Cancel = false;
                //funciones.EscribirLog("info", "", true, 1);
            }
        }

        
    }
}
