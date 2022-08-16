using System;
using System.Windows.Input;

namespace RebmemMusicPlayer.MVVM
{
    internal class RelayCommand : ICommand
    {
        private Action _action;

        public RelayCommand(Action action)
        {
            _action = action;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _action.Invoke();
        }
    }

    internal class RelayCommand<TArg> : ICommand
    {
        private Action<TArg> _action;

        public RelayCommand(Action<TArg> action)
        {
            _action = action;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is TArg)
            {
                _action.Invoke((TArg)parameter);
            }
            else
            {
                _action.Invoke(default!);
            }
        }
    }
}
