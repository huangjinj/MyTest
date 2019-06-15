using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;

namespace AppSettingsEditor
{
    public class SettingItem : BindableBase
    {
        public static bool SaveFromUI = false;

        string _container;
        public string Container
        {
            get
            {
                return _container;
            }
            set
            {
                if (_container == value)
                    return;
                _container = value;
                OnPropertyChanged("Container");
            }
        }

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

        object _value;
        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value == value)
                    return;
                try
                {
                    if (SaveFromUI)
                    {
                        object cValue;
                        if (SaveValue(value, out cValue))
                        {
                            _value = cValue;
                            OnPropertyChanged("Value");
                        }
                    }
                    else
                    {
                        _value = value;
                        OnPropertyChanged("Value");
                    }
                }
                catch(Exception ex)
                {
                    MessageDialog dialog = new MessageDialog(ex.Message);
                    dialog.ShowAsync();
                }
            }
        }

        public string DataType
        {
            get
            {
                return Value?.GetType().Name;
            }

        }

        public bool DeleteItem()
        {
            var dataContainer = AppSettingsUtil.ContainerExists(Container);
            if(dataContainer != null)
            {
                dataContainer.Values.Remove(Name);
                return true;
            }
            return false;
        }

        bool SaveValue(object value, out object convertedValue)
        {
            convertedValue = null;
            var dataContainer = AppSettingsUtil.ContainerExists(Container);
            if (dataContainer != null)
            {
                switch (DataType)
                {
                    case "String":
                        convertedValue = value;
                        break;
                    case "Int32":
                        convertedValue = System.Int32.Parse(value.ToString());
                        break;
                    case "Int64":
                        convertedValue = System.Int64.Parse(value.ToString());
                        break;
                    case "UInt32":
                        convertedValue = System.UInt32.Parse(value.ToString());
                        break;
                    case "UInt64":
                        convertedValue = System.UInt64.Parse(value.ToString());
                        break;
                    case "Byte":
                        convertedValue = System.Byte.Parse(value.ToString());
                        break;
                    case "Boolean":
                        convertedValue = System.Boolean.Parse(value.ToString());
                        break;
                    case "Char":
                        convertedValue = System.Char.Parse(value.ToString());
                        break;
                    default:
                        convertedValue = value;
                        break;
                }
                dataContainer.Values[Name] = convertedValue;
                return true;
            }
            return false;
        }

    }
}
