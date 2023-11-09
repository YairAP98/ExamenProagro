using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estado
    {

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenProagroContext context = new DL.ExamenProagroContext())
                {
                    var query = context.Usuarios.FromSqlRaw("GetAllUsuarios").ToList();
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
        public static ML.Result GetById(int IdEstado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenProagroContext context = new DL.ExamenProagroContext())
                {
                    var query = context.Estados.FromSqlRaw($"GetByIdEstado {IdEstado}").AsEnumerable().SingleOrDefault();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        ML.Estado estado = new ML.Estado();
                        estado.IdEstado = query.IdEstado;
                        estado.Nombre = query.Nombre;
                        estado.Siglas = query.Siglas;

                        result.Object = estado;
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
      

    }
}
