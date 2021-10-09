using System.Collections.Generic;

namespace ERService.Contracts.Navigation
{
    public interface IDetailMenuItems
    {
        IEnumerable<DetailMenuItem> DetailMenuItems();
    }
}
