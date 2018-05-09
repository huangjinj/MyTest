using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSettingsEditor
{
    public class SettingItem : BindableBase
    {
        string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name == value)
                    return;
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        string _value;
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value == value)
                    return;
                _value = value;
                OnPropertyChanged("Value");
            }
        }

        public string DataType
        {
            get
            {
                return Value?.GetType().Name;
            }

        }

    }
}
