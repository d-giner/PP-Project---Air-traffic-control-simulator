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
    public partial class Simulacion : Form
    {
        ListaAviones lista = new ListaAviones();
        Sectores sector = new Sectores();
        const int MAX = 100;
        bbdd basedatos = new bbdd();
        Inicio inicio = new Inicio();        
        PictureBox[] avion_picture_vector = new PictureBox[MAX];
        AvionesEnSector avionensector = new AvionesEnSector();
        AvionesEnEspacio avionEnEspacio = new AvionesEnEspacio();
        ListaUsuarios consultaUsuarios = new ListaUsuarios();
        int segundos, ContadorAutoSimulacion, segundosTotales;
        bool RutaTransparente;
        bool avionesCargados = false;
        bool sectoresCargados = false;

        public Simulacion()
        {
            InitializeComponent();
        }

        private void evento(object sender, EventArgs e) //Listar los datos de un avion al hacer click sobre él
        {
            PictureBox p = (PictureBox)sender;
            int tag = (int)p.Tag;
            string id = lista.GetAvion(tag).GetIdentificador();
            string co = lista.GetAvion(tag).GetCompañia();
            int posx = lista.GetAvion(tag).GetPosicionX();
            int posy = lista.GetAvion(tag).GetPosicionY();
            float destx = lista.GetAvion(tag).GetDestinoX();
            float desty = lista.GetAvion(tag).GetDestinoY();
            float vel = lista.GetAvion(tag).GetVelocidad();

            string dbLocation = "..\\..\\..\\PROYECTO_0\\MiDatabase.sdf";
            DBGestion db;

            db = new DBGestion(dbLocation);
            int res = db.openDB();
            if (res == -1)
            {
                MessageBox.Show("Error abriendo la base de datos");
            }

            DataTable dt = db.getByCompania(co);
            if (dt.Rows.Count > 0)
            { 
                DataRow row = dt.Rows[0];
                MessageBox.Show("Este es el avión: " + id + "." + "\nCon coordenadas: " + "(" + posx + "," + posy + ")." + "\nCon destino: " + "(" + destx + "," + desty + ")." + "\nCon velocidad: " + vel + " nudos." + "\nCon compañía: " + co + "." + "\nEmail: " + row[2] + ".");
            }
            else
            {
                MessageBox.Show("Este es el avión: " + id + "." + "\nCon coordenadas: " + "(" + posx + "," + posy + ")." + "\nCon destino: " + "(" + destx + "," + desty + ")." + "\nCon velocidad: " + vel + " nudos." + "\nCon compañía: " + co + "." + "\nEmail: No disponible.");
            }
            db.closeDB();
        }

        private void avionesToolStripMenuItem_Click(object sender, EventArgs e) //Cargar fichero aviones e identificar cada avión emplazado en el espacio aéreo al hacer clic sobre él
        {
            if (this.avionesCargados == false)
            {
                openFileDialog1.ShowDialog();
                lista.Cargar(openFileDialog1.FileName);
                if (lista.GetCargaCorrecta() == false)
                {
                    MessageBox.Show("Formato de fichero incorrecto o no seleccionado.");
                }
                else
                {
                    this.avionesCargados = true;
                    for (int i = 0; i < lista.GetNumAviones(); i++) //Bucle para rellenar el vector con los datos de los aviones, coger sus localizaciones y demás datos.
                    {
                        PictureBox avion_picture = new PictureBox();
                        avion_picture.ClientSize = new Size(20, 20);
                        avion_picture.Location = new Point(lista.GetAvion(i).GetPosicionX(), lista.GetAvion(i).GetPosicionY());
                        avion_picture.SizeMode = PictureBoxSizeMode.StretchImage;
                        Bitmap image = new Bitmap("avion.jpg");
                        avion_picture.Image = (Image)image;
                        panel1.Controls.Add(avion_picture);
                        avion_picture_vector[i] = avion_picture;
                        avion_picture.Tag = i;
                        avion_picture.Click += new System.EventHandler(this.evento);
                    }
                }
                if (sector.GetCargaCorrecta() == true)
                {
                    label2.Text = Convert.ToString(sector.DentroSectorUno(lista));
                    label7.Text = Convert.ToString(sector.DentroSectorDos(lista));
                    label8.Text = Convert.ToString(sector.DentroSectorTres(lista));
                }
                panel1.Invalidate();
            }
            else
            {
                MessageBox.Show("¡Los aviones ya están cargados!");
            }
        }

        private void Simulación_Load(object sender, EventArgs e) //Cosas varias a ejecutar nada más arrancar el programa
        {
            timer2.Interval = 1000;
            timer2.Start();
            inicio.ShowDialog();
            if (inicio.GetAcceso() == false)
            {
                this.Close();
            }
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            label4.BackColor = Color.Transparent;
            label5.BackColor = Color.Transparent;
            label6.BackColor = Color.Transparent;
            label7.BackColor = Color.Transparent;
            label8.BackColor = Color.Transparent;
            label9.BackColor = Color.Transparent;
            label10.BackColor = Color.Transparent;
            label11.BackColor = Color.Transparent;
            label3.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label2.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label2.Text = Convert.ToString(sector.DentroSectorUno(lista));
            label7.Text = Convert.ToString(sector.DentroSectorDos(lista));
            label8.Text = Convert.ToString(sector.DentroSectorTres(lista));
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = true;
            button6.Visible = false;
            button7.Visible = false;
            button8.Visible = false;
            label1.Visible = false;
            textBox1.Visible = false;
            label4.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e) //Simular manualmente el tráfico aéreo
        {

            if (lista.GetCargaCorrecta() == true)
            {
                try
                {
                    for (int i = 0; i < this.lista.GetNumAviones(); i++)
                    {
                        lista.GetAvion(i).SetTiempo(Convert.ToInt32(textBox1.Text));
                    }
                    lista.Mover();
                    for (int i = 0; i < this.lista.GetNumAviones(); i++)
                    {
                        avion_picture_vector[i].Location = new Point(lista.GetAvion(i).GetPosicionX(), lista.GetAvion(i).GetPosicionY());
                    }
                    label2.Text = Convert.ToString(sector.DentroSectorUno(lista));
                    label7.Text = Convert.ToString(sector.DentroSectorDos(lista));
                    label8.Text = Convert.ToString(sector.DentroSectorTres(lista));
                }
                catch (FormatException)
                {
                    MessageBox.Show("Escribe correctamente el tiempo del ciclo.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, primero carga los ficheros.");
            }
            panel1.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e) //Reiniciar los aviones a su posicion inicial antes de la simulación
        {
            lista.BorrarPila();
            lista.Reiniciar();
            label2.Text = Convert.ToString(sector.DentroSectorUno(lista));
            label7.Text = Convert.ToString(sector.DentroSectorDos(lista));
            label8.Text = Convert.ToString(sector.DentroSectorTres(lista));
            for (int i = 0; i < this.lista.GetNumAviones(); i++)
            {
                lista.GetAvion(i).BorrarPila();
                avion_picture_vector[i].Location = new Point(lista.GetAvion(i).GetPosicionX(), lista.GetAvion(i).GetPosicionY());
            }
            panel1.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e) //Deshacer el último movimiento
        {
                for (int i = 0; i < this.lista.GetNumAviones(); i++)
                {
                    if (lista.GetAvion(i).NumeroPilaY() > 0)
                    {
                        lista.GetAvion(i).DeshacerPosiciones();
                        avion_picture_vector[i].Location = new Point(lista.GetAvion(i).GetPosicionX(), lista.GetAvion(i).GetPosicionY());
                    }
                    else
                    {
                        MessageBox.Show("No hay ningún ciclo que deshacer para el avión: " + lista.GetAvion(i).GetIdentificador() + ".");
                    }
                }
                label2.Text = Convert.ToString(sector.DentroSectorUno(lista));
                label7.Text = Convert.ToString(sector.DentroSectorDos(lista));
                label8.Text = Convert.ToString(sector.DentroSectorTres(lista));
                panel1.Invalidate();
        }

        private void añadirNuevoAviónToolStripMenuItem_Click(object sender, EventArgs e) //Añadir nuevo avión a la simulación
        {
            if (lista.GetCargaCorrecta() == true)
            {
                AñadirAvion añadir = new AñadirAvion();
                añadir.ShowDialog();
                if (añadir.GetConfirmarAñadido() == true)
                {
                    lista.NuevoAvion(añadir.GetIdentificador(), añadir.GetCompañia(), añadir.GetPosicionX(), añadir.GetPosicionY(), añadir.GetVelocidad(), añadir.GetOrigenX(), añadir.GetOrigenY(), añadir.GetDestinoX(), añadir.GetDestinoY());
                    MessageBox.Show(Convert.ToString("¡Nuevo avión añadido correctamente! Ahora hay: " + lista.GetNumAviones() + "."));
                    lista.BorrarPila();
                    PictureBox avion_picture = new PictureBox();
                    avion_picture.ClientSize = new Size(20, 20);
                    int i = lista.GetNumAviones() - 1;
                    avion_picture.Location = new Point(lista.GetAvion(i).GetPosicionX(), lista.GetAvion(i).GetPosicionY());
                    avion_picture.SizeMode = PictureBoxSizeMode.StretchImage;
                    Bitmap image = new Bitmap("avion.jpg");
                    avion_picture.Image = (Image)image;
                    panel1.Controls.Add(avion_picture);
                    avion_picture_vector[i] = avion_picture;
                    avion_picture.Tag = i;
                    avion_picture.Click += new System.EventHandler(this.evento);
                }
                label2.Text = Convert.ToString(sector.DentroSectorUno(lista));
                panel1.Invalidate();
            }
            else
            {
                MessageBox.Show("Por favor, primero carga el ficheros de los aviones.");
            }
        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e) //Guardar en el fichero los cambios del programa
        {
            if (lista.GetCargaCorrecta() == true)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.ShowDialog();
                lista.Salvar(save.FileName + ".txt");
            }
            else
            {
                MessageBox.Show("No hay cambios que guardar.");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e) //Pintar los sectores y las rutas de los aviones en el espacio aéreo
        {
            System.Drawing.Graphics graphics = e.Graphics;
            Pen blackPenSectorUno = new Pen(Color.Black, 2);
            Pen blackPenSectorDos = new Pen(Color.Black, 2);
            Pen blackPenSectorTres = new Pen(Color.Black, 2);            
            if (lista.GetCargaCorrecta() == true) //Si los aviones se han cargado correctamente, pintar la ruta de cada uno de ellos (de origen a destino)
            {
                if (this.RutaTransparente == true)
                {
                    Pen greenPenRuta = new Pen(Color.Transparent, 2);
                    for (int i = 0; i < lista.GetNumAviones(); i++)
                    {
                        Point PosicionOrigen = new Point(Convert.ToInt32(lista.GetAvion(i).GetOrigenX()), Convert.ToInt32(lista.GetAvion(i).GetOrigenY()));
                        Point PosicionDestino = new Point(Convert.ToInt32(lista.GetAvion(i).GetDestinoX()), Convert.ToInt32(lista.GetAvion(i).GetDestinoY()));
                        graphics.DrawLine(greenPenRuta, PosicionOrigen, PosicionDestino);
                    }
                    greenPenRuta.Dispose();
                }
                if (this.RutaTransparente == false)
                {
                    Pen greenPenRuta = new Pen(Color.Green, 2);
                    for (int i = 0; i < lista.GetNumAviones(); i++)
                    {
                        Point PosicionOrigen = new Point(Convert.ToInt32(lista.GetAvion(i).GetOrigenX()), Convert.ToInt32(lista.GetAvion(i).GetOrigenY()));
                        Point PosicionDestino = new Point(Convert.ToInt32(lista.GetAvion(i).GetDestinoX()), Convert.ToInt32(lista.GetAvion(i).GetDestinoY()));
                        graphics.DrawLine(greenPenRuta, PosicionOrigen, PosicionDestino);
                    }
                    greenPenRuta.Dispose();                    
                }
            }
            if (sector.GetCargaCorrecta() == true) //Si el sector se ha cargado correctamente, via libre para pintar los sectores 
            {                
                Point[] sectorUno = new Point[4]; //Pintar sector uno
                sectorUno[0] = new Point(sector.GetListaSectores()[0].GetBottomLeftX(), sector.GetListaSectores()[0].GetBottomLeftY());
                sectorUno[1] = new Point(sector.GetListaSectores()[0].GetTopRightX(), sector.GetListaSectores()[0].GetBottomLeftY());
                sectorUno[2] = new Point(sector.GetListaSectores()[0].GetTopRightX(), sector.GetListaSectores()[0].GetTopRightY());
                sectorUno[3] = new Point(sector.GetListaSectores()[0].GetBottomLeftX(), sector.GetListaSectores()[0].GetTopRightY());
                graphics.DrawPolygon(blackPenSectorUno, sectorUno);
                blackPenSectorUno.Dispose();
                Point[] sectorDos = new Point[4]; //Pintar sector dos
                sectorDos[0] = new Point(sector.GetListaSectores()[1].GetBottomLeftX(), sector.GetListaSectores()[1].GetBottomLeftY());
                sectorDos[1] = new Point(sector.GetListaSectores()[1].GetTopRightX(), sector.GetListaSectores()[1].GetBottomLeftY());
                sectorDos[2] = new Point(sector.GetListaSectores()[1].GetTopRightX(), sector.GetListaSectores()[1].GetTopRightY());
                sectorDos[3] = new Point(sector.GetListaSectores()[1].GetBottomLeftX(), sector.GetListaSectores()[1].GetTopRightY());
                graphics.DrawPolygon(blackPenSectorDos, sectorDos);
                blackPenSectorDos.Dispose();
                Point[] sectorTres = new Point[4]; //Pintar sector tres
                sectorTres[0] = new Point(sector.GetListaSectores()[2].GetBottomLeftX(), sector.GetListaSectores()[2].GetBottomLeftY());
                sectorTres[1] = new Point(sector.GetListaSectores()[2].GetTopRightX(), sector.GetListaSectores()[2].GetBottomLeftY());
                sectorTres[2] = new Point(sector.GetListaSectores()[2].GetTopRightX(), sector.GetListaSectores()[2].GetTopRightY());
                sectorTres[3] = new Point(sector.GetListaSectores()[2].GetBottomLeftX(), sector.GetListaSectores()[2].GetTopRightY());
                graphics.DrawPolygon(blackPenSectorTres, sectorTres);
                blackPenSectorTres.Dispose();
                if (sector.DentroSectorUno(lista) > 2) //Si hay más de X número de aviones en el sector uno, cambiar a color rojo
                {
                    Pen redPen = new Pen(Color.Red, 5);
                    graphics.DrawPolygon(redPen, sectorUno);
                }
                if (sector.DentroSectorDos(lista) > 0) //Si hay más de X número de aviones en el sector dos, cambiar a color rojo
                {
                    Pen redPen = new Pen(Color.Red, 5);
                    graphics.DrawPolygon(redPen, sectorDos);
                }
                if (sector.DentroSectorTres(lista) > 0) //Si hay más de X número de aviones en el sector tres, cambiar a color rojo
                {
                    Pen redPen = new Pen(Color.Red, 5);
                    graphics.DrawPolygon(redPen, sectorTres);
                }
            }
        }

        private void sectoresToolStripMenuItem_Click(object sender, EventArgs e) //Cargar fichero sectores y datos de los sectores
        {
            if (sectoresCargados == false)
            {                
                openFileDialog1.ShowDialog();
                sector.cargarSectores(openFileDialog1.FileName);
                if (sector.GetCargaCorrecta() == false)
                {
                    MessageBox.Show("Formato de fichero incorrecto o no seleccionado.");
                }
                else
                {
                    label3.Visible = true;
                    label5.Visible = true;
                    label6.Visible = true;
                    label2.Visible = true;
                    label7.Visible = true;
                    label8.Visible = true;
                    label2.Text = Convert.ToString(sector.DentroSectorUno(lista));
                    label7.Text = Convert.ToString(sector.DentroSectorDos(lista));
                    label8.Text = Convert.ToString(sector.DentroSectorTres(lista));
                    sectoresCargados = true;
                    panel1.Invalidate();
                }
            }
            else
            {
                MessageBox.Show("¡Los sectores ya están cargados!");
            }
        }

        private void salirToolStripMenuItem1_Click(object sender, EventArgs e) //Salir del programa principal e indicar cuanto tiempo ha durado la sesión
        {
            timer2.Stop();
            double tiempoMinutos = this.segundosTotales / 60;
            double tiempoSegundos = this.segundosTotales % 60;
            if (this.segundosTotales > 60)
            {
                if (tiempoMinutos > 1)
                {
                    MessageBox.Show("¡Hasta la próxima! :) \nDuración de la sesión: " + tiempoMinutos + " minutos y " + tiempoSegundos + " segundos.");
                }
                else
                {
                    MessageBox.Show("¡Hasta la próxima! :) \nDuración de la sesión: " + tiempoMinutos + " minuto y " + tiempoSegundos + " segundos.");
                }
            }
            else
            {
                MessageBox.Show("¡Hasta la próxima! :) \nDuración de la sesión: " + this.segundosTotales + " segundos.");
            }
            this.Close();
        }

        private void eliminarUnAviónToolStripMenuItem_Click(object sender, EventArgs e) //Eliminar un avión
        {
            if (lista.GetCargaCorrecta() == true)
            {
                EliminarAvion eliminaravion = new EliminarAvion();
                eliminaravion.SetData(this.lista);
                eliminaravion.ShowDialog();
                if (lista.AvionEncontrado() == true)
                {
                    MessageBox.Show(Convert.ToString("Avión eliminado. Ahora hay: " + lista.GetNumAviones()) + ".");
                    lista.BorrarPila();
                    int cont = lista.GetNumAvionEliminado();
                    panel1.Controls.Remove(this.avion_picture_vector[cont]);
                    while (cont < MAX - 1)
                    {
                        this.avion_picture_vector[cont] = this.avion_picture_vector[cont + 1];
                        cont++;
                    }
                }
                if (eliminaravion.GetConfirmarElimiado() == true && lista.AvionEncontrado() == false)
                {
                    MessageBox.Show("No existe ningún avión con este identificador.");
                }
                label2.Text = Convert.ToString(sector.DentroSectorUno(lista));
                label7.Text = Convert.ToString(sector.DentroSectorDos(lista));
                label8.Text = Convert.ToString(sector.DentroSectorTres(lista));
                panel1.Invalidate();
            }
            else
            {
                MessageBox.Show("Actualmente no hay ningún avión que eliminar.");
            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e) //Listar los aviones que hay dentro del sector en el que se haga click
        {
            if (lista.GetCargaCorrecta() && sector.GetCargaCorrecta())
            {
                Point point = new Point(e.X, e.Y);
                if (Convert.ToInt32(e.X) >= sector.GetListaSectores()[0].GetBottomLeftX() && Convert.ToInt32(e.X) <= sector.GetListaSectores()[0].GetTopRightX()
                    && Convert.ToInt32(e.Y) >= sector.GetListaSectores()[0].GetBottomLeftY() && Convert.ToInt32(e.Y) <= sector.GetListaSectores()[0].GetTopRightY())
                {
                    sector.SetSectorPinchado(1);
                    if (sector.DentroSectorUno(lista) > 0)
                    {                        
                        avionensector.SetData(this.lista, this.sector);
                        avionensector.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("No hay ningún avión en este sector.");
                    }
                }
                if (Convert.ToInt32(e.X) >= sector.GetListaSectores()[1].GetBottomLeftX() && Convert.ToInt32(e.X) <= sector.GetListaSectores()[1].GetTopRightX()
                    && Convert.ToInt32(e.Y) >= sector.GetListaSectores()[1].GetBottomLeftY() && Convert.ToInt32(e.Y) <= sector.GetListaSectores()[1].GetTopRightY())
                {
                    sector.SetSectorPinchado(2);
                    if (sector.DentroSectorDos(lista) > 0)
                    {
                        avionensector.SetData(this.lista, this.sector);
                        avionensector.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("No hay ningún avión en este sector.");
                    }
                }
                if (Convert.ToInt32(e.X) >= sector.GetListaSectores()[2].GetBottomLeftX() && Convert.ToInt32(e.X) <= sector.GetListaSectores()[2].GetTopRightX()
                    && Convert.ToInt32(e.Y) >= sector.GetListaSectores()[2].GetBottomLeftY() && Convert.ToInt32(e.Y) <= sector.GetListaSectores()[2].GetTopRightY())
                {
                    sector.SetSectorPinchado(3);
                    if (sector.DentroSectorTres(lista) > 0)
                    {
                        avionensector.SetData(this.lista, this.sector);
                        avionensector.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("No hay ningún avión en este sector.");
                    }
                }
            }
            panel1.Invalidate();
        }

        private void button4_Click(object sender, EventArgs e) //Simulacion Automática
        {
            AutoSimulacion auto = new AutoSimulacion();
            if (lista.GetCargaCorrecta() == true)
            {
                auto.ShowDialog();
                if (auto.GetSimulacionActiva() == true)
                {
                    this.segundos = 0;
                    this.ContadorAutoSimulacion = auto.GetTiempoAutoSimulacion();

                    for (int i = 0; i < this.lista.GetNumAviones(); i++)
                    {
                        lista.GetAvion(i).SetTiempo(auto.GetCicloAutoSimulacion());
                    }
                    timer1.Interval = 1000;
                    timer1.Start();
                    label4.Visible = true;
                    label4.Text = Convert.ToString(this.segundos);
                }
            }
            else
            {
                MessageBox.Show("Por favor, primero carga los ficheros.");
            }
        }

        private void timer1_Tick(object sender, EventArgs e) //Timer para la simulación automática
        {
            lista.Mover();
            for (int i = 0; i < this.lista.GetNumAviones(); i++)
            {
                avion_picture_vector[i].Location = new Point(lista.GetAvion(i).GetPosicionX(), lista.GetAvion(i).GetPosicionY());
            }
            label2.Text = Convert.ToString(sector.DentroSectorUno(lista));
            this.segundos++;
            label4.Text = Convert.ToString(this.segundos);
            label2.Text = Convert.ToString(sector.DentroSectorUno(lista));
            label7.Text = Convert.ToString(sector.DentroSectorDos(lista));
            label8.Text = Convert.ToString(sector.DentroSectorTres(lista));
            if (this.segundos == this.ContadorAutoSimulacion)
            {
                timer1.Stop();
                label4.Visible = false;
                panel1.Invalidate();
                MessageBox.Show("Simulación de duración " + this.segundos + " segundos completada.");
            }
            panel1.Invalidate();
        }

        private void button5_Click(object sender, EventArgs e) //Mostrar los botones del form principal
        {
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
            button5.Visible = false;
            button6.Visible = true;
            button7.Visible = true;
            if (RutaTransparente == false)
            {
                button8.Visible = true;
            }
            label1.Visible = true;
            textBox1.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e) //Ocultar los botones del form principal
        {
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = true;
            button6.Visible = false;
            button7.Visible = false;
            button8.Visible = false;
            label1.Visible = false;
            textBox1.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e) //Mostrar la ruta de los aviones
        {
            this.RutaTransparente = false;
            button7.Visible = false;
            button8.Visible = true;
            panel1.Invalidate();
        }

        private void button8_Click(object sender, EventArgs e) //Ocultar la ruta de los aviones
        {
            this.RutaTransparente = true;
            button7.Visible = true;
            button8.Visible = false;
            panel1.Invalidate();
        }

        private void timer2_Tick(object sender, EventArgs e) //Timer para reloj y fecha en tiempo real y duración de la sesión de uso del software
        {
            label9.Text = DateTime.Now.ToLongTimeString();
            label10.Text = DateTime.Now.ToShortDateString();
            label11.Text = DateTime.Now.ToString("dddd");
            this.segundosTotales++;
        }

        private void avionesEnElEspacioAéreoToolStripMenuItem_Click(object sender, EventArgs e) //Listar todos los aviones que hay en el espacio aéreo
        {
            if (lista.GetCargaCorrecta())
            {
                avionEnEspacio.SetData(this.lista);
                avionEnEspacio.ShowDialog();
            }
            else
            {
                MessageBox.Show("Para poder usar esta opción, primero debes cargar el archivo con los datos de los aviones.");
            }
        }

        private void datosDeLasCompañíasToolStripMenuItem_Click(object sender, EventArgs e) //Abrir la base de datos de las compañías
        {
            basedatos.ShowDialog();
        }

        private void gestionarUsuariosToolStripMenuItem_Click(object sender, EventArgs e) //Listar en un data grid todos los usuarios registrados
        {
            if (inicio.GetPoderUsuario() == 1)
            {
                this.consultaUsuarios.ShowDialog();
            }
            else
            {
                MessageBox.Show("Acceso denegado. No tienes permisos suficientes.");
            }
        }

        private void acercaDeSimuladorDeTráficoAéreoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AcercaDe acerca = new AcercaDe();
            acerca.ShowDialog();
        }
    }
}