using AutoMapper;
using ERService.Contracts.RunTime;
using System;
using System.Linq;

namespace ERService.MappingProvider
{
    internal abstract class MapResolver<TDestinationBaseType, TSourceBaseType> : IMapResolver
    {
        private readonly IAssemblyManager assemblyManager;

        public MapResolver(IAssemblyManager assemblyManager) =>
            this.assemblyManager = assemblyManager;

        public abstract Func<(Type source, Type destination), bool> Comparer { get; }

        public abstract Action<(Type source, Type destination), IMapperConfigurationExpression> Resolver { get; }

        public void Resolve(IMapperConfigurationExpression mapperConfiguration)
        {
            var destinationTypes = assemblyManager.GetTypesOf<TDestinationBaseType>().ToList();
            var sourceTypes = assemblyManager.GetTypesOf<TSourceBaseType>().ToList();
            foreach (var sourceType in sourceTypes)
            {
                var destinationType = destinationTypes.SingleOrDefault(typeToCompare => Comparer((sourceType, typeToCompare)));
                if (destinationType != null)
                    Resolver((sourceType, destinationType), mapperConfiguration);
            }
        }
    }
}
