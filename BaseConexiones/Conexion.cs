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
                cn.Open();
                Console.WriteLine("Conectado");
            }
            catch (Exception ex)
            {
                Console.WriteLine("No se conectó: "+ex.ToString());
            }
        }



        public string insertar(int idCliente, string nombres, string apellidos, string cedula)
        {
            string salida = "Se inserto";
            try
            {
                try
                {
                    Cmd = new SqlCommand(" Insert into Clientes " + "(idCliente,nombres,apellidos,cedula) values (@idCliente,@nombres,@apellidos,@cedula)", cn);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Formato no valido: " + ex.ToString);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Comando Insertar Incorrecto: "+ ex.ToString());
                }
                try
                {
                    cmd.Parameters.Add("@idCliente", SqlDbType.Int);
                    cmd.Parameters["@idCliente"].Value = idCliente;
                    cmd.Parameters.AddWithValue("@nombres", nombres);
                    cmd.Parameters.AddWithValue("@apellidos", apellidos);
                    cmd.Parameters.AddWithValue("@cedula", cedula);
                    cmd.ExecuteNonQuery();
                }catch(Exception ex)
                {
                    Console.WriteLine("Intercambio incorrecto en el Insert " + ex.ToString());
                }
            }
            catch (DBConcurrencyException ex)
            {
                Console.WriteLine("Error de concurrencia: " + ex.ToString());
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine("No puede generar: " + ex.ToString);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Formato no valido: " + ex.ToString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Algo fallo: " + ex.ToString());
            }
                return salida;
        }
        public string consultar() 
        {
            string consulta = "Se consulto";
            try
            {
                
                cmd = new SqlCommand("select * from Clientes", cn);
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
            catch (DBConcurrencyException ex)
            {
                Console.WriteLine("Error de concurrencia: " + ex.ToString());
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine("No puede generar: " + ex.ToString);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Formato no valido: " + ex.ToString);
            }
            catch (Exception ex)
            {
                consulta = "No se inserto: " + ex.ToString();
                Console.WriteLine(consulta);
            }
            return consulta;
        }

        public string consultar_id(int id)
        {
            string consulta = "Se consulto";
            try
            {

                cmd = new SqlCommand("select * from Clientes where idCliente =  @idCliente", cn);
                cmd.Parameters.Add("@idCliente", SqlDbType.Int);
                cmd.Parameters["@idCliente"].Value = id;
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
            catch (DBConcurrencyException ex)
            {
                Console.WriteLine("Error de concurrencia: " + ex.ToString());
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine("No puede generar: " + ex.ToString);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Formato no valido: " + ex.ToString);
            }
            catch (Exception ex)
            {
                consulta = "No se inserto: " + ex.ToString();
                Console.WriteLine(consulta);
            }
            return consulta;
        }

        public string Eliminar(int id)
        {
            string eliminar = "Se Elimino";
            try
            {

                try
                {
                    cmd = new SqlCommand("Delete Clientes where idCliente =  @idCliente", cn);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Formato no valido: " + ex.ToString);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Linea de comando incorrecto" + ex.ToString);
                }
                try
                {
                    cmd.Parameters.Add("@idCliente", SqlDbType.Int);
                    cmd.Parameters["@idCliente"].Value = id;
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Mal asignación de variables" + ex.ToString);
                }
                cmd.ExecuteNonQuery();
                

            }
            catch (Exception ex)
            {
                Console.WriteLine("Algo fallo: " + ex.ToString);
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
                try
                {
                    cmd = new SqlCommand("UPDATE Clientes SET nombre = @nombres Where idCliente = idCliente", cn);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Linea de sentencia incorrecto: "+ex.ToString);
                }
                try
                {
                    cmd.Parameters.Add("@idCliente", SqlDbType.Int);
                    cmd.Parameters["@idCliente"].Value = id;
                    cmd.Parameters.AddWithValue("@nombres", nombre);
                    cmd.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Fallo en la asignación de las variables: " + ex.ToString);
                }
                
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Algo fallo: " + ex.ToString);
            }
            return actualizarSql;
        }

    }
}

