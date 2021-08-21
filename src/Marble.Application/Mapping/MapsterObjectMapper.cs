using Mapster;
using Marble.Core.ObjectMapper;

namespace Marble.Application.Mapping
{
    public class MapsterObjectMapper : IObjectMapper
    {
        public TDestination MapTo<TSource, TDestination>(TSource source, TDestination destination)
        {
            return source.Adapt(destination);
        }

        public TDestination MapTo<TDestination>(object source)
        {
            return source.Adapt<TDestination>();
        }
    }
}