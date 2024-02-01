using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosmodrom.Helpers
{
    using System;
    using System.Windows.Input;

    namespace Kosmodrom.Helpers
    {
        using System;
        using System.Windows.Input;

        namespace Kosmodrom.Helpers
        {
            public class ParameterBaseCommand : ICommand
            {
                private readonly Action<int> _command;
                private readonly Func<bool> _canExecute;

                public ParameterBaseCommand(Action<int> command, Func<bool> canExecute = null)
                {
                    if (command == null)
                        throw new ArgumentNullException("command");
                    _canExecute = canExecute;
                    _command = command;
                }

                public void Execute(object parameter)
                {
                        _command(int.Parse((string)parameter));
                    
                }

                public bool CanExecute(object parameter)
                {
                    if (_canExecute == null)
                        return true;
                    return _canExecute();
                }

                public event EventHandler CanExecuteChanged;
            }
        }

    }

}
