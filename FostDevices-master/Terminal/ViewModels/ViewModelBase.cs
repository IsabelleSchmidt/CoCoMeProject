using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CoCoME.Terminal.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        protected bool Set<T>(ref T field, T value, [CallerMemberName] string property = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                if (Application.Current?.Dispatcher is var dispatcher)
                {
                    dispatcher.Invoke(() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property)));
                }
                else
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
                }
                return true;
            }
            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
