using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class GeoReferencia
    {
        //getall y getbyid para mostarr en el mapa 

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenProagroContext context = new DL.ExamenProagroContext())
                {
                    var query = context.Georeferencias.FromSqlRaw("GetAllGeo").ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var row in query)
                        {
                            ML.GeoReferencia geo = new ML.GeoReferencia();
                            geo.Estado = new ML.Estado();
                            geo.IdGeoreferencia = row.IdGeoreferencia;
                            geo.Latitud =(decimal) row.Latitud;
                            geo.Longitud =(decimal)row.Longitud;
                            geo.Estado.IdEstado = (int)row.IdEstado;
                            result.Objects.Add(geo);
                            result.Correct = true;
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros de geo";
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
        public static ML.Result GetById(int IdGeo)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenProagroContext context = new DL.ExamenProagroContext())
                {
                    var query = context.Georeferencias.FromSqlRaw($"GetByIdGeo {IdGeo}").AsEnumerable().SingleOrDefault();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        ML.GeoReferencia geo = new ML.GeoReferencia();
                        geo.Estado = new ML.Estado();

                        geo.IdGeoreferencia = query.IdGeoreferencia;
                        geo.Estado.IdEstado =(int) query.IdEstado;
                        geo.Latitud =(decimal)query.Latitud;
                        geo.Longitud = (decimal)query.Longitud;
                       
                        result.Object = geo;
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
        public static ML.Result Add(ML.GeoReferencia geo)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenProagroContext context = new DL.ExamenProagroContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AddGeo {geo.IdGeoreferencia},{geo.Estado.IdEstado},{geo.Latitud},{geo.Longitud}");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error no se agrego la geo";
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
        public static ML.Result Update(ML.GeoReferencia geo)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenProagroContext context = new DL.ExamenProagroContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UpdateGeo {geo.IdGeoreferencia},{geo.Estado.IdEstado},{geo.Latitud},{geo.Longitud}");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error no se actualizo la geo ";
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
        public static ML.Result Delete(int IdGeo)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ExamenProagroContext context = new DL.ExamenProagroContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"DeleteGeo {IdGeo}");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo eliminar la geo";
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
