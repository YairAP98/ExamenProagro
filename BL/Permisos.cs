using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Permisos
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenProagroContext context = new DL.ExamenProagroContext())
                {
                    var query = context.Permisos.FromSqlRaw("GetAllPermiso").ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var row in query)
                        {
                            ML.Permisos permiso = new ML.Permisos();
                            permiso.Usuario = new ML.Usuario();
                            permiso.Usuario.IdUsuario =(int) row.IdUsuario;
                            permiso.Estado = new ML.Estado();
                            permiso.Estado.IdEstado = (int) row.IdEstado;

                            result.Objects.Add(permiso);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros de Permisos";
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
        public static ML.Result GetById(int IdPerimiso)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenProagroContext context = new DL.ExamenProagroContext())
                {
                    var query = context.Permisos.FromSqlRaw($"GetByIdPermiso '{IdPerimiso}'").AsEnumerable().SingleOrDefault();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        ML.Permisos permiso = new ML.Permisos();
                        permiso.Usuario = new ML.Usuario();
                        permiso.Estado = new ML.Estado();
                        permiso.Usuario.IdUsuario = (int)query.IdUsuario;
                        permiso.Estado.IdEstado = (int)query.IdEstado;
                     
                        result.Object = permiso;
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


        public static ML.Result Add(ML.Permisos permiso)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenProagroContext context = new DL.ExamenProagroContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AddPermisos {permiso.Usuario.IdUsuario},{permiso.Estado.IdEstado}");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error no se agrego el permiso";
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
        public static ML.Result Update(ML.Permisos permiso)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenProagroContext context = new DL.ExamenProagroContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UpdatePermiso {permiso.Usuario.IdUsuario},{permiso.Estado.IdEstado}");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error no se actualizo el permiso";
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

        public static ML.Result Delete(int IdPermiso)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenProagroContext context = new DL.ExamenProagroContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"DeletePermisos '{IdPermiso}'");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo eliminar el permiso";
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


    }
}
