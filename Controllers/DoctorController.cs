using ClinicQueueFrontend.Models;
using Frontend_Exam.Models;
using Frontend_Exam.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Frontend_Exam.Controllers
{
    public class DoctorController : Controller
    {

        #region ConfigurationField
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;
        public DoctorController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://cmsback.sampaarsh.cloud/");
        }
        #endregion

        #region AddToken
        private void AddToken(HttpRequestMessage request)
        {
            string token = CommonVariables.Token();
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }
        #endregion

        #region Get
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "doctor/queue");
            AddToken(request);

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                var queue = JsonConvert.DeserializeObject<List<DoctorQueueModel>>(result);

                return View(queue);
            }

            TempData["Error"] = "Failed to load users";
            return View(new List<DoctorQueueModel>());

            return View();
        }
        #endregion

        #region AddPrescription
        [HttpGet]
        public async Task<IActionResult> AddPrescription(int appointmentId)
        {
            var model = new AddPrescriptionModel { appointmentId = appointmentId };

            model.medicines.Add(new Models.PrescriptionMedicine());
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPrescription(AddPrescriptionModel model)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"prescriptions/{model.appointmentId}");
            AddToken(request);
            var body = new
            {
                medicines = model.medicines.Where(m => !string.IsNullOrEmpty(m.name)).ToArray(),
                notes = model.notes
            };

            var json = JsonConvert.SerializeObject(body);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var errorResult = await response.Content.ReadAsStringAsync();
            try
            {
                var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(errorResult);
                ViewBag.msg = dict.ContainsKey("error") ? dict["error"] : errorResult;
            }
            catch
            {
                ViewBag.msg = errorResult;
            }
            return View(model);
        }
        #endregion

        #region AddReport
        [HttpGet]
        public IActionResult AddReport(int appointmentId)
        {
            var model = new AddReportModel { appointmentId = appointmentId };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddReport(AddReportModel model)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"reports/{model.appointmentId}");
            AddToken(request);
            var body = new
            {
                diagnosis = model.diagnosis,
                testRecommended = model.testRecommended,
                remarks = model.remarks
            };

            var json = JsonConvert.SerializeObject(body);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var errorResult = await response.Content.ReadAsStringAsync();
            try
            {
                var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(errorResult);
                ViewBag.msg = dict.ContainsKey("error") ? dict["error"] : errorResult;
            }
            catch
            {
                ViewBag.msg = errorResult;
            }
            return View(model);
        }
        #endregion
    }
}
