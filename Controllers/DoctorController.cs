using Frontend_Exam.Services;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace Frontend_Exam.Controllers
{
    public class DoctorController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;

        #region ConfigurationField
        public DoctorController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://cmsback.sampaarsh.cloud/appointments");
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
    }
}
