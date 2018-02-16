using MahApps.Metro;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Markup;

namespace Integrado
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-pe"); ;
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-pe"); ;

            FrameworkElement.LanguageProperty.OverrideMetadata(
              typeof(FrameworkElement),
              new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));


            //ThemeManager.ChangeAppStyle(this,
            //                       ThemeManager.GetAccent("Red"),
            //                       ThemeManager.GetAppTheme("BaseLight"));

            //var allTypes = typeof(App).Assembly.GetTypes();
            //var filteredTypes = allTypes.Where(d =>
            //    typeof(MetroWindow).IsAssignableFrom(d)
            //    && typeof(MetroWindow) != d
            //    && !d.IsAbstract).ToList();

            //foreach (var type in filteredTypes)
            //{
            //    var defaultStyle = this.Resources["MetroWindowDefault"];
            //    this.Resources.Add(type, defaultStyle);
            //}


            base.OnStartup(e);
        }
    }
}
