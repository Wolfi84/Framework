using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wolfi.Core.Templates.Winforms
{
    /// <summary>
    /// Steuert die Anwendung
    /// </summary>
   internal static class Anwendung
    {
        [System.STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            //Infrastruktur hochfahren...
            var AppKontext = new Wolfi.Core.Kontext.Anwendungskontext();
            
            //Zuletzt gewählte Sprache wiederherstellen...
            AppKontext.Sprachen.Festlegen(Wolfi.Tests.Winforms.Template.Properties.Settings.Default.AktuelleSprache);

            //Flag für anstehenden Sprachwechsel erzeugen...
            var Sprachwechsel = false;

            
            //Aufbau des Hauptfensters und ggf. wiederaufbau nach Sprachwechsel...
            do
            {
                //UI Sprache festlegen...
                System.Threading.Thread.CurrentThread.CurrentUICulture =
                    new System.Globalization.CultureInfo(AppKontext.Sprachen.AktuelleSprache.Code);

                //Cache leeren...
                AppKontext.Sprachen.Aktualisieren();

                //Hauptfenster Starten...
                using (var Hauptfenster = AppKontext.Erzeuge<Hauptfenster>())
                {
                    //Das Anwendungsfenster Starten...
                    Application.Run(Hauptfenster);


                    //Sprachwechsel anforderung aktualisieren...
                    Sprachwechsel = Hauptfenster.Sprachwechsel;
                }



            } while(Sprachwechsel);


            //Die aktuelle Sprache in der Anwendungskonfiguration speichern...
            Tests.Winforms.Template.Properties.Settings.Default.AktuelleSprache = AppKontext.Sprachen.AktuelleSprache.Code;
            Tests.Winforms.Template.Properties.Settings.Default.Save();
            
            //Fensterpositionen speichern...
            AppKontext.Herunterfahren();

        }
    }
}
