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

     

    }
}
