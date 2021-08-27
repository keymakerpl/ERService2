using ERService.Contracts.RunTime;
using ERService.Core.RunTime;
using Prism.Ioc;
using Prism.Modularity;

namespace ERService.Core
{
    public class CoreModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry) => 
            containerRegistry.Register<IAssemblyManager, AssemblyManager>();
    }
}
