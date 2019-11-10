using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using StringFormatter.Wpf.Events;
using StringFormatter.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StringFormatter.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = Locator.MainViewModel;
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await ((MainViewModel)DataContext).Load();
        }

        /// <summary>
        /// Will show message dialog
        /// </summary>
        public async Task<MessageDialogResult> ShowMetroWindowMessage(object sender, MessageDialogEventArgs e)
        {
            return e.Settings == null 
                ? await this.ShowMessageAsync(e.MessageHeader, e.MessageText, e.DialogStyle) 
                : await this.ShowMessageAsync(e.MessageHeader, e.MessageText, e.DialogStyle, e.Settings);
        }
    }
}
