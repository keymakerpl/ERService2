using System;
using System.Collections.Generic;

namespace ERService.Contracts.RunTime
{
    public interface IAssemblyManager
    {
        IEnumerable<Type> GetTypesOf<T>();
    }
}