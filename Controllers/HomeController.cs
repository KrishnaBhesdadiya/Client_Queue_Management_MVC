using ClinicQueueFrontend.Models;
using Frontend_Exam.Models;
using Frontend_Exam.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Frontend_Exam.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;

        #region ConfigurationField
        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
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
            return View();
        }
        #endregion
        #region GetAppointments
        [HttpGet]
        public async Task<IActionResult> MyAppointments()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "appointments/my");
            AddToken(request);
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<Appointment>>(result);
                return View(list);
            }
            return View();
        }
        #endregion

        #region BookAppointment
        public IActionResult Book()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Book(BookAppointmentModel model)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "appointments");
            AddToken(request);
            var json = JsonConvert.SerializeObject(model);
             request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("MyAppointments");
            }

            var error = await response.Content.ReadAsStringAsync();
            ViewBag.msg = error;

            return View();
        }
        #endregion

        #region Details
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"appointments/{id}");
            AddToken(request);
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var appointment = JsonConvert.DeserializeObject<Appointment>(data);
                return View(appointment);
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region My Prescription
        public async Task<IActionResult> MyPrescription()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "prescriptions/my");
            AddToken(request);
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<PrescriptionModel>>(result);
                return View(list);
            }
            return View();
        }
        #endregion

        #region My Report
        public async Task<IActionResult> MyReport()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "reports/my");
            AddToken(request);
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<ReportModel>>(result);
                return View(list);
            }
            return View();
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
