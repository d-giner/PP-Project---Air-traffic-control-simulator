using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace ProyectoLib
{
    public class Sectores
    {
        //Atributos
        int Bottom_left_X;
        int Bottom_left_Y;
        int Top_right_X;
        int Top_right_Y;
        int NumeroSector;
        bool cargaCorrecta;
        List<Sectores> ListaSectores = new List<Sectores>();

        const int numerocampos = 4;

        //Constructor del sector
        public Sectores(int leftX, int lefty, int RIGHTx, int RIGHTy)
        {
            this.Bottom_left_X = leftX;
            this.Bottom_left_Y = lefty;
            this.Top_right_X = RIGHTx;
            this.Top_right_Y = RIGHTy;
        }

        //Constructor vacío del sector
        public Sectores()
        {
        }

        //Métodos para asginar coordenadas al sector
        public void SetBottomLeftX(int a)
        {
            this.Bottom_left_X = a;
        }
        public int GetBottomLeftX()
        {
            return (this.Bottom_left_X);
        }
        public void SetBottomLeftY(int b)
        {
            this.Bottom_left_Y = b;
        }
        public int GetBottomLeftY()
        {
            return (this.Bottom_left_Y);
        }
        public void SetTopRightX(int c)
        {
            this.Top_right_X = c;
        }
        public int GetTopRightX()
        {
            return (this.Top_right_X);
        }
        public void SetTopRightY(int d)
        {
            this.Top_right_Y = d;
        }
        public int GetTopRightY()
        {
            return (this.Top_right_Y);
        }
        public bool GetCargaCorrecta()
        {
            return this.cargaCorrecta;
        }
        public List<Sectores> GetListaSectores()
        {
            return this.ListaSectores;
        }
        public void SetSectorPinchado(int numero)
        {
            this.NumeroSector = numero;
        }
        public int GetSectorPinchado()
        {
            return this.NumeroSector;
        }

        public void cargarSectores(string name) //Cargar el fichero de sectores y crear una lista de ellos
        {
            string[] campo = new string[numerocampos];
            this.cargaCorrecta = true;
            string linealeida;
            StreamReader Fdatos;
            try
            {
                Fdatos = new StreamReader(name);

                linealeida = Fdatos.ReadLine();

                while (linealeida != null)
                {
                    campo = linealeida.Split(' '); //Rellena las 4 posiciones del vector con los 4 datos que hay por línea separados por espacios
                    Sectores s = new Sectores(Convert.ToInt32(campo[0]), Convert.ToInt32(campo[1]), Convert.ToInt32(campo[2]), Convert.ToInt32(campo[3])); //Nuevo sector y a continuación lo añadimos a la lista
                    ListaSectores.Add(s); //Añade el nuevo sector creado a la lista de sectores
                    linealeida = Fdatos.ReadLine();
                }
                Fdatos.Close();
            }
            catch (FileNotFoundException)
            {
                //Console.WriteLine("Fichero no encontrado");
                this.cargaCorrecta = false;
            }
            catch (FormatException)
            {
                this.cargaCorrecta = false;
                //Console.WriteLine("Formato del fichero incorrecto.");
            }
        }

        public int DentroSectorUno(ListaAviones lista) //Método para saber cuantos aviones hay dentro del sector
        {
            int i = 0;
            int cont = 0;
            if (this.cargaCorrecta == true)
            {
                while (i < lista.GetNumAviones())
                {
                    if ((lista.GetAvion(i).GetPosicionX() <= ListaSectores[0].GetTopRightX()) &&
                        (lista.GetAvion(i).GetPosicionY() <= ListaSectores[0].GetTopRightY()) &&
                        (lista.GetAvion(i).GetPosicionX() >= ListaSectores[0].GetBottomLeftX()) &&
                        (lista.GetAvion(i).GetPosicionY() >= ListaSectores[0].GetBottomLeftY()))
                    {
                        cont++;
                    }
                    i++;
                }
            }
            return cont;
        }

        public int DentroSectorDos(ListaAviones lista) //Método para saber cuantos aviones hay dentro del sector
        {
            int i = 0;
            int cont = 0;
            if (this.cargaCorrecta == true)
            {
                while (i < lista.GetNumAviones())
                {
                    if ((lista.GetAvion(i).GetPosicionX() <= ListaSectores[1].GetTopRightX()) &&
                        (lista.GetAvion(i).GetPosicionY() <= ListaSectores[1].GetTopRightY()) &&
                        (lista.GetAvion(i).GetPosicionX() >= ListaSectores[1].GetBottomLeftX()) &&
                        (lista.GetAvion(i).GetPosicionY() >= ListaSectores[1].GetBottomLeftY()))
                    {
                        cont++;
                    }
                    i++;
                }
            }
            return cont;
        }

        public int DentroSectorTres(ListaAviones lista) //Método para saber cuantos aviones hay dentro del sector
        {
            int i = 0;
            int cont = 0;
            if (this.cargaCorrecta == true)
            {
                while (i < lista.GetNumAviones())
                {
                    if ((lista.GetAvion(i).GetPosicionX() <= ListaSectores[2].GetTopRightX()) &&
                        (lista.GetAvion(i).GetPosicionY() <= ListaSectores[2].GetTopRightY()) &&
                        (lista.GetAvion(i).GetPosicionX() >= ListaSectores[2].GetBottomLeftX()) &&
                        (lista.GetAvion(i).GetPosicionY() >= ListaSectores[2].GetBottomLeftY()))
                    {
                        cont++;
                    }
                    i++;
                }
            }
            return cont;
        }

        public bool AvionDentroSectorUno(Aviones avion) //Método para verificar si un avión concreto está dentro del sector UNO
        {
            bool resultado = false;
            if ((avion.GetPosicionX() <= ListaSectores[0].GetTopRightX()) &&
                (avion.GetPosicionY() <= ListaSectores[0].GetTopRightY()) &&
                (avion.GetPosicionX() >= ListaSectores[0].GetBottomLeftX()) &&
                (avion.GetPosicionY() >= ListaSectores[0].GetBottomLeftY()))
            {
                resultado = true;
            }
            return resultado;
        }

        public bool AvionDentroSectorDos(Aviones avion) //Método para verificar si un avión concreto está dentro del sector DOS
        {
            bool resultado = false;
            if ((avion.GetPosicionX() <= ListaSectores[1].GetTopRightX()) &&
                (avion.GetPosicionY() <= ListaSectores[1].GetTopRightY()) &&
                (avion.GetPosicionX() >= ListaSectores[1].GetBottomLeftX()) &&
                (avion.GetPosicionY() >= ListaSectores[1].GetBottomLeftY()))
            {
                resultado = true;
            }
            return resultado;
        }

        public bool AvionDentroSectorTres(Aviones avion) //Método para verificar si un avión concreto está dentro del sector TRES
        {
            bool resultado = false;
            if ((avion.GetPosicionX() <= ListaSectores[2].GetTopRightX()) &&
                (avion.GetPosicionY() <= ListaSectores[2].GetTopRightY()) &&
                (avion.GetPosicionX() >= ListaSectores[2].GetBottomLeftX()) &&
                (avion.GetPosicionY() >= ListaSectores[2].GetBottomLeftY()))
            {
                resultado = true;
            }
            return resultado;
        }
    }
}