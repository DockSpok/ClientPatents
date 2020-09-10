using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ClientPatents.Models;

namespace ClientPatents.Controllers
{
    public class PatentsController : Controller
    {
        private readonly string apiUrl = "https://localhost:44383/api/patents";

        public async Task<IActionResult> Index()
        {
            List<DtoPatent> listPatents = new List<DtoPatent>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(apiUrl))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    listPatents = JsonConvert.DeserializeObject<List<DtoPatent>>(apiResponse);
                }
            }
            return View(listPatents);
        }

        public ViewResult GetPatent() => View();

        [HttpPost]
        public async Task<IActionResult> GetPatent(int id)
        {
            DtoPatent patent = new DtoPatent();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(apiUrl + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    patent = JsonConvert.DeserializeObject<DtoPatent>(apiResponse);
                }
            }
            return View(patent);
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePatent(int id)
        {
            DtoPatent patent = new DtoPatent();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(apiUrl + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    patent = JsonConvert.DeserializeObject<DtoPatent>(apiResponse);
                }
            }
            return View(patent);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateReserva(DtoPatent patent)
        {
            DtoPatent patentInput= new DtoPatent();
            using (var httpClient = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(patent.PatentId.ToString()), "PatentId");
                content.Add(new StringContent(patent.PatentTitle), "PatentTitle");
                content.Add(new StringContent(patent.PatentNumber), "PatentNumber");
                content.Add(new StringContent(patent.PatentClaims), "PatentClaims");
                using (var response = await httpClient.PutAsync(apiUrl, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Sucesso";
                    patentInput = JsonConvert.DeserializeObject<DtoPatent>(apiResponse);
                }
            }
            return View(patentInput);
        }
    }
}