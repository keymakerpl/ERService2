using AutoMapper;
using System;

namespace ERService.MappingProvider
{
    public interface IMapResolver
    {
        Func<(Type source, Type destination), bool> Comparer { get; }
        Action<(Type source, Type destination), IMapperConfigurationExpression> Resolver { get; }
        void Resolve(IMapperConfigurationExpression mapperConfiguration);
    }
}