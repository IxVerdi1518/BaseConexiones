using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace BaseConexiones
{
    public class Conexion
    {
        SqlConnection cn;
        SqlCommand cmd;
        public SqlConnection Cn { get => cn; set => cn = value; }
        public SqlCommand Cmd { get => cmd; set => cmd = value; }

        public Conexion()
        {
            try
            {
                cn = new SqlConnection("Data Source=DESKTOP-KBQIMBE;Initial Catalog=Clientes;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                //                      Data Source=DESKTOP-KBQIMBE;Initial Catalog=Clientes;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
                cn.Open();
                Console.WriteLine("Conectado");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }



        public string insertar(int idCliente, string nombres, string apellidos, string cedula)
        {
            string salida = "Se inserto";
            try
            {
                Cmd = new SqlCommand(" Insert into Clientes " +"(idCliente,nombres,apellidos,cedula) values (@idCliente,@nombres,@apellidos,@cedula)", cn);
                cmd.Parameters.Add("@idCliente", SqlDbType.Int);
                cmd.Parameters["@idCliente"].Value = idCliente;
                cmd.Parameters.AddWithValue("@nombres", nombres);
                cmd.Parameters.AddWithValue("@apellidos", apellidos);
                cmd.Parameters.AddWithValue("@cedula", cedula);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                salida = "No se conecto: " + ex.ToString();
                Console.WriteLine(salida);
            }
            
            return salida;
        }
        public string consultar() 
        {
            string consulta = "Se consulto";
            try
            {
                
                cmd = new SqlCommand("select * from Cliente", cn);
                cmd.ExecuteNonQuery();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write(reader.GetValue(i) + "  | ");
                        }
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception ex) 
            {
                consulta = "No se conecto: " + ex.ToString();
                Console.WriteLine(consulta);
            }
            return consulta;
        }

        public string consultar_id(int id)
        {
            string consulta = "Se consulto";
            try
            {

                cmd = new SqlCommand("select * from Cliente where IdCliente =  @IdCliente", cn);
                cmd.Parameters.Add("@IdCliente", SqlDbType.Int);
                cmd.Parameters["@IdCliente"].Value = id;
                cmd.ExecuteNonQuery();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write(reader.GetValue(i)+ "  | ");
                        }
                        Console.WriteLine(); 
                    }
                }
            }
            catch (Exception ex)
            {
                consulta = "No se conecto: " + ex.ToString();
                Console.WriteLine(consulta);
            }
            return consulta;
        }

        public string Eliminar(int id)
        {
            string eliminar = "Se Elimino";
            try
            {

                cmd = new SqlCommand("Delete Cliente where IdCliente =  @IdCliente", cn);
                cmd.Parameters.Add("@IdCliente", SqlDbType.Int);
                cmd.Parameters["@IdCliente"].Value = id;
                cmd.ExecuteNonQuery();
                

            }
            catch (Exception ex)
            {
                eliminar = "No se conecto: " + ex.ToString();
                Console.WriteLine(eliminar);
            }
            return eliminar;
        }
        public string Actualizar (int id, string nombre, string apellido, string cedula) 
        {
            string actualizar = "Se Actualizo";
            try
            {
                Eliminar(id);
                insertar(id,nombre,apellido,cedula);
                Console.WriteLine("Se actualizo Correctamente");
            }
            catch (Exception ex)
            {
                actualizar = "No se conecto: " + ex.ToString();
                Console.WriteLine(actualizar);
            }
            return actualizar;
        }
        public string Actualizar_sql(int id, string nombre)
        {
            string actualizarSql = "Se Actualizo";
            try
            {
                cmd = new SqlCommand("UPDATE Cliente SET nombre = @nombre Where IdCliente = IdCliente", cn);
                cmd.Parameters.Add("@IdCliente", SqlDbType.Int);
                cmd.Parameters["@IdCliente"].Value = id;
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) 
            {
                actualizarSql = "No se actualizo: " + ex.ToString();
                Console.WriteLine(actualizarSql);
            }
            return actualizarSql;
        }

    }
}

