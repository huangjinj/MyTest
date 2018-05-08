using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace AppSettingsViewer
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            _settingItems = new List<SettingItem>();
            _settingItems.Add(new SettingItem() { Name = "name1", Value = "value1" });
            _settingItems.Add(new SettingItem() { Name = "name2", Value = "value2" });
            _settingItems.Add(new SettingItem() { Name = "name3", Value = "value3" });

            var localSettings = ApplicationData.Current.LocalSettings;
            //localSettings.Values["exampleSetting"] = 1;

            foreach (var v in localSettings.Values)
            {
                _settingItems.Add(new SettingItem() { Name = v.Key, Value = v.Value.ToString() });
            }

            foreach (var c in localSettings.Containers)
            {
                foreach (var v in c.Value.Values)
                {
                    _settingItems.Add(new SettingItem() { Name = v.Key, Value = v.Value.ToString() });
                }
            }

            this.DataContext = this;
        }

        private List<SettingItem> _settingItems;
        public IList<SettingItem> Settings
        {
            get
            {
                return _settingItems;
            }
        }
    }
}
