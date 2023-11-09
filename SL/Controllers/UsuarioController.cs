using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        [Route("GetAll")]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Usuario.GetAll();

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
        [Route("Delete/{IdUsuario}")]
        public ActionResult Delete(int IdUsuario)
        {
            ML.Result result = BL.Usuario.Delete(IdUsuario);

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
        [Route("GetById/{IdUsuario}")]
        public IActionResult GetById(int IdUsuario)
        {
            ML.Result result = BL.Usuario.GetById(IdUsuario);

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
        public IActionResult Add([FromBody] ML.Usuario usuario)
        {
            var result = BL.Usuario.Add(usuario);
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
        public IActionResult Update(int IdUsuario, [FromBody] ML.Usuario usuario)
        {
            var result = BL.Usuario.Update(usuario);
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
