using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PropertyManager.ResponseModels;
using PropertyManager.ViewModels.Application.Common;
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

        protected FilterDto GetFilterDto()
        {
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var orderColumn = Request.Form["order[0][column]"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + orderColumn + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var skip = start != null ? Convert.ToInt32(start) : 0;
            var pageSize = length != null ? Convert.ToInt32(length) : 0;

            if (sortColumn == "LandlordId")
            {
                sortColumn = "LastName";
            }

            var result = new FilterDto()
            {
                Skip = skip,
                PageSize = pageSize,
                SortColumn = sortColumn,
                SortDirection = sortColumnDirection,
                SearchValue = searchValue
            };
            return result;
        }
    }
}
