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
        public IActionResult Cargar(ML.Result result)
        {
            var file = Request.Form.Files["Excel"];

            if (HttpContext.Session.GetString("pathExcel") == null)
            {
                if (file != null)
                {

                    string extensionArchivo = Path.GetExtension(file.FileName).ToLower();
                    string extensionValida = _configuration["AppSettings:TipoExcel"];
                    string a ;
                    if (extensionArchivo == extensionValida)
                    {
                        var rutaproyecto = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Carga");
                        var filePath = Path.Combine(rutaproyecto, $"{Path.GetFileNameWithoutExtension(file.FileName)}-{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx");

                        if (!System.IO.File.Exists(filePath))
                        {
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }
                            var connectionString = _configuration.GetConnectionString("OleDbConnection") + filePath;
                            ML.Result resultUsuarios = BL.Usuario.LeerExcel(connectionString);
                            //string connectionString = ConfigurationManager.ConnectionStrings["OleDbConnection"] + filePath;

                            if (resultUsuarios.Correct)
                            {
                                ML.Result resultValidacion = BL.Usuario.ValidarExcel(resultUsuarios.Objects);

                                if (resultValidacion.Objects.Count == 0)
                                {
                                    resultValidacion.Correct = true;
                                    HttpContext.Session.SetString("pathExcel", filePath);
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
                    ViewBag.Message = "No selecciono ningun archivo, Seleccione uno correctamente";
                }
                return View(result);
            }
            else
            {
                string filepath = HttpContext.Session.GetString("pathExcel");

                if (filepath != null)
                {
                    //var connectioString = connectionStrings.OleDbConnection + filepath;
                    var connectionString = _configuration.GetConnectionString("OleDbConnection") + filepath;
                    //string connectionString = ConfigurationManager.ConnectionStrings["OleDbConnection"] + filepath;
                    ML.Result resultUsuarios = BL.Usuario.LeerExcel(connectionString);

                    if (resultUsuarios.Correct)
                    {
                        ML.Result resultErrores = new ML.Result();//Instanciar antes de entrar al flujo 
                        resultErrores.Objects = new List<object>();

                        foreach (ML.Usuario usuario in resultUsuarios.Objects)
                        {
                            ML.Result result1 = BL.Usuario.Add(usuario);
                            if (!result1.Correct)
                            {
                                string error = "Ocurrio un problema al insertar los datos: " + usuario.Rfc + " con este error" + resultErrores.ErrorMessage;
                                resultErrores.Objects.Add(error);
                            }
                            HttpContext.Session.Remove("pathExcel");
                        }
                        if (resultErrores.Objects.Count > 0)
                        {
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "logErrores");
                            var filePath = Path.Combine(path, $"{DateTime.Now.ToString("yyyyMMddHHmmss")}.txt");

                            //string path = Server.MapPath(@"~\Files\logErrores");
                            //string filePath = path + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";

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
