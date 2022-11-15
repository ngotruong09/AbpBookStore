using Volo.Abp.Application.Services;

namespace MyAbp.BookStore.Services.OpenIdApplications
{
    public interface IOpenIdApplicationAppService: IApplicationService
    {
        Task CreateAsync();
    }
}
