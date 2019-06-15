using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace AppSettingsEditor
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        AppSettings _curSettings;
        public MainPage()
        {
            this.InitializeComponent();
            _curSettings = new AppSettings();
            this.DataContext = _curSettings;
            SettingItem.SaveFromUI = true;
        }


        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshSettings();
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            if(listView.SelectedItem != null)
            {
                SettingItem item = listView.SelectedItem as SettingItem;
                if(item.DeleteItem())
                {
                    _curSettings.Settings.Remove(item);
                }
            }
        }

        private async void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            string value = txtValue.Text;
            string container = txtContainer.Text;
            if (string.IsNullOrEmpty(name))
            {
                MessageDialog dialog = new MessageDialog("Key cannot be empty");
                await dialog.ShowAsync();
                return;
            }

            object objToAdd = AppSettingsUtil.StringToObject(value);
            if (SaveValue(name, objToAdd, container))
                RefreshSettings();
        }

        bool SaveValue(string key, object value, string container)
        {
            var dataContainer = AppSettingsUtil.CreateContainer(container);
            if (dataContainer != null)
            {
                dataContainer.Values[key] = value;
                return true;
            }
            return false;
        }

        private void RefreshSettings()
        {
            SettingItem.SaveFromUI = false;
            _curSettings = new AppSettings();
            this.DataContext = _curSettings;
            SettingItem.SaveFromUI = true;
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SettingItem item = listView.SelectedItem as SettingItem;
            if(item != null)
                txtContainer.Text = item.Container;
        }
    }
}
