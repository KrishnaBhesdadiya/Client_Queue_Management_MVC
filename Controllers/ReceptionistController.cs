using ClinicQueueFrontend.Models;
using Frontend_Exam.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Frontend_Exam.Controllers
{
    public class ReceptionistController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;

        #region ConfigurationField
        public ReceptionistController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
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
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            var request = new HttpRequestMessage(HttpMethod.Get, $"queue?date={today}");
            AddToken(request);

            var response = await _httpClient.SendAsync(request);

            List<QueueModel> list = new List<QueueModel>();

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<List<QueueModel>>(data);
            }

            return View(list);
        }
        #endregion

        #region TV Display
        public async Task<IActionResult> TvDisplay()
        {
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            var request = new HttpRequestMessage(HttpMethod.Get, $"queue?date={today}");
            AddToken(request);

            var response = await _httpClient.SendAsync(request);

            List<QueueModel> list = new List<QueueModel>();

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<List<QueueModel>>(data);
            }

            return View(list);
        }
        #endregion

        #region UpdateStatus
        public async Task<IActionResult> StatusUpdate(int id, string newStatus = "in-progress")
        {
            // 1. Create the payload object to match what the API expects
            var payload = new { status = newStatus };

            // 2. Serialize the object to JSON
            var jsonPayload = System.Text.Json.JsonSerializer.Serialize(payload);

            var request = new HttpRequestMessage(HttpMethod.Patch, $"queue/{id}")
            {
                Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
            };

            AddToken(request);

            try
            {
                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    // Success: Back to the list
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
                // Log error if needed: var error = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                // Handle network/timeout errors
                ModelState.AddModelError("", "Unable to connect to the server.");
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Done
        public async Task<IActionResult> StatusDone(int id, string newStatus = "done")
        {
            // 1. Create the payload object to match what the API expects
            var payload = new { status = newStatus };

            // 2. Serialize the object to JSON
            var jsonPayload = System.Text.Json.JsonSerializer.Serialize(payload);

            var request = new HttpRequestMessage(HttpMethod.Patch, $"queue/{id}")
            {
                Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
            };

            AddToken(request);

            try
            {
                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    // Success: Back to the list
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
                // Log error if needed: var error = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                // Handle network/timeout errors
                ModelState.AddModelError("", "Unable to connect to the server.");
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Status Skip
        public async Task<IActionResult> StatusSkip(int id, string newStatus = "skipped")
        {
            // 1. Create the payload object to match what the API expects
            var payload = new { status = newStatus };

            // 2. Serialize the object to JSON
            var jsonPayload = System.Text.Json.JsonSerializer.Serialize(payload);

            var request = new HttpRequestMessage(HttpMethod.Patch, $"queue/{id}")
            {
                Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
            };

            AddToken(request);

            try
            {
                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    // Success: Back to the list
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
                // Log error if needed: var error = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                // Handle network/timeout errors
                ModelState.AddModelError("", "Unable to connect to the server.");
            }

            return RedirectToAction("Index");
        }
        #endregion
    }
}
