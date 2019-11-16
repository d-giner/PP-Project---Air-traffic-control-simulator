using System;
using System.Windows.Forms;
using ProyectoLib;

namespace AvionesForms
{
    public partial class AvionEnSector : Form
    {
        ListaAviones lista;
        Sector sector;
        const int MAX = 100;
        

        public void SetData(ListaAviones lista, Sector sector)
        {
            this.lista = lista;
            this.sector = sector;
        }
        
        
        public AvionEnSector()
        {
            InitializeComponent();
        }

        private void AvionEnSector_Load(object sender, EventArgs e)
        {
            

            Aviones.ColumnCount = 4;
            Aviones.RowCount = 4;
            

            int i = 0;
            while (i < lista.GetNumAviones())
            {
                if (sector.DentroAvion(lista.GetAvion(i)))
                {
                    Aviones[0, i].Value = lista.GetAvion(i).GetIdentificador();
                    Aviones[1, i].Value = lista.GetAvion(i).GetPosicionX() + lista.GetAvion(i).GetPosicionY();
                    Aviones[2, i].Value = lista.GetAvion(i).GetDestinoX() + lista.GetAvion(i).GetDestinoY() ;
                    Aviones[3, i].Value = lista.GetAvion(i).GetVelocidad();
                }

                i++; 
            }

        }

        

      

       
        
    }
}
