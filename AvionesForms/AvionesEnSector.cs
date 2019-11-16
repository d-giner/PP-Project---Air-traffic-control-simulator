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
    public partial class AvionesEnSector : Form
    {
        ListaAviones lista;
        Sectores sector;

        public void SetData(ListaAviones lista, Sectores sector)
        {
            this.lista = lista;
            this.sector = sector;
        }

        public AvionesEnSector()
        {
            InitializeComponent();
        }

        private void AvionesEnSector_Load(object sender, EventArgs e) //Rellenar el data grid con los datos de los aviones que hay en el sector seleccionado
        {
            dataGridAviones.ColumnCount = 4;
            dataGridAviones.RowHeadersVisible = false;
            dataGridAviones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            int i = 0;
            int j = 0;
            while (i < lista.GetNumAviones())
            {
                if (sector.AvionDentroSectorUno(lista.GetAvion(i)) && sector.GetSectorPinchado() == 1) //Rellenar data grid si se selecciona el sector 1
                {
                    label1.Text = ("Aviones en el sector: " + sector.GetSectorPinchado() + ".");
                    dataGridAviones.RowCount = sector.DentroSectorUno(this.lista);
                    dataGridAviones[0, j].Value = lista.GetAvion(i).GetIdentificador();
                    dataGridAviones[1, j].Value = lista.GetAvion(i).GetPosicionX() + ", " + lista.GetAvion(i).GetPosicionY();
                    dataGridAviones[2, j].Value = lista.GetAvion(i).GetDestinoX() + ", " + lista.GetAvion(i).GetDestinoY();
                    dataGridAviones[3, j].Value = lista.GetAvion(i).GetVelocidad();
                    j++;
                }
                if (sector.AvionDentroSectorDos(lista.GetAvion(i)) && sector.GetSectorPinchado() == 2) //Rellenar data grid si se selecciona el sector 2
                {
                    label1.Text = ("Aviones en el sector: " + sector.GetSectorPinchado() + ".");
                    dataGridAviones.RowCount = sector.DentroSectorDos(this.lista);
                    dataGridAviones[0, j].Value = lista.GetAvion(i).GetIdentificador();
                    dataGridAviones[1, j].Value = lista.GetAvion(i).GetPosicionX() + ", " + lista.GetAvion(i).GetPosicionY();
                    dataGridAviones[2, j].Value = lista.GetAvion(i).GetDestinoX() + ", " + lista.GetAvion(i).GetDestinoY();
                    dataGridAviones[3, j].Value = lista.GetAvion(i).GetVelocidad();
                    j++;
                }
                if (sector.AvionDentroSectorTres(lista.GetAvion(i)) && sector.GetSectorPinchado() == 3) //Rellenar data grid si se selecciona el sector 3
                {
                    label1.Text = ("Aviones en el sector: " + sector.GetSectorPinchado() + ".");
                    dataGridAviones.RowCount = sector.DentroSectorTres(this.lista);
                    dataGridAviones[0, j].Value = lista.GetAvion(i).GetIdentificador();
                    dataGridAviones[1, j].Value = lista.GetAvion(i).GetPosicionX() + ", " + lista.GetAvion(i).GetPosicionY();
                    dataGridAviones[2, j].Value = lista.GetAvion(i).GetDestinoX() + ", " + lista.GetAvion(i).GetDestinoY();
                    dataGridAviones[3, j].Value = lista.GetAvion(i).GetVelocidad();
                    j++;
                }
                i++;
            }
        }
    }
}