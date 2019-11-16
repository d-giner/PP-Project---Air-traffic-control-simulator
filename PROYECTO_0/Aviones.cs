using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProyectoLib
{
    public class Aviones
    {
        //Atributos
        string identificador;
        string compañia;
        int posicionX;
        int posicionY;
        float velocidad;
        float origenX;
        float origenY;
        float destinoX;
        float destinoY;
        int tiempo;

        //Constructor de un avión
        public Aviones(string a, string co, int b, int c, float vel, float oriX, float oriY, float destX, float destY)
        {
            this.identificador = a;
            this.compañia = co;
            this.posicionX = b;
            this.posicionY = c;
            this.velocidad = vel;
            this.origenX = oriX;
            this.origenY = oriY;
            this.destinoX = destX;
            this.destinoY = destY;
        }

        //Constructor de un avion con valores a 0 y/o null
        public Aviones()
        {
        }

        Stack<int> pilaX = new Stack<int>();
        Stack<int> pilaY = new Stack<int>();

        //Métodos para asginar valores a los atributos
        public void SetIdentificador(string a)
        {
            this.identificador = a;
        }
        public string GetIdentificador()
        {
            return (this.identificador);
        }
        public void SetPosicionX(int b)
        {
            this.posicionX = b;
        }
        public int GetPosicionX()
        {
            return (this.posicionX);
        }
        public void SetPosicionY(int c)
        {
            this.posicionY = c;
        }
        public int GetPosicionY()
        {
            return (this.posicionY);
        }
        public void SetCompañia(string co)
        {
            this.compañia = co;
        }
        public string GetCompañia()
        {
            return (this.compañia);
        }
        public void SetVelocidad(float vel)
        {
            this.velocidad = vel;
        }
        public float GetVelocidad()
        {
            return (this.velocidad);
        }
        public void SetOrigenX(float oriX)
        {
            this.origenX = oriX;
        }
        public float GetOrigenX()
        {
            return (this.origenX);
        }
        public void SetOrigenY(float oriY)
        {
            this.origenY = oriY;
        }
        public float GetOrigenY ()
        {
            return (this.origenY);
        }
        public void SetDestinoX(float destX)
        {
            this.destinoX = destX;
        }
        public float GetDestinoX()
        {
            return (this.destinoX);
        }
        public void SetDestinoY(float destY)
        {
            this.destinoY = destY;
        }
        public float GetDestinoY()
        {
            return (this.destinoY);
        }
        public void SetTiempo(int tiempo)
        {
            this.tiempo = tiempo;
        }
        public int GetTiempo()
        {
            return this.tiempo;
        }

        public void Mover() //Método para simular el cambio de posiciones de los aviones
        {
            bool comprobacionX = false;
            bool comprobacionY = false;

            if (this.posicionX != this.destinoX)
            {
                pilaX.Push(this.posicionX);
                if (this.origenX < this.destinoX)
                    comprobacionX = false;
                else
                    comprobacionX = true;

                if (comprobacionX == false)
                {
                    double posx = this.tiempo * (this.velocidad / 60) * 2 * (this.destinoX - this.origenX) / Math.Sqrt((this.destinoX - this.origenX) * (this.destinoX - this.origenX) + ((this.destinoY - this.origenY) * (this.destinoY - this.origenY)));
                    if (this.posicionX + posx < this.destinoX)
                        this.posicionX += Convert.ToInt32(posx);
                    else
                        this.posicionX = Convert.ToInt32(this.destinoX);
                }
                else
                {
                    double posx = this.tiempo * (this.velocidad / 60) * 2 * (this.destinoX - this.origenX) / Math.Sqrt((this.destinoX - this.origenX) * (this.destinoX - this.origenX) + ((this.destinoY - this.origenY) * (this.destinoY - this.origenY)));
                    if (this.posicionX + posx > this.destinoX)
                        this.posicionX += Convert.ToInt32(posx);
                    else
                        this.posicionX = Convert.ToInt32(this.destinoX);
                }
            }

            if (this.posicionY != this.destinoY)
            {
                pilaY.Push(this.posicionY);
                if (this.origenY < this.destinoY)
                    comprobacionY = false;
                else
                    comprobacionY = true;

                if (comprobacionY == false)
                {
                    double posy = this.tiempo * (this.velocidad / 60) * 2 * (this.destinoY - this.origenY) / Math.Sqrt((this.destinoX - this.origenX) * (this.destinoX - this.origenX) + ((this.destinoY - this.origenY) * (this.destinoY - this.origenY)));
                    if (this.posicionY + posy < this.destinoY)
                        this.posicionY += Convert.ToInt32(posy);
                    else
                        this.posicionY = Convert.ToInt32(this.destinoY);
                }
                else
                {
                    double posy = this.tiempo * (this.velocidad / 60) * 2 * (this.destinoY - this.origenY) / Math.Sqrt((this.destinoX - this.origenX) * (this.destinoX - this.origenX) + ((this.destinoY - this.origenY) * (this.destinoY - this.origenY)));
                    if (this.posicionY + posy > this.destinoY)
                        this.posicionY += Convert.ToInt32(posy);
                    else
                        this.posicionY = Convert.ToInt32(this.destinoY);
                }
            }
        }
        
        public void Reiniciar() //Método para reiniciar la simulación, devolvemos las posiciones actuales a las posiciones iniciales
        {
            this.posicionX = Convert.ToInt32(this.origenX);
            this.posicionY = Convert.ToInt32(this.origenY);
        }
        
        public void DeshacerPosiciones() //Pilas de posiciones X e Y para poder deshacer avances en la simulación
        {
            this.posicionX = pilaX.Pop();
            this.posicionY = pilaY.Pop();
        }

        public int NumeroPilaX() //Saber cuantos datos apilados tenemos para las posiciones en el eje X
        {
            return pilaX.Count;
        }

        public int NumeroPilaY() //Saber cuantos datos apilados tenemos para las posiciones en el eje Y
        {
            return pilaY.Count;
        }

        public void BorrarPila() //Vacía las pilas de posiciones
        {
            pilaX.Clear();
            pilaY.Clear();
        }
    }
}
