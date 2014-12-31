using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace KwikFormation {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {

        [STAThread]
        public static void Main() {
            try {
                var application = new App();
                application.InitializeComponent();
                application.Run();
            } catch (Exception e) {
                string[] lines = { 
                                     "Time: " + DateTime.Now.ToLongTimeString(),
                                     "Exception: " + e.Message,
                                     "Source: " + e.Source,
                                     "HelpLink: " + e.HelpLink,
                                     "StackTrace: " + e.StackTrace
                                 };
                System.IO.File.WriteAllLines(Path.Combine(Environment.SpecialFolder.ApplicationData.ToString(), "KwikFormation/log/exception.log"), lines);
            }
        }

    }
}
