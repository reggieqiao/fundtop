using AutoMapper;

namespace Fundtop.Core
{
    public static class AutoMapperHelper
    {
        public static IEnumerable<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> sourceList)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            var mapper = config.CreateMapper();
            return mapper.Map<IEnumerable<TDestination>>(sourceList);
        }
    }
}