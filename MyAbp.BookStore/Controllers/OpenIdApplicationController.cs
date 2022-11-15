using Microsoft.AspNetCore.Mvc;
using MyAbp.BookStore.Services.OpenIdApplications;
using System.Xml.Linq;
using Volo.Abp;

namespace MyAbp.BookStore.Controllers
{
    [RemoteService(Name = "BookStore")]
    [Area("BookStore")]
    [ControllerName("OpenIdApplication")]
    [Route("api/openid/app")]
    public class OpenIdApplicationController : BookStoreController, IOpenIdApplicationAppService
    {
        private readonly IOpenIdApplicationAppService _openIdApplicationAppService;

        public OpenIdApplicationController(IOpenIdApplicationAppService openIdApplicationAppService)
        {
            _openIdApplicationAppService = openIdApplicationAppService;
        }

        [HttpGet]
        [Route("create")]
        public Task CreateAsync()
        {
            return _openIdApplicationAppService.CreateAsync();
        }
    }
}
