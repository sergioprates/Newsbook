using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Newsbook.App.Model
{
    public class ModeloBase : INotifyPropertyChanged
    {
        public bool SetPropertyValue<T>(ref T backingField, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(backingField,newValue))
            {
                return false;
            }
            else
            {
                backingField = newValue;
            }

            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
