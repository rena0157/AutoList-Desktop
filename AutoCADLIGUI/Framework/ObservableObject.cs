// ObservableObject.cs
// By: Adam Renaud
// Created: July, 2018
// Purpose: To house the Obervable object class that is used in the MVVM framework
// for this Project

using System.ComponentModel;

namespace AutoCADLIGUI.Framework
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChangedEvent(string propertyName)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}