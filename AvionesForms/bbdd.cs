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
    public partial class bbdd : Form
    {
        string dbLocation = "..\\..\\..\\PROYECTO_0\\MiDatabase.sdf";
        DBGestion db;
        string n;
        int t;
        string m;
        private int res;

        public bbdd()
        {
            InitializeComponent();
            db = new DBGestion(dbLocation);
            int res = db.openDB();
            if (res == -1)
            {
                MessageBox.Show("Error abriendo la base de datos");
            }
        }


        private void bbdd_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'miDatabaseDataSet.información_compañias_aereas' table. You can move, or remove it, as needed.
            //    this.información_compañias_aereasTableAdapter.Fill(this.miDatabaseDataSet.información_compañias_aereas);
            DataTable dt = db.getAll();
            // Link the information in your table to your DataGridView control  
            dataGridView1.DataSource = dt;
            // Refresh the grid to show your data  
            dataGridView1.Refresh();
        } //Volcar base de datos en DataGrid

        private void fillToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.información_compañias_aereasTableAdapter.Fill(this.miDatabaseDataSet.información_compañias_aereas);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        } //Salir de este form

        private void button1_Click(object sender, EventArgs e) //Filtrar por compañías
        {
            DataTable dt = db.getByCompania(textBox1.Text);            
            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();
        }

        private void button4_Click(object sender, EventArgs e) //Añadir datos a la base de datos de compañías
        {
            db.openDB();
            try
            {
                n = textBox1.Text;
                m = textBox3.Text;
                t = Convert.ToInt32(textBox2.Text);
                res = db.getByAñadir(n, t, m);

                if (res == -1)
                {
                    MessageBox.Show("No se ha podido añadir el nombre.");
                }
                else if (res == -2)
                {
                    MessageBox.Show("Ya hay nombre.");
                }
                else
                {
                    MessageBox.Show("nombre creado.");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Error de conversión en alguno de los campos.");
            }
            db.closeDB();
            this.Close();     
        }

        private void button2_Click(object sender, EventArgs e) //Eliminar compañía
        {
            db.openDB();
            int res = db.getByEliminar(textBox1.Text);
            if (res == -1)
            {
                MessageBox.Show("No hay ninguna Compañía con ese nombre.");
            }
            else
            {
                DataTable dt = db.getAll();
                dataGridView1.DataSource = dt;
                dataGridView1.Refresh();
                textBox3.ResetText();
            }
            db.closeDB();
        }

        private void button3_Click(object sender, EventArgs e) //Modificar datos de la base de datos
        {
            db.openDB();
            DataTable dt = db.getAll();
            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();
            Modificar F2 = new Modificar();
            F2.ShowDialog();
            dt = db.getAll();
            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();
            db.closeDB();
        }

        private void button5_Click(object sender, EventArgs e) //Salir del form de la base de datos de las compañías
        {
            this.Close();
        }       
    }
}


 
            
        




       
    

