using AutoMapper;
using ERService.Contracts.Mapping;

namespace ERService.MappingProvider
{
    public class AutoMapperMappingProvider : IMappingProvider
    {
        private readonly IMapper mapper;

        public AutoMapperMappingProvider(IMapper mapper) => this.mapper = mapper;

        public TDestination MapTo<TDestination>(object source) => mapper.Map<TDestination>(source);
    }
}
