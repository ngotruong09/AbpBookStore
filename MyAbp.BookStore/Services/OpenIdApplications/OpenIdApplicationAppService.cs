using OpenIddict.Abstractions;
using Volo.Abp.Application.Services;
using Volo.Abp.OpenIddict.Applications;

namespace MyAbp.BookStore.Services.OpenIdApplications
{
    public class OpenIdApplicationAppService: ApplicationService, IOpenIdApplicationAppService
    {
        private readonly IOpenIddictApplicationManager _applicationManager;

        public OpenIdApplicationAppService(IOpenIddictApplicationManager applicationManager)
        {
            _applicationManager = applicationManager;
        }
        public virtual async Task CreateAsync()
        {
            var descriptor = new AbpApplicationDescriptor
            {
                ClientId = "test123",
                ClientSecret = "test123",
                ConsentType = "Implicit",
                DisplayName = "test123",
                Type = "confidential",
                ClientUri = null,
                LogoUri = null
            };
            descriptor.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Token);
            descriptor.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Revocation);
            descriptor.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Introspection);
            descriptor.Permissions.Add(OpenIddictConstants.Permissions.GrantTypes.ClientCredentials);
            descriptor.Permissions.Add($"{OpenIddictConstants.Permissions.Prefixes.Scope}BookStore");

            var application = await _applicationManager.CreateAsync(descriptor);
            //await _applicationManager.UpdateAsync(application);
        }
    }
}
