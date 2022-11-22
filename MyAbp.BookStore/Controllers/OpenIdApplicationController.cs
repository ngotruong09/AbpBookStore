using Microsoft.AspNetCore.Mvc;
using MyAbp.BookStore.Services.OpenIdApplications;
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

        [HttpPost]
        [Route("create")]
        public Task CreateAsync(CreateApplicationInput input)
        {
            return _openIdApplicationAppService.CreateAsync(input);
        }
    }
}
