using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PLMVC.Controllers
{
    public class CargaController1 : Controller
    {
        private readonly IHttpContextAccessor _session;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CargaController1(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _session = httpContextAccessor;
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public ActionResult CargaMasiva()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            return View(result);
        }

        [HttpPost]
        public async Task<ActionResult> CargarAsync(ML.Result result)
        {
            IFormFile file = HttpContext.Request.Form.Files["Excel"];

            if (_session.HttpContext.Session.GetString("pathExcel") == null)
            {
                if (file != null)
                {
                    string extensionArchivo = Path.GetExtension(file.FileName).ToLower();
                    string extesionValida = _configuration["TipoExcel"];

                    if (extensionArchivo == extesionValida)
                    {
                        string rootPath = _hostingEnvironment.ContentRootPath;
                        string rutaproyecto = Path.Combine(rootPath, "CargaMasiva");
                        string filePath = Path.Combine(rutaproyecto, $"{Path.GetRandomFileName()}.xlsx");

                        if (!System.IO.File.Exists(filePath))
                        {
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            string connectionString = _configuration.GetConnectionString("OleDbConnection") + filePath;
                            ML.Result resultUsuarios = BL.Excel.LeerExcel(connectionString);

                            if (resultUsuarios.Correct)
                            {
                                ML.Result resultValidacion = BL.Excel.ValidarExcel(resultUsuarios.Objects);

                                if (resultValidacion.Objects.Count == 0)
                                {
                                    resultValidacion.Correct = true;
                                    _session.HttpContext.Session.SetString("pathExcel", filePath);
                                }

                                return View(resultValidacion);
                            }
                            else
                            {
                                ViewBag.Message = "El excel no contiene registros";
                            }
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Favor de seleccionar un archivo .xlsx, Verifique su archivo";
                    }
                }
                else
                {
                    ViewBag.Message = "No seleccionó ningún archivo, seleccione uno correctamente";
                }
                return View(result);
            }
            else
            {
                string filepath = _session.HttpContext.Session.GetString("pathExcel");

                if (filepath != null)
                {
                    string connectionString = _configuration.GetConnectionString("OleDbConnection") + filepath;
                    ML.Result resultUsuarios = BL.Excel.LeerExcel(connectionString);

                    if (resultUsuarios.Correct)
                    {
                        ML.Result resultErrores = new ML.Result();
                        resultErrores.Objects = new List<object>();

                        foreach (ML.Usuario usuario in resultUsuarios.Objects)
                        {
                            ML.Result result1 = BL.Usuario.Add(usuario);
                            if (!result1.Correct)
                            {
                                string error = $"Ocurrió un problema al insertar los datos: {usuario.IdUsuario} con este error {result1.ErrorMessage}";
                                resultErrores.Objects.Add(error);
                            }
                            _session.HttpContext.Session.SetString("pathExcel", null);
                        }

                        if (resultErrores.Objects.Count > 0)
                        {
                            string path = Path.Combine(_hostingEnvironment.ContentRootPath, "Files", "logErrores");
                            string filePath = Path.Combine(path, $"{DateTime.Now.ToString("yyyyMMddHHmmss")}.txt");

                            using (StreamWriter writer = new StreamWriter(filePath))
                            {
                                foreach (string linea in resultErrores.Objects)
                                {
                                    writer.WriteLine(linea);
                                }
                            }
                        }
                    }
                }
            }
            return View(result);
        }
    }
}
