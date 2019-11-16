using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ProyectoLib;
using System.Data;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            //// Create an object instance of the class DBGestion
            //DBGestion db = new DBGestion("..\\..\\..\\PROYECTO_0\\MiDatabase.sdf");
            //// Open the connection with the database
            //int res = db.openDB();
            //if (res != 0)
            //{
            //    Console.Error.WriteLine("Unable to open a connection to the database");
            //    System.Environment.Exit(-1);
            //}
            //// Show all the records in the database
            //DataTable dt = db.getAll();
            //Console.WriteLine("Records in the database:"); // Print separator.
            //foreach (DataRow row in dt.Rows) // Loop over the rows.
            //{
            //    Console.WriteLine("‐‐‐");
            //    Console.WriteLine("Nombre:  {0}", row["Nombre"]);    // or row.Field<string>(0)
            //    Console.WriteLine("Teléfono: {0}", row["Teléfono"]); // or row.Field<string>(1)
            //    Console.WriteLine("email:  {0}", row["email"]);   // or row.Field<int>(2)
            //}
            //Console.WriteLine();
            //Console.ReadKey();




            //ListaAviones llistaAvions = new ListaAviones();
            //int menu = 1;
            ////Cargar ficher aviones
            //int res1 = llistaAvions.Cargar("aviones.txt");
            //    if (res1 == 1)
            //        Console.WriteLine(" Archivo leído correctamente.");
            //    else if (res1 == 0)
            //        Console.WriteLine(" Error al leer el archivo.");

            ////Cargar fichero sectores
            //Sector sector1 = new Sector();
            //int res2 = sector1.cargarSectores("sector.txt");
            //    if (res2 == 1)
            //        Console.WriteLine(" Archivo leído correctamente.");
            //    else if (res2 == 0)
            //        Console.WriteLine(" Error al leer el archivo.");


            //    //Menú
            //    Console.WriteLine("");
            //    Console.WriteLine(" Simulador de vuelo");
            //    while (menu != 0)
            //    {
            //        Console.WriteLine("");
            //        Console.WriteLine(" Menu principal");
            //        Console.WriteLine(" 1- Mostrar posicion aviones");
            //        Console.WriteLine(" 2- Mostrar congestión sector");
            //        Console.WriteLine(" 3- Simulación");
            //        Console.WriteLine(" 4- Guardar y salir");

            //        try
            //        {
            //            Console.WriteLine("");
            //            menu = Convert.ToInt32(Console.ReadLine());
            //            if (menu > 4)
            //                Console.WriteLine("La opción no existe.");
            //        }
            //        catch (FormatException)
            //        {
            //            Console.WriteLine("No has elegido una opcion correcta.");
            //            Console.ReadKey();
            //        }
            //        switch (menu)
            //        {
            //            case 1:
            //                Console.WriteLine("");
            //                llistaAvions.imprimir();
            //                break;

            //            case 2:
            //                Console.WriteLine("");
            //                int cont = sector1.Dentro(llistaAvions);
            //                Console.WriteLine(" Hay " + cont + " aviones dentro del sector.");
            //                break;

            //            //case 3:
            //            //    Console.WriteLine("");
            //            //    sector1.Simulacion(llistaAvions);
            //            //    break;
            //        }
            //    }
        }
    }
}

