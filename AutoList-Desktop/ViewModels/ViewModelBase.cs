using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoList_Desktop.Annotations;

namespace AutoList_Desktop.ViewModels
{
    /// <inheritdoc />
    /// <summary>
    /// The ViewModel Base Object
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <inheritdoc />
        /// <summary>
        /// The Event the fires when a property changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The invocation function for the <see cref="PropertyChanged"/> Event
        /// </summary>
        /// <param name="propertyName">The Property Name</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
