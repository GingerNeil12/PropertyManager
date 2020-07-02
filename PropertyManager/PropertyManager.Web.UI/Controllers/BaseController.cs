using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PropertyManager.ResponseModels;
using PropertyManager.Web.UI.Common;

namespace PropertyManager.Web.UI.Controllers
{
    public class BaseController : Controller
    {
        protected HttpClient HttpClient { get; private set; }
        protected IConfiguration Configuration { get; private set; }

        protected BaseController(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            HttpClient = httpClientFactory.CreateClient(ProjectConstants.CLIENT_NAME);
            Configuration = configuration;
        }

        protected StringContent CreateContent(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return new StringContent(json, Encoding.UTF8, ProjectConstants.APPLICATION_JSON);
        }

        protected T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        protected void AddBadRequestErrorsToModelState(BadRequestApiResponse response)
        {
            foreach (var entry in response.Errors)
            {
                for (int i = 0; i < entry.Value.Length; i++)
                {
                    ModelState.AddModelError(entry.Key, entry.Value[i]);
                }
            }
        }
    }
}
