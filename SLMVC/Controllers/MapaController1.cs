using Microsoft.AspNetCore.Mvc;

namespace PLMVC.Controllers
{
    public class MapaController1 : Controller
    {
        [HttpGet]
        public IActionResult Mapa()
        {
            ML.GeoReferencia geo = new ML.GeoReferencia();
            geo.GeoReferencias = new List<object>();
            ML.Result result = new ML.Result();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5011/GeoReferencia/");
                var responseTask = client.GetAsync("GetAllGeo");
                responseTask.Wait();
                var resultServicio = responseTask.Result;
                if (resultServicio.IsSuccessStatusCode)
                {
                    var readTask = resultServicio.Content.ReadFromJsonAsync<ML.Result>();
                    readTask.Wait();
                    foreach (var resultGeo in readTask.Result.Objects)
                    {
                        ML.GeoReferencia resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.GeoReferencia>(resultGeo.ToString());
                        geo.GeoReferencias.Add(resultItemList);
                    }
                }
            }

            return View(geo);
        }
    }
}
