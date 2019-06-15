using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace AppSettingsEditor
{
    public class AppSettings : BindableBase
    {
        private ObservableCollection<SettingItem> _settingItems;

        public AppSettings()
        {
            _settingItems = new ObservableCollection<SettingItem>();

            LoadSetting(ApplicationData.Current.LocalSettings, null);
        }

        void LoadSetting(ApplicationDataContainer dataContainer, string parentContainer)
        {
            string dataContainerName = dataContainer.Name;
            if(!string.IsNullOrEmpty(parentContainer))
                dataContainerName = parentContainer + "\\" + dataContainerName;

            foreach (var v in dataContainer.Values)
            {
                _settingItems.Add(new SettingItem() { Name = v.Key, Container = dataContainerName, Value = v.Value });
            }

            foreach (var c in dataContainer.Containers)
            {
                LoadSetting(c.Value, dataContainerName);
            }
        }

        public string Title
        {
            get
            {
                return "All local settings";
            }
        }

        public ObservableCollection<SettingItem> Settings
        {
            get
            {
                return _settingItems;
            }
            set
            {
                _settingItems = value;
                OnPropertyChanged("Settings");
            }
        }
    }
}
