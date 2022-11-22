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
        public virtual async Task CreateAsync(CreateApplicationInput input)
        {
            //var descriptor = new AbpApplicationDescriptor
            //{
            //    ClientId = "mobile",
            //    ClientSecret = "123@Abc",
            //    ConsentType = "Implicit",
            //    DisplayName = "mobile",
            //    Type = "confidential",
            //    ClientUri = null,
            //    LogoUri = null
            //};
            var descriptor = new AbpApplicationDescriptor
            {
                ClientId = input.ClientId,
                ClientSecret = input.ClientSecret,
                ConsentType = input.ConsentType,
                DisplayName = input.DisplayName,
                Type = input.Type,
                ClientUri = input.ClientUri,
                LogoUri = input.LogoUri
            };
            descriptor.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Token);
            descriptor.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Revocation);
            descriptor.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Introspection);
            descriptor.Permissions.Add(OpenIddictConstants.Permissions.GrantTypes.ClientCredentials);
            descriptor.Permissions.Add($"{OpenIddictConstants.Permissions.Prefixes.Scope}BookStore");

            var application = await _applicationManager.CreateAsync(descriptor);
        }
    }
}
