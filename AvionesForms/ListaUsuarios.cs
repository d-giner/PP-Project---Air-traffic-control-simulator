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
    public partial class ListaUsuarios : Form
    {
        string dbLocation = "..\\..\\..\\PROYECTO_0\\UsersDB.sdf";
        GestionUsersDB usdb;
        int filaSeleccionada;
        bool seleccionado = false;

        public ListaUsuarios()
        {
            InitializeComponent();
            usdb = new GestionUsersDB(dbLocation);
            int res = usdb.openDB();
            if (res == -1)
            {
                MessageBox.Show("Error al intentar abrir la base de datos.");
            }
        }

        private void ListaUsuarios_Load(object sender, EventArgs e) //Volcar base de datos en el Data Grid
        {
            usdb.openDB();
            DataTable dt = usdb.GetAll();
            dataGridUsuarios.DataSource = dt;
            dataGridUsuarios.RowHeadersVisible = false;
            dataGridUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridUsuarios.Refresh();
        }

        private void button2_Click(object sender, EventArgs e) //Cerrar el form
        {            
            usdb.closeDB();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e) //Eliminar de la base de datos el usuario seleccionado
        {
            string seleccion = dataGridUsuarios[0, this.filaSeleccionada].Value.ToString();
            if (this.seleccionado == true)
            {
                if (seleccion == "admin")
                {
                    MessageBox.Show("¡No te puedes eliminar a ti mism@!");
                }
                else
                {
                    usdb.openDB();
                    usdb.EliminarUsuario(seleccion);
                    DataTable dt = usdb.GetAll();
                    dataGridUsuarios.DataSource = dt;
                    dataGridUsuarios.RowHeadersVisible = false;
                    dataGridUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    dataGridUsuarios.Refresh();
                    this.seleccionado = false;
                }
            }
            else
            {
                MessageBox.Show("No has seleccionado a ningún usuario.");
            }
        }

        private void dataGridUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e) //Seleccionar un usuario haciendo click en su celda y guardarlo en la variable
        {
            this.filaSeleccionada = e.RowIndex;
            MessageBox.Show("Usuario seleccionado: " + dataGridUsuarios[0, this.filaSeleccionada].Value.ToString());
            this.seleccionado = true;
        }
    }
}
