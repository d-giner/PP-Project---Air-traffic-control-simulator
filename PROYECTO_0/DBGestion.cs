using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace ProyectoLib
{
    public class DBGestion
    {
        OleDbConnection cnx;
        string providerStr = "Provider=Microsoft.SQLSERVER.CE.OLEDB.3.5;";       
        string cnxStr;

        public DBGestion(string dbFileName)
        {
            cnxStr = providerStr + "Data Source=" + dbFileName + ";Persist Security Info=False;";
        }
         
        public int openDB() //Abrir la conexión con la base de datos
        {
            try
            {
                cnx = new OleDbConnection(cnxStr);
                cnx.Open();
            }
            catch (OleDbException)
            {
                // Error abriendo la base de datos
                return -1;
            }
            return 0;
        }
        
        public DataTable getAll() //Obtener todos los datos de la compañías
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM [información compañias aereas]";
            OleDbDataAdapter adp = new OleDbDataAdapter(query, cnx);
            adp.Fill(dt);
            return dt;
        }

        public DataTable getByCompania(string c) //Obtener datos de los aviones de X compañía
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM [información compañias aereas] WHERE Nombre='" + c + "'";
            OleDbDataAdapter adp = new OleDbDataAdapter(query, cnx);
            adp.Fill(dt);
            return dt;
        }

        public int getByAñadir(string Nombre, int Teléfono, string email) //Añadir nueva compañía
        {
            DataTable res2 = getByCompania(Nombre);
            if (res2.Rows.Count != 0)
            {
                return -2;
            }
            string query = "INSERT INTO [información compañias aereas] VALUES ('" + Nombre + "', '" + Teléfono + "', '" + email + "')";
            OleDbCommand command = new OleDbCommand(query, cnx);
            int res = command.ExecuteNonQuery();
            if (res != 1)
            {
                // No records have been updated
                return -1;
            }
            else
            {
                // Success!
                return 0;
            }
        }

        public int getByEliminar(string Nombre) //Eliminar datos de la Base de Datos
        {
            string query = "DELETE [información compañias aereas] WHERE Nombre='" + Nombre + "'";
            OleDbCommand command = new OleDbCommand(query, cnx);
            int res = command.ExecuteNonQuery();
            if (res != 1)
            {
                // No records have been updated
                return -1;
            }
            else
            {
                // Success!
                return 0;
            }
        }

        public int getByModificar(string Nombre, string Nombre2, int Teléfono2, string email2) //Modificar (Actualizar), la base de datos
        {
            string query = "UPDATE [información compañias aereas] SET NomModificat='" + Nombre2 + "', TelModificat='" + Teléfono2 + "', emailModificat='" + email2 + "' WHERE Nombre='" + Nombre +"'";
            OleDbCommand command = new OleDbCommand(query, cnx);
            int resu = command.ExecuteNonQuery();
            if (resu != 1)
            {
                // No records have been updated
                return -1;
            }
            else
            {
                // Success!
                return 0;
            }
        }

        public void closeDB() //Cerra la conexión con la base de datos
        {
            if (cnx != null)
            {
                cnx.Close();
                cnx = null;
            }
        }
    }
}
