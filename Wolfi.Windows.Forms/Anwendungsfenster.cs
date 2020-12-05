using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wolfi.Core.Kontext;

namespace Wolfi.Windows.Forms
{
    /// <summary>
    /// Stellt Das Basis Anwendungsfenster für Anwendungen bereit.
    /// </summary>
    public partial class Anwendungsfenster : Form, Wolfi.Core.Kontext.IAppKontext
    {
       

        /// <summary>
        /// Initialisiert ein neues Anwendungsfenster.
        /// </summary>
        public Anwendungsfenster()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Ruft die Infrastruktur ab
        /// oder legt diese fest.
        /// </summary>
        public Anwendungskontext AppKontext { get; set;}


        /// <summary>
        /// Löst das Load Ereignis aus.
        /// </summary>
        /// <remarks>Wird um das Wiederherstellen der
        /// alten Fensterposition und -größe mit dem Fenstermanager
        /// der Infrastruktur erweitert.</remarks>
        protected override void OnLoad(EventArgs e)
        {
            //Basis Routine durchführen...
            base.OnLoad(e);

            //Nur wenn die Anwendung läuft, ...
            if (!this.DesignMode)
            {
                var AltePosition = this.AppKontext.Fenster.Abrufen(this.Name);

                if (AltePosition != null)
                {
                    //Das alte Links und Oben wiederherstellen...
                    this.Left = AltePosition.Links.HasValue ? AltePosition.Links.Value : this.Left;
                    this.Top = AltePosition.Oben.HasValue ? AltePosition.Oben.Value : this.Top;

                    //Damit ein Breite und Höhe, eingestellt im Designer, nicht überschrieben wird...
                    if (this.FormBorderStyle == FormBorderStyle.Sizable)
                    {
                        this.Width = AltePosition.Breite ?? this.Width;
                        this.Height = AltePosition.Höhe ?? this.Height;
                    }

                    //Nur Maximiert wiederherstellen, Minimiert ist Normal...
                    this.WindowState
                        = AltePosition.Zustand == (int)System.Windows.Forms.FormWindowState.Maximized
                        ? FormWindowState.Maximized : FormWindowState.Normal;
                }
            }
        }

        /// <summary>
        /// Löst das FormClosing Ereignis aus.
        /// </summary>
        /// <remarks>Wird um das Speichern der Fensterposition und -größe
        /// mit dem Fenstermanager der Infrastruktur erweitert.</remarks>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {

            if (!this.DesignMode)
            {
                //Die Positionsdaten des Fensters hinterlegen...
                var Daten = new Wolfi.Core.Fenster.Fenster();

                Daten.Name = this.Name;
                Daten.Zustand = (int)this.WindowState;

                //Die Lage und Größe nur,
                //wenn der Fensterzustand normal ist...
                if (this.WindowState == FormWindowState.Normal)
                {
                    Daten.Links = this.Left;
                    Daten.Oben = this.Top;

                    //Nur, wenn die Größe änderbar ist...
                    if (this.FormBorderStyle == FormBorderStyle.Sizable)
                    {
                        Daten.Breite = this.Width;
                        Daten.Höhe = this.Height;
                    }
                }

                //Die Daten dem Manager übergeben...
                this.AppKontext.Fenster.Hinterlegen(Daten);
            }

            //Basis Routine ausführen...
            base.OnFormClosing(e);
        }

    }
}
