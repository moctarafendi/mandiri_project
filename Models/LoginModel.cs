using mandiri_project.RequestResponseModels.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using NuGet.Common;
using NuGet.Protocol.Plugins;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;

namespace mandiri_project.Models
{
    public class LoginData
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    [IgnoreAntiforgeryToken(Order = 1001)]
    public class LoginModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public LoginModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [BindProperty]
        public LoginData InputData { get; set; }

        public LoginData SubmittedData { get; set; }

        public async Task OnGet()
        {
            var token=  HttpContext.Session.GetString("Token");
            // Specify the API endpoint

            var apiUrl = "https://localhost:7184/tes";

            try
            {
                // Make the API call

                // Set the authorization header
               // _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");

                var response = await _httpClient.PostAsync(apiUrl, new StringContent("{}", Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var successBody = await response.Content.ReadAsStringAsync();
                    //var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(successBody);
                    //   HttpContext.Session.SetString("Token", loginResponse.Token);
                    //Response.Cookies.Append("Token", loginResponse.Token);

                    //    return RedirectToAction("Login", "Account");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    RedirectToPage("/Account/Index");
                }


             //   return RedirectToAction("Login");
            }
            catch (HttpRequestException ex)
            {
                // Handle exception (e.g., network error)
                ///   ApiResponse = $"Error: {ex.Message}";
             //   return RedirectToAction("Login");
            }
        }

        public async Task<ActionResult> OnPost()
        {
            SubmittedData = InputData;
            // Specify the API endpoint
            var apiUrl = "https://localhost:7184/token";

            try
            {
                // Make the API call

                var response = await _httpClient.PostAsync(apiUrl, new StringContent("{}", Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var successBody = await response.Content.ReadAsStringAsync();
                    var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(successBody);
                    HttpContext.Session.SetString("Token", loginResponse.Token);
                    //Response.Cookies.Append("Token", loginResponse.Token);

                    //  return RedirectToAction("Login", "Account");
                }


                return RedirectToAction("tes", "Home");
            }
            catch (HttpRequestException ex)
            {
                // Handle exception (e.g., network error)
                ///   ApiResponse = $"Error: {ex.Message}";
                return RedirectToAction("/");
            }
        }

    }
}
