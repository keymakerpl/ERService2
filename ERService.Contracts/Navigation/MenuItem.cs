using System.Windows.Input;

namespace ERService.Contracts.Navigation
{
    public class MenuItem
    {
        public object Icon { get; set; }

        public string Name { get; set; }

        public ICommand Command { get; set; }
    }
}
