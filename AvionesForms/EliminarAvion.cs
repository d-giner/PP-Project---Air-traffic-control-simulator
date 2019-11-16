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
    public partial class EliminarAvion : Form
    {
        ListaAviones lista;
        bool ConfirmarEliminado = false;

        public bool GetConfirmarElimiado()
        {
            return ConfirmarEliminado;
        }
        
        public EliminarAvion()
        {
            InitializeComponent();
        }

        public void SetData(ListaAviones lista)
        {
            this.lista = lista;
        }

        private void EliminarAvion_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //Recibir el identificador del avion a eliminar y enviarlo por parámetro
        {
            lista.EliminarAvion(textBox1.Text);
            ConfirmarEliminado = true;
            this.Close();
        }
    }
}
