using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("GeoReferencia/")]
    public class GeoController : Controller
    {
     
      
            [HttpGet]
            [Route("GetAllGeo")]
            public ActionResult GetAll()
            {
                ML.Result result = BL.GeoReferencia.GetAll();

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
            [Route("DeletePermiso/{IdGeo}")]
            public ActionResult Delete(int IdGeo)
            {
                ML.Result result = BL.GeoReferencia.Delete(IdGeo);

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
            [Route("GetById/{IdGeo}")]
            public IActionResult GetById(int IdGeo)
            {
                ML.Result result = BL.GeoReferencia.GetById(IdGeo);

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
            public IActionResult Add([FromBody] ML.GeoReferencia geo)
            {
                var result = BL.GeoReferencia.Add(geo);
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
            [Route("{IdGeo}")]
            public IActionResult Update(int IdGeo, [FromBody] ML.GeoReferencia geo)
            {
                var result = BL.GeoReferencia.Update(geo);
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
