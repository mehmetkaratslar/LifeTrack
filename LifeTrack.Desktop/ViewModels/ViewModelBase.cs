using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LifeTrack.Desktop.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);

            // Debug için
            Console.WriteLine($"Property değişti: {propertyName}, Yeni değer: {value}");

            return true;
        }
    }
}