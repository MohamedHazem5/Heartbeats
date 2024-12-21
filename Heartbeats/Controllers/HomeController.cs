using Heartbeats.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace Heartbeats.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public async Task<ActionResult> Medications()
        {
            string baseUrl = "https://dailymed.nlm.nih.gov/dailymed/services/v2/spls";
            int totalItemsToFetch = 1000;
            int pageSize = 100;
            int totalPages = (int)Math.Ceiling((double)totalItemsToFetch / pageSize);
            List<string> medications = new List<string>();

            using (HttpClient client = new HttpClient())
            {
                for (int page = 1; page <= totalPages; page++)
                {
                    string apiUrl = $"{baseUrl}?page={page}&pagesize={pageSize}";

                    try
                    {
                        var response = await client.GetAsync(apiUrl);
                        if (response.IsSuccessStatusCode)
                        {
                            var jsonData = await response.Content.ReadAsStringAsync();
                            var apiResult = JsonSerializer.Deserialize<MedicationApiResult>(jsonData, new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            });
                            if (apiResult?.Data != null)
                            {
                                medications.AddRange(apiResult.Data.Select(p => p.Title));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = "Unable to fetch medications.";
                        break;
                    }
                }
            }

            // Limit to the desired total items in case the API returns more than expected
            medications = medications.Take(totalItemsToFetch).ToList();

            return View(new MedicationDto { StringMedications = string.Join("#$#", medications) });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}