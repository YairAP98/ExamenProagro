using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace PLMVC.Controllers
{
    public class LoginController1 : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        //consunnir servicios sl 
        [HttpPost]
        public IActionResult Login(int IdUsuario, string password)
        
        {

            ML.Result result = BL.Usuario.GetById(IdUsuario);

            if (result.Correct)
            {
                var data = (ML.Usuario)result.Object;

                  if (password==data.Contrasena)
                    {
                    return RedirectToAction("Index", "Home");
                    }
                    else
                    {

                    }
                
              
            }
            else
            {
                ViewBag.Login = false;
                ViewBag.Mensaje = "No existe la cuenta";
                return PartialView("modal");
            }
            return View();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Cine cine = new ML.Cine();
            cine.Cines = new List<object>();
            ML.Result result = new ML.Result();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5211/");
                var responseTask = client.GetAsync("GetAll");
                responseTask.Wait();
                var resultServicio = responseTask.Result;
                if (resultServicio.IsSuccessStatusCode)
                {
                    var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();
                    foreach (var resultCine in readTask.Result.Objects)
                    {
                        ML.Cine resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Cine>(resultCine.ToString());
                        cine.Cines.Add(resultItemList);
                    }
                }
            }

            return View(cine);
        }

    }
}
