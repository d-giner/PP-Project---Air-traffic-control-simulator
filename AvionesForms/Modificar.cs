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
    public partial class Modificar : Form
    {
        string dbLocation = "..\\..\\..\\PROYECTO_0\\MiDatabase.sdf";
        DBGestion db;
        string n;
        int t;
        string m, nm;
        int res;
        public Modificar()
        {
            InitializeComponent();
            db = new DBGestion(dbLocation);
            int res = db.openDB();
            if (res != 0)
            {
                MessageBox.Show("Error abriendo la base de datos.");
            }
        }

        private void Modificar_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //Recoger datos a modificar en la base de datos y cerrar el form
        {
            db.openDB();
            try
            {
                n = textBox1.Text;
                m = textBox3.Text;
                t = Convert.ToInt32(textBox2.Text);
                nm = textBox4.Text;
                res = db.getByModificar(n,nm,t,m);

                if (res == -1)
                {
                    MessageBox.Show("No se ha encontrado ninguna compañía con ese nombre.");
                }
                else
                {
                    MessageBox.Show("Parámetros modificados correctamente.");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Error de conversión en alguno de los campos.");
            }

            this.Close();
        }       
   }
}
