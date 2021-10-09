using AutoMapper;
using ERService.Contracts.Data;
using ERService.Contracts.RunTime;
using ERService.Mvvm.Base;
using ERService.Shared.Extensions;
using System;

namespace ERService.MappingProvider
{
    internal class EntityToModelWrapperMapResolver : GenericMapResolver<IWrappable, IEntity>
    {
        public EntityToModelWrapperMapResolver(IAssemblyManager assemblyManager) : base(assemblyManager)
        {
        }

        public override Func<(Type source, Type destination), bool> Comparer =>
            types => $"{types.source.GetFriendlyName()}Wrapper" == types.destination.GetFriendlyName();

        public override Action<(Type source, Type destination), IMapperConfigurationExpression> Resolver =>
            (types, config) =>
            {
                config.CreateMap(types.source, types.destination).ForCtorParam("model", opt => opt.MapFrom(src => src));
                config.CreateMap(types.destination, types.source).ConvertUsing((source, destination, ctx) => 
                {
                    return source.GetType().GetProperty("Model").GetValue(source);
                });
            };
    }
}
