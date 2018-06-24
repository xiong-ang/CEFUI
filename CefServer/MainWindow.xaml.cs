using Microsoft.Owin.Hosting;
using System.Windows;

namespace CefServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //Start Owin Host and Server
            WebApp.Start<Startup>(url: App.Url);

            InitializeComponent();

            //CefBrowser.BrowserSettings.WebSecurity = CefState.Disabled;
            //CefBrowser.BrowserSettings.FileAccessFromFileUrls = CefState.Enabled;
            //CefBrowser.BrowserSettings.UniversalAccessFromFileUrls = CefState.Enabled;
            //CefBrowser.BrowserSettings.TextAreaResize = CefState.Enabled;
            //CefBrowser.Address = baseAddress + "/ui";

            //Set Cef Address
            CefBrowser.Address = App.Url + "\\ui";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CefBrowser.Dispose();

            //Dispose Owin server
            Application.Current.Shutdown();
        }
    }
}
