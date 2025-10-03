global using Microsoft.Extensions.Configuration;

namespace Services.MappingProfiles
{
    internal class PictureUrlResolver(IConfiguration _configuration) : IValueResolver<Product, ProductResultDto, string>
    {
        public string Resolve(Product source, ProductResultDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configuration["baseUrl"]}{source.PictureUrl}";
            }
            return string.Empty;
        }
    }
}
