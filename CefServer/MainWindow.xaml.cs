using CefSharp;
using Microsoft.Owin.Hosting;
using System.Windows;
using System.Windows.Input;

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
            CefBrowser.Address = App.Url;

            // Allow JS allow clipboard
            CefBrowser.BrowserSettings.JavascriptAccessClipboard = CefState.Enabled;
            CefBrowser.BrowserSettings.JavascriptDomPaste = CefState.Enabled;

            TestSendEvent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CefBrowser.Dispose();

            //Dispose Owin server
            Application.Current.Shutdown();
        }

        private void TestSendEvent()
        {
            this.KeyDown += Window_KeyDown;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (Key.F12 == e.Key)//Open dev tools
                CefBrowser.ShowDevTools();
            else if (Key.F11 == e.Key)//Send key event
                KeyEventSender.SendKey(CefBrowser);
            else if (Key.F10 == e.Key)//Send common event
                CefBrowser.Paste();

            //CefBrowser.Copy();
            //CefBrowser.SelectAll();
            //CefBrowser.Cut();
            //CefBrowser.Redo();
            //CefBrowser.Undo();
            //CefBrowser.Delete();
        }
    }
}
