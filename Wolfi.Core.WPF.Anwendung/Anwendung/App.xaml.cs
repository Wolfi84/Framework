using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Wolfi.Core.WPF.Anwendung
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        [System.STAThread]
        private static void Main()
        {

            //Unsere Infrastruktur hochfahren...
            var AppKontext = new Core.Kontext.Anwendungskontext();

            //Die Protokolleinträge sollen gespeichert werden...
            if (Anwendung.Properties.Settings.Default.LoggingEinAus)
            {
                var Pfad = System.IO.Path.Combine(AppKontext.AnwendungsdatenPfadLokal, Anwendung.Properties.Settings.Default.ProtokollVerzeichnis);

                System.IO.Directory.CreateDirectory(Pfad);
                
                AppKontext.Protokoll.Pfad = System.IO.Path.Combine(Pfad, "Protokoll.log");
                AppKontext.Protokoll.Zusammenräumen(maxGenerationen: 2);
            }

            //Für WPF die Anwendung initialisieren...
            var WpfApp = new App();

            //Damit die in App.xaml definierten
            //Ressourcen geladen werden...
            WpfApp.InitializeComponent();

            //Der Infrastruktur den Thread-Dienst mitteilen...
            AppKontext.Dispatcher = WpfApp.Dispatcher;


            //die Anwendungssprache einstellen
            AppKontext.Sprachen.Festlegen(Anwendung.Properties.Settings.Default.AktuelleSprache);


            //Im Thread eine neue Kultur für die eingestellte Sprache einstellen.
            //Default wird Deutsch eingestellt da davon ausgegangen wird das der
            //Großteil der User Deutsch spricht.
            //
            System.Threading.Thread.CurrentThread.CurrentUICulture
                = new System.Globalization.CultureInfo(AppKontext.Sprachen.AktuelleSprache.Code);

            if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name
                != System.Threading.Thread.CurrentThread.CurrentCulture.Parent.Name)
            {
                AppKontext.Protokoll.Eintragen(
                    "Die Oberflächensprache unterscheidet sich von der Sprache für die Zahlenformatierung!",
                    Core.Protokoll.ProtokollEintragTyp.Warnung);
            }

            //Sprachen-Manager seine Caches entleert
            //damit die Sprachen sicher übersetzt werden...
            AppKontext.Sprachen.Aktualisieren();


            //Das ViewModel starten...
            var VM = AppKontext.Erzeuge<ViewModels.Hauptfenster.HauptfensterViewModel>();

            _ = VM.Aufgaben;

            VM.Starten<Views.Hauptfenster.Hauptfenster>();


            //Die aktuelle Sprache in die Konfiguration übernehmen...
            Anwendung.Properties.Settings.Default.AktuelleSprache = AppKontext.Sprachen.AktuelleSprache.Code;

            //Damit z. B. die Fensterpositionen gespeichert werden...
            AppKontext.Herunterfahren();

            //Damit die Konfiguration gespeichert wird...
            Anwendung.Properties.Settings.Default.Save();

            //AppKontext.Protokoll.Zusammenräumen(maxGenerationen: 2);
        }
    }
}
