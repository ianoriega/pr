using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ConexionDB
    {
        public SqlConnection Conexion { get; set; }
        public SqlCommand Comand { get; set; }

        public SqlDataReader Reader { get; set; }

        public ConexionDB(string direccionConexion)
        {
            Conexion = new SqlConnection(direccionConexion);
            Comand = new SqlCommand();
            Comand.Connection = Conexion;
            Comand.CommandType = CommandType.Text;
        }


        /// <summary>
        /// Permite enviar una consulta a la base de datos y leer la informacion las columnas 
        /// </summary>
        /// <param name="consulta">Recibe la consulta a la DB</param>
        /// <param name="columnas">recibe el numero de columnas a leer</param>
        /// <returns>Una lista con la lectura de las filas</returns>
        public List<string> LeerColumnasDeDB(string consulta, int columnas)
        {
            List<string> auxLectura = new List<string>();

            Comand.CommandText = consulta;

            ComprobarConexion();

            Reader = Comand.ExecuteReader();

            while (Reader.Read()) 
            {
                for (int i = 0; i < columnas; i++)
                {
                    auxLectura.Add(Reader[i].ToString()); 
                }                
            }

            Reader.Close();

            return auxLectura;
        }

        /// <summary>
        /// Permite enviar una consulta a la base de datos y leer la informacion de una columna
        /// </summary>
        /// <param name="consulta">Recibe la consula a la DB</param>
        /// <param name="columna">Recibe el nro de columna a leer</param>
        /// <returns></returns>
        public List<string> LeerUnaColumnaDeDB(string consulta, int columna)
        {
            List<string> auxLectura = new List<string>();

            Comand.CommandText = consulta;

            ComprobarConexion();

            Reader = Comand.ExecuteReader();

            while (Reader.Read())
            {
                auxLectura.Add(Reader[columna].ToString());
            }

            Reader.Close();

            return auxLectura;
        }

        /// <summary>
        /// Permite comprobar si la conexion esta abierta, caso contrario la abre
        /// </summary>
        public void ComprobarConexion()
        {
            if (Conexion.State != ConnectionState.Open) Conexion.Open();
        }

        /// <summary>
        /// Permite cerrar la conexion
        /// </summary>
        public void CerrarConexion()
        {
            Conexion.Close();
        }

        public void LimpiarParametros()
        {
            Comand.Parameters.Clear();
        }
    }
}
