using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolfi.Core.WPF.Anwendung.Views;

namespace Wolfi.Core.WPF.Anwendung.Models.Manager
{
    /// <summary>
    /// Stellt einen Dienst zum
    /// Verwalten der Anwendungspunkte bereit.
    /// </summary>
    public class AufgabenManager : Wolfi.Core.Kontext.Anwendungsobjekt
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private DTOs.Aufgaben _Liste = null;

        /// <summary>
        /// Ruft die Liste mit den unterstützten
        /// Anwendungspunkten ab.
        /// </summary>
        public DTOs.Aufgaben Liste
        {
            get
            {
                if (this._Liste == null)
                {
                    this._Liste = new DTOs.Aufgaben();


                    this._Liste.Add(new DTOs.Aufgabe
                    {
                        Symbol = "",
                        Name = "Testfenster",
                        ArbeitsbereichTyp = typeof(TestView)
                    });
               
                }

                return this._Liste;
            }
        }

    }
}
