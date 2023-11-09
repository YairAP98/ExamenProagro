using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Usuario
    {


        //GETALL
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenProagroContext context = new DL.ExamenProagroContext())
                {
                    var query = context.Usuarios.FromSqlRaw("GetAllUsuario").ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var row in query)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = row.IdUsuario;
                            usuario.Nombre = row.Nombre;
                            usuario.Contrasena = row.Contrasena;
                            usuario.Creacion = row.Creacion.Value.ToString("dd-mm-yyyy");
                            usuario.Rfc = row.Rfc;
                          
                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros de cines";
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenProagroContext context = new DL.ExamenProagroContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"GetByIdUsuario '{IdUsuario}'").AsEnumerable().SingleOrDefault();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Nombre = query.Nombre;
                        usuario.Contrasena = query.Contrasena;
                        usuario.Creacion = query.Creacion.Value.ToString("dd-mm-yyyy");
                        usuario.Rfc = query.Rfc;
                      
                        result.Object = usuario;
                        result.Correct = true;
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;

        }


        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenProagroContext context = new DL.ExamenProagroContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AddUsuario '{usuario.Contrasena}','{usuario.Nombre}','{usuario.Creacion}','{usuario.Rfc}'");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error no se agrego el usuario";
                    }
                    result.Correct = true;

                }
            }
            catch (Exception ex)
            {
                result.Correct = true;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;

        }
        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenProagroContext context = new DL.ExamenProagroContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioUpdate {usuario.IdUsuario},'{usuario.Contrasena}','{usuario.Nombre}','{usuario.Creacion}','{usuario.Rfc}'");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error no se actualizo el cine";
                    }
                    result.Correct = true;

                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenProagroContext context = new DL.ExamenProagroContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"DeleteUsuario '{IdUsuario}'");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo eliminar el usuario";
                    }
                    result.Correct = true;

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        //        /GETBYID


    }
}