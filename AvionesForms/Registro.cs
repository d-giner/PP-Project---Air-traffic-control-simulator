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
    public partial class Registro : Form
    {
        string dbLocation = "..\\..\\..\\PROYECTO_0\\UsersDB.sdf";
        GestionUsersDB usdb;
        
        public Registro()
        {
            InitializeComponent();
            usdb = new GestionUsersDB(dbLocation);
            int res = usdb.openDB();
            if (res == -1)
            {
                MessageBox.Show("Error al intentar abrir la base de datos.");
            }
        }

        private void button2_Click(object sender, EventArgs e) //Cerrar este form
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e) //Recoger datos para crear un nuevo usuario y añadirlo a la base de datos
        {
            usdb.openDB();
            int res = usdb.ConsultarUsuario(textBox1.Text);
            int uLength = textBox1.Text.Length;
            int cLength = textBox2.Text.Length;
            if (uLength != 6 || cLength != 6)
            {
                MessageBox.Show("Por favor, introduce usuario y contraseña de 6 carácteres.");
            }
            if (res == 1)
            {
                MessageBox.Show("Nombre de usuario en uso. Por favor, escoge otro.");
            }
            else if (uLength == 6 && cLength == 6)
            {
                usdb.NuevoUsuario(textBox1.Text, textBox2.Text);
                MessageBox.Show("¡Usuario registrado con éxito!");
                this.Close();
            }
            usdb.closeDB();
        }

        private void Registro_Load(object sender, EventArgs e)
        {
            textBox1.MaxLength = 6;
            textBox2.MaxLength = 6;
        }
    }
}
