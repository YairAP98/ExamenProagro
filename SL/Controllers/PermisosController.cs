using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("permisos/")]
    public class PermisosController : Controller
    {
        [HttpGet]
        [Route("GetAllPermiso")]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Permisos.GetAll();

            if (result.Correct)
            {
                return Ok(result);
                //  return StatusCode(200,result);

            }
            else
            {
                return NotFound();
            }


        }

        [HttpDelete]
        [Route("DeletePermiso/{IdPermiso}")]
        public ActionResult Delete(int IdPermiso)
        {
            ML.Result result = BL.Permisos.Delete(IdPermiso);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetByIdPermiso/{IdPermiso}")]
        public IActionResult GetById(int IdPermiso)
        {
            ML.Result result = BL.Permisos.GetById(IdPermiso);

            if (result.Correct)
            {
                return Ok(result);
            }
            else if (result.ErrorMessage == "Not Found") // Puedes personalizar el mensaje de error según tus necesidades.
            {
                return NotFound();
            }
            else
            {
                return StatusCode(400, result);
            }
        }

        [HttpPost]
        [Route("")]
        public IActionResult Add([FromBody] ML.Permisos permiso)
        {
            var result = BL.Permisos.Add(permiso);
            if (result.Correct)
            {
                return StatusCode(200, result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut]
        [Route("{IdUsuario}")]
        public IActionResult Update(int IdUsuario, [FromBody] ML.Permisos permiso)
        {
            var result = BL.Permisos.Update(permiso);
            if (result.Correct)
            {
                return StatusCode(200, result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
