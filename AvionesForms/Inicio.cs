using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProyectoLib;

namespace AvionesForms
{
    public partial class Inicio : Form
    {
        string dbLocation = "..\\..\\..\\PROYECTO_0\\UsersDB.sdf";
        GestionUsersDB usdb;
        bool accesoAceptado;
        int poder;

        public Inicio()
        {
            InitializeComponent();
            usdb = new GestionUsersDB(dbLocation);
            int res = usdb.openDB();
            if (res == -1)
            {
                MessageBox.Show("Error al intentar abrir la base de datos.");
            }
        }

        public bool GetAcceso()
        {
            return this.accesoAceptado;
        }

        public int GetPoderUsuario()
        {
            return this.poder;
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
            textBox1.MaxLength = 6;
            textBox2.MaxLength = 6;
        }

        private void button1_Click(object sender, EventArgs e) //Comprobar usuario en la base de datos UsersDB y dar acceso al form principal o indicaciones
        {
            this.accesoAceptado = false;
            usdb.openDB();
            int usuario = usdb.ConsultarUsuario(textBox1.Text);
            int contraseña = usdb.ConsultarContraseña(textBox2.Text);
            if (usuario == 0)
            {
                MessageBox.Show("El usuario introducido no existe.");
            }
            if (usuario == 1 && contraseña == 0)
            {
                MessageBox.Show("Contraseña incorrecta.");
            }
            if (usuario == 1 && contraseña == 1)
            {
                this.accesoAceptado = true;
                this.poder = usdb.GetPoder(textBox1.Text);
                this.Close();
            }
            usdb.closeDB();
        }

        private void button3_Click(object sender, EventArgs e) //Cerrar este form
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) //Invocar al form de registrar nuevo usuario
        {
            Registro nuevoUsuario = new Registro();
            nuevoUsuario.ShowDialog();
        }
    }
}
