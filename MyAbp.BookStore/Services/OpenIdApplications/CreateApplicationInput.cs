namespace MyAbp.BookStore.Services.OpenIdApplications
{
    public class CreateApplicationInput
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ConsentType { get; set; }
        public string DisplayName { get; set; }
        public string Type { get; set; }
        public string ClientUri { get; set; }
        public string LogoUri { get; set; }
    }
}
