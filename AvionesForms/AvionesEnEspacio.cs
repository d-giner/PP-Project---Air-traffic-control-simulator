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
    public partial class AvionesEnEspacio : Form
    {
        ListaAviones lista;

        public void SetData(ListaAviones lista)
        {
            this.lista = lista;
        }
        
        public AvionesEnEspacio()
        {
            InitializeComponent();
        }

        private void AvionesEnEspacio_Load(object sender, EventArgs e) //Rellenar el data grid con los datos de los aviones que hay en el espacio aéreo
        {
            dataGridAviones.ColumnCount = 4;
            dataGridAviones.RowCount = lista.GetNumAviones();
            dataGridAviones.RowHeadersVisible = false;
            dataGridAviones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            for (int i = 0; i < lista.GetNumAviones(); i++)
            {
                dataGridAviones[0, i].Value = lista.GetAvion(i).GetIdentificador();
                dataGridAviones[1, i].Value = lista.GetAvion(i).GetCompañia();
                dataGridAviones[2, i].Value = lista.GetAvion(i).GetOrigenX() + ", " + lista.GetAvion(i).GetOrigenY();
                dataGridAviones[3, i].Value = lista.GetAvion(i).GetDestinoX() + ", " + lista.GetAvion(i).GetDestinoY();                
            }
        }
    }
}
