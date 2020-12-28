using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Wolfi.Core.WPF.NetCore.Anwendung.Models.Manager
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
                        Name = "Testfenster"
                        //ArbeitsbereichTyp = typeof()
                    });
               
                }

                return this._Liste;
            }
        }

    }
}
