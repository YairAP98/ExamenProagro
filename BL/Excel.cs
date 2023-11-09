using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Excel
    {
        public static ML.Result LeerExcel(string connectionString)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (OleDbConnection context = new OleDbConnection(connectionString))
                {
                    string query = "SELECT * FROM [CAT_USUARIO]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;

                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;


                        DataTable tableUsuario = new DataTable();

                        da.Fill(tableUsuario);

                        if (tableUsuario.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow row in tableUsuario.Rows)
                            {
                                ML.Usuario usuario = new ML.Usuario();
                                usuario.IdUsuario =int.Parse(row[0].ToString());
                                usuario.Contrasena = row[1].ToString();
                                usuario.Nombre = row[2].ToString();
                                usuario.Creacion = row[3].ToString();
                                usuario.Rfc = row[4].ToString();
                           
                                result.Objects.Add(usuario);
                            }
                            result.Correct = true;

                        }

                        result.Object = tableUsuario;

                        if (tableUsuario.Rows.Count > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en el excel";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        }
        public static ML.Result ValidarExcel(List<object> usuarios)
        {
            ML.Result result = new ML.Result();

            try
            {
                result.Objects = new List<object>(); //almacena los registros incompletos
                int i = 1; //guarda el numero de la fila
                foreach (ML.Usuario usuario in usuarios)
                {
                    ML.ErrorExcel error = new ML.ErrorExcel();
                    error.IdRegistro = i++;

                    if (usuario.Nombre == "")
                    {
                        error.Usuario.Contrasena += "Ingresar el Id Usuario  ";
                    }
                    if (usuario.Nombre == "")
                    {
                        error.Mensaje += "Ingresar el Apellido paterno  ";
                    }
                    if (usuario.Creacion == "")
                    {
                        error.Mensaje += "Ingresar el Apellido materno  ";
                    }
                    if (usuario.Rfc == null)
                    {
                        error.Mensaje += "Ingresa la fecha de nacimiento ";
                    }

                    if (error.Mensaje != null)
                    {
                        result.Objects.Add(error);
                    }


                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        }
    }
}
