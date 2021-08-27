using AutoMapper;
using ERService.Contracts.Data;
using ERService.Contracts.Models;
using ERService.Contracts.RunTime;
using ERService.Shared.Extensions;
using System;

namespace ERService.MappingProvider
{
    internal class EntityToLookupItemMapResolver : MapResolver<ILookupItem, IEntity>
    {
        public EntityToLookupItemMapResolver(IAssemblyManager assemblyManager) : base(assemblyManager)
        {
        }

        public override Func<(Type source, Type destination), bool> Comparer =>
            types => $"{types.source.GetFriendlyName()}LookupItem" == types.destination.GetFriendlyName();

        public override Action<(Type source, Type destination), IMapperConfigurationExpression> Resolver =>
            (types, config) => config.CreateMap(types.source, types.destination).ReverseMap();
    }
}
