using ERService.Contracts.RunTime;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERService.Core.RunTime
{
    public class AssemblyManager : IAssemblyManager
    {
        public IEnumerable<Type> GetTypesOf<T>() =>
            AppDomain.CurrentDomain.GetAssemblies()
                                   .ToList()
                                   .Select(assembly => assembly.GetTypes()
                                                               .Where(type => typeof(T).IsAssignableFrom(type)
                                                                   && !type.IsInterface
                                                                   && !type.IsAbstract))
                                   .Aggregate((a1, a2) => a1.Concat(a2));

        public IEnumerable<Type> GetTypesByName(string typeName) =>
            AppDomain.CurrentDomain.GetAssemblies()
                                   .ToList()
                                   .Select(assembly => assembly.GetTypes()
                                                               .Where(type => type.Name == typeName))
                                   .Aggregate((a1, a2) => a1.Concat(a2));
    }
}
