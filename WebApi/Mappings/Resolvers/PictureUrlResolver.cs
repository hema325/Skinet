using AutoMapper;

namespace WebApi.AutoMapper.Resolvers
{
    public class PictureUrlResolver : IValueResolver<object, object, string>
    {
        private readonly HttpContext? _context;
        public PictureUrlResolver(IHttpContextAccessor contextAccessor)
        {
            _context = contextAccessor.HttpContext;
        }

        public string Resolve(dynamic source, object destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                var scheme = _context?.Request.Scheme;
                var host = _context?.Request.Host;
                return $"{scheme}://{host}/Files/{source.PictureUrl}";
            }

            return null;
        }
    }
}
