using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace ProyectoLib
{
    public class GestionUsersDB
    {
        OleDbConnection cnx;
        string providerStr = "Provider=Microsoft.SQLSERVER.CE.OLEDB.3.5;";
        string cnxStr;

        public GestionUsersDB(string dbFileName)
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

        public void closeDB() //Cerra la conexión con la base de datos
        {
            if (cnx != null)
            {
                cnx.Close();
                cnx = null;
            }
        }

        public DataTable GetAll() //Obtener todos los datos de la compañías
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM Usuarios";
            OleDbDataAdapter adp = new OleDbDataAdapter(query, cnx);
            adp.Fill(dt);
            return dt;
        }

        public int GetPoder(string usuario)
        {
            DataTable dt = new DataTable();
            string query = "SELECT Poder FROM Usuarios WHERE Usuario='" + usuario + "'";
            OleDbDataAdapter adp = new OleDbDataAdapter(query, cnx);
            adp.Fill(dt);
            return Convert.ToInt32(dt.Rows[0]["poder"]);            
        }

        public int ConsultarUsuario(string usuario)
        {
            DataTable dt = new DataTable();
            string query = "SELECT Usuario FROM Usuarios WHERE Usuario='" + usuario + "'";
            OleDbDataAdapter adp = new OleDbDataAdapter(query, cnx);
            adp.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int ConsultarContraseña(string usuario)
        {
            DataTable dt = new DataTable();
            string query = "SELECT Contraseña FROM Usuarios WHERE Contraseña='" + usuario + "'";
            OleDbDataAdapter adp = new OleDbDataAdapter(query, cnx);
            adp.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public void NuevoUsuario(string usuario, string contraseña)
        {
            string query = "INSERT INTO Usuarios VALUES ('" + usuario + "', '" + contraseña + "', 0)";
            OleDbCommand command = new OleDbCommand(query, cnx);
            command.ExecuteNonQuery();
        }

        public void EliminarUsuario(string usuario)
        {
            string query = "DELETE FROM Usuarios WHERE Usuario='" + usuario + "'";
            OleDbCommand command = new OleDbCommand(query, cnx);
            command.ExecuteNonQuery();
        }
    }
}
