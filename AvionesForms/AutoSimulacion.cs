using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AvionesForms
{
    public partial class AutoSimulacion : Form
    {
        public AutoSimulacion()
        {
            InitializeComponent();
        }

        bool SimulacionActiva = false;
        int tiempo, ciclo;

        public void SetSimulacionActiva(bool SimulacionActiva)
        {
            this.SimulacionActiva = SimulacionActiva;
        }
        public bool GetSimulacionActiva()
        {
            return this.SimulacionActiva;
        }
        public int GetCicloAutoSimulacion()
        {
            return this.ciclo;
        }
        public int GetTiempoAutoSimulacion()
        {
            return this.tiempo;
        }

        private void button2_Click(object sender, EventArgs e) //Salir de este form
        {
            this.SimulacionActiva = false;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e) //Recoger datos para ejecutar la simulación automática
        {
            try
            {
                this.ciclo = Convert.ToInt32(textCiclo.Text);
                this.tiempo = Convert.ToInt32(textTiempo.Text);
                this.SimulacionActiva = true;
                MessageBox.Show("Datos introducidos correctamente.");
                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, introduce un número entero.");
            }
        }

        private void AutoSimulacion_Load(object sender, EventArgs e)
        {

        }
    }
}
