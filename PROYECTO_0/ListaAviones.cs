using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ProyectoLib
{
    public class ListaAviones
    {
        const int MAX = 100;
        Aviones[] aviones = new Aviones[MAX];
        int numAviones;
        const int numcampos = 7;
        Stack<Aviones> deshacer = new Stack<Aviones>();
        bool encontrado, cargaCorrecta;
        int numAvionEliminado;


        public void SetNumaviones() //Recorrer el vector para saber cuantos aviones tenemos
        {
            int cont = 0;
            while (this.aviones[cont] != null)
            {
                cont++;
            }
            this.numAviones = cont;
        }

        public int GetNumAviones() //Devolver el número de aviones que tenemos
        {
            return this.numAviones;
        }

        public void SetNumAvionEliminado(int cont)
        {
            this.numAvionEliminado = cont;
        }

        public int GetNumAvionEliminado()
        {
            return this.numAvionEliminado;
        }

        public Aviones GetAvion(int i) //Retornar un avión en concreto
        {
            return this.aviones[i];
        }

        public bool AvionEncontrado()
        {
            return this.encontrado;
        }
        public bool GetCargaCorrecta()
        {
            return this.cargaCorrecta;
        }

        public void Cargar(string name) //Método cargar el fichero con los datos de lo aviones
        {
            string[] campos = new string[numcampos];
            string linealeida;
            StreamReader Fdatos;
            this.aviones = new Aviones[MAX];
            this.cargaCorrecta = true;
            int i = 0;

            try
            {
                Fdatos = new StreamReader(name);
                linealeida = Fdatos.ReadLine();

                while (linealeida != null)
                {
                    campos = linealeida.Split('/');
                    this.aviones[i] = new Aviones();
                    this.aviones[i].SetIdentificador(campos[0]);
                    this.aviones[i].SetPosicionX(Convert.ToInt32(campos[1]));
                    this.aviones[i].SetPosicionY(Convert.ToInt32(campos[2]));
                    this.aviones[i].SetOrigenX(Convert.ToInt32(campos[1]));
                    this.aviones[i].SetOrigenY(Convert.ToInt32(campos[2]));
                    this.aviones[i].SetDestinoX(Convert.ToInt32(campos[3]));
                    this.aviones[i].SetDestinoY(Convert.ToInt32(campos[4]));
                    this.aviones[i].SetVelocidad(Convert.ToInt32(campos[5]));
                    this.aviones[i].SetCompañia((campos[6]));
                    linealeida = Fdatos.ReadLine();
                    i++;
                }
                Fdatos.Close();
            }
            catch (FileNotFoundException)
            {
                this.cargaCorrecta = false;
            }
            catch (FormatException)
            {
                this.cargaCorrecta = false;
            }
            catch (IndexOutOfRangeException)
            {
                this.cargaCorrecta = false;
            }
            i = 0;
            SetNumaviones();
        }

        public void imprimir() //Método mostrar en consola los datos de los aviones
        {
            for (int i = 0; i < this.numAviones; i++)
            {
                Console.Write(this.aviones[i].GetIdentificador() + "");
                Console.Write(" X:" + this.aviones[i].GetPosicionX() + "");
                Console.WriteLine(" Y:" + this.aviones[i].GetPosicionY());
            }
        }

        public void Mover() //Simular vuelo de los aviones, (cambio de posición en el espacio aéreo)
        {
            for (int i = 0; i < this.numAviones; i++)
            {
                aviones[i].Mover();
            }
        }

        public void Reiniciar() //Reiniciar la posición de los aviones a su posición inicial
        {
            for (int i = 0; i < this.numAviones; i++)
            {
                aviones[i].Reiniciar();
            }
        }

        public void BorrarPila() //Vaciar el contenido de la pila de aviones
        {
            for (int i = 0; i < this.numAviones; i++)
            {
                aviones[i].BorrarPila();
            }
        }

        public void NuevoAvion(string a, string co, int b, int c, float vel, float oriX, float oriY, float destX, float destY) //Añadir nuevos aviones al programa
        {
            Aviones avion = new Aviones(a, co, b, c, vel, oriX, oriY, destX, destY);
            aviones[this.numAviones] = avion;
            SetNumaviones();
        }

        public int Salvar(string name) //Método salvar nuevos datos en el fichero aviones
        {
            StreamWriter Fdatos;
            try
            {
                Fdatos = new StreamWriter(name);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Fichero no encontrado.");
                return 0;
            }

            //Guardar datos del programa
            int i = 0;
            while (i < this.numAviones)
            {
                Fdatos.WriteLine(this.aviones[i].GetIdentificador() + "/" + this.aviones[i].GetPosicionX() + "/" + this.aviones[i].GetPosicionY() + "/" + this.aviones[i].GetDestinoX() + "/" + this.aviones[i].GetDestinoY() + "/" + this.aviones[i].GetVelocidad() + "/" + this.aviones[i].GetCompañia());
                i++;
            }
            Fdatos.Close();
            return 1;
        }

        public void EliminarAvion(string id) //Eliminar un avión del vector de aviones y por ende, del espacio aereo
        {
            this.encontrado = false;
            int i = 0;
            int cont = 0;
            this.numAvionEliminado = 0;
            while (i < this.numAviones)
            {
                if (this.aviones[i].GetIdentificador() == id)
                {
                    cont = i;
                    this.numAvionEliminado = i;
                    this.encontrado = true;
                }
                i++;
            }

            if (this.encontrado == true)
            {
                while (cont < MAX - 1)
                {
                    this.aviones[cont] = this.aviones[cont + 1];
                    cont++;
                }
            }
            SetNumaviones();
        }
    }
}