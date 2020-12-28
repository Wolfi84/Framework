
using System;
using System.Collections.Generic;
using System.Text;
using Wolfi.Core.WPF.NetCore.Anwendung.ViewModels.Hauptfenster;

namespace Wolfi.Core.WPF.NetCore.Anwendung
{
    class Anwendung 
    {
        [STAThread]
        static void Main()
        {
            var Application = new App();

            var Kontext = new AnwendungsKontext();

            var VM = Kontext.Erzeuge<HauptfensterViewModel>();

            VM.Starten<Views.Hauptfenster.Hauptfenster>();

        }
    }
}
