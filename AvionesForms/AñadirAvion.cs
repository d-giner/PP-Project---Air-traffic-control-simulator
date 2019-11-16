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
    public partial class AñadirAvion : Form
    {
        ListaAviones añadir = new ListaAviones();
        
        public AñadirAvion()
        {
            InitializeComponent();
        }

        string identificador;
        string compañia;
        float velocidad;
        float origenX;
        float origenY;
        float destinoX;
        float destinoY;
        bool comprobarAñadido = false;

        //Diversos métodos para recoger los atributos para el nuevo avión
        public void SetIdentificador(string id)
        {
            this.identificador = id;
        }
        public string GetIdentificador()
        {
            return this.identificador;
        }
        public void SetCompañia(string co)
        {
            this.compañia = co;
        }
        public string GetCompañia()
        {
            return this.compañia;
        }
        public void SetOrigenX(float orix)
        {
            this.origenX = orix;
        }
        public float GetOrigenX()
        {
            return this.origenX;
        }
        public int GetPosicionX()
        {
            int posx = Convert.ToInt32(this.origenX);
            return posx;
        }
        public void SetOrigenY(float oriy)
        {
            this.origenY = oriy;
        }
        public float GetOrigenY()
        {
            return this.origenY;
        }
        public int GetPosicionY()
        {
            int posy = Convert.ToInt32(this.origenY);
            return posy;
        }
        public void SetDestinoX(float destx)
        {
            this.destinoX = destx;
        }
        public float GetDestinoX()
        {
            return this.destinoX;
        }
        public void SetDestinoY(float desty)
        {
            this.destinoY = desty;
        }
        public float GetDestinoY()
        {
            return this.destinoY;
        }
        public void SetVelocidad(float vel)
        {
            this.velocidad = vel;
        }
        public float GetVelocidad()
        {
            return this.velocidad;
        }
        public bool GetConfirmarAñadido()
        {
            return this.comprobarAñadido;
        }

        private void button2_Click(object sender, EventArgs e) //Cerrar este form
        {
            this.comprobarAñadido = false;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e) //Recoger los datos del nuevo avión a crear y pasarlos por parametro a la función encargada de crearlo
        {
            try
            {
                this.identificador = textBox1.Text;
                this.compañia = textBox7.Text;
                this.origenX = Convert.ToSingle(textBox2.Text);
                this.origenY = Convert.ToSingle(textBox5.Text);
                this.destinoX = Convert.ToSingle(textBox6.Text);
                this.destinoY = Convert.ToSingle(textBox3.Text);
                this.velocidad = Convert.ToSingle(textBox4.Text);
                this.comprobarAñadido = true;
                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Introduce los datos correctamente.");
            }
        }

        private void AñadirAvion_Load(object sender, EventArgs e)
        {

        }
    }
}
