using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RebmemMusicPlayer.ViewModels
{
    internal class BaseViewModel : INotifyPropertyChanged
    {
        private readonly Dictionary<string, object> properties = new Dictionary<string, object>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected TResult Get<TResult>([CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrWhiteSpace(propertyName) == false
                && properties.TryGetValue(propertyName, out object value)
                && value is TResult result)
            {
                return result;
            }
            return default;
        }

        protected void Set<TValue>(TValue value, [CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrWhiteSpace(propertyName) == false)
            {
                properties[propertyName] = value;
                NotifyPropertyChanged(propertyName);
            }
        }
    }
}
