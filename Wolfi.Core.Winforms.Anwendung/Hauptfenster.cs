using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wolfi.Core.Templates.Winforms
{
    public partial class Hauptfenster : Wolfi.Windows.Forms.Anwendungsfenster
    {
        /// <summary>
        /// Initialisiert ein neues
        /// Haupfensterobjekt
        /// </summary>
        public Hauptfenster()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Ereignisbehandler des OnLoad Events
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //TODO: Eigene Initialisierungs Routinen ausführen...





        }


        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private bool _Sprachwechsel = false;

        /// <summary>
        /// Ruft einen Wahrheitswert ab, der angibt,
        /// ob das Fenster aktuell wegen eines 
        /// Sprachwechsels geschlossen wurde.
        /// </summary>
        /// <remarks>Standard: False</remarks>
        public bool Sprachwechsel
        {
            get
            {
                return this._Sprachwechsel;
            }
        }
    }
}
