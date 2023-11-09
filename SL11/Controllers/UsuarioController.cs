using Microsoft.AspNetCore.Mvc;

namespace SL11.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
