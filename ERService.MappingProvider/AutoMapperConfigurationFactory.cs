using AutoMapper;
using System;
using System.Linq;

namespace ERService.MappingProvider
{
    internal class AutoMapperConfigurationFactory
    {
        public MapperConfiguration CreateConfiguration(params IMapResolver[] resolvers) =>
            new MapperConfiguration(config =>
            {
                config.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
                resolvers.ToList().ForEach(resolver => resolver.Resolve(config));
            });
    }
}
