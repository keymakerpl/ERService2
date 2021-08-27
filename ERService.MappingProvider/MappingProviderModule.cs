using AutoMapper;
using ERService.Contracts.Mapping;
using ERService.Contracts.RunTime;
using Prism.Ioc;
using Prism.Modularity;

namespace ERService.MappingProvider
{
    public class MappingProviderModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) 
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry) =>
            containerRegistry.Register<AutoMapperConfigurationFactory>()
                             .RegisterSingleton<IMapper>(container => 
                             container.Resolve<AutoMapperConfigurationFactory>()
                                      .CreateConfiguration(
                                        new EntityToLookupItemMapResolver(container.Resolve<IAssemblyManager>()),
                                        new EntityToModelWrapperMapResolver(container.Resolve<IAssemblyManager>()))
                                      .CreateMapper())
                             .Register<IMappingProvider, AutoMapperMappingProvider>();
    }
}
