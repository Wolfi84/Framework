using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wolfi.Core.WPF.Anwendung.ViewModels.Hauptfenster
{
    /// <summary>
    /// Stellt den Dienst zum verwalten des HauptFensters 
    /// zur Verfügung
    /// </summary>
    public class HauptfensterViewModel : Core.Kontext.Anwendungsobjekt
    {
        /// <summary>
        /// Wird vom Starten initialisiert und
        /// beim Anzeigen eines neuens Fensters benötigt.
        /// </summary>
        private static Type _FensterTyp = null;

        /// <summary>
        /// Zeigt ein Fenster des gewünschten Typs an
        /// und macht es zum Hauptfenster der WPF Anwendung.
        /// </summary>
        /// <typeparam name="T">Der Typ der anzuzeigenden View.</typeparam>
        public void Starten<T>() where T : System.Windows.Window, new()
        {

            this.AktiviereBeschäftigt("ViewModel");

            HauptfensterViewModel._FensterTyp = typeof(T);

            var Fenster = new T();

            this.InitialisiereFenster(Fenster);

            //Das ViewModel und die View "verknüpfen"...
            Fenster.DataContext = this;

            this.DeaktiviereBeschäftigt("ViewModel");

            System.Windows.Application.Current.Run(Fenster);

        }


        /// <summary>
        /// Bereitet ein Fenster für die Anzeige vor.
        /// </summary>
        /// <param name="fenster">Das Fenster-Objekt,
        /// das vorbereitet werden soll.</param>
        protected virtual void InitialisiereFenster(System.Windows.Window fenster)
        {
            this.AktiviereBeschäftigt();

            //Damit xml keine Probleme beim Formatieren
            //von Datumsangaben und anderen Zahlen hat...
            fenster.Language
                = System.Windows.Markup.XmlLanguage.GetLanguage(
                    System.Threading.Thread.CurrentThread.CurrentCulture.IetfLanguageTag);

            //Dazu dem Fenster einen Namen verpassen...
            fenster.Name = fenster.GetType().Name;

            //Eine eigene Liste mit den aktuellen Fensternamen aufbauen...
            var OffeneFenster = new System.Collections.ArrayList(System.Windows.Application.Current.Windows.Count);
            foreach (System.Windows.Window w in System.Windows.Application.Current.Windows)
            {
                OffeneFenster.Add(w.Name);
            }

            var FreieNummer = 1;
            while (OffeneFenster.Contains(fenster.Name + FreieNummer))
            {
                FreieNummer++;
            }

            fenster.Name += FreieNummer;

            //Eine alte Fensterposition wiederherstellen...
            var AltePosition = this.AppKontext.Fenster.Abrufen(fenster.Name);

            if (AltePosition != null)
            {
                fenster.Left = AltePosition.Links ?? fenster.Left;
                fenster.Top = AltePosition.Oben ?? fenster.Top;
                fenster.Width = AltePosition.Breite ?? fenster.Width;
                fenster.Height = AltePosition.Höhe ?? fenster.Height;

                //Nur Maximiert wiederherstellen, sonst normal,
                //weil minimierte Fenster von den Benutzern übersehen werden...
                fenster.WindowState =
                    (System.Windows.WindowState)AltePosition.Zustand == System.Windows.WindowState.Maximized ?
                    System.Windows.WindowState.Maximized : System.Windows.WindowState.Normal;

            }

            //Dafür sorgen, dass beim Schließen
            //die Fensterposition gespeichert wird...
            fenster.Closing += (sender, e) =>
            {
                this.AktiviereBeschäftigt("FensterPositionSpeichern");

                var Position = new Core.Fenster.Fenster { Name = fenster.Name };

                //Position initialisieren:
                Position.Zustand = (int)fenster.WindowState;

                //Die Größenangaben nur, falls ein normales Fenster...
                if (fenster.WindowState == System.Windows.WindowState.Normal)
                {
                    Position.Links = (int)fenster.Left;
                    Position.Oben = (int)fenster.Top;
                    Position.Breite = (int)fenster.Width;
                    Position.Höhe = (int)fenster.Height;
                }

                this.AppKontext.Fenster.Hinterlegen(Position);

                this.DeaktiviereBeschäftigt("FensterPositionSpeichern");


                //den DataContext freigeben...
                fenster.DataContext = null;
            };

            this.DeaktiviereBeschäftigt();
        }


        /// <summary>
        /// Ruft die aktuelle Anwendungssprache
        /// ab oder legt diese fest.
        /// </summary>
        public Core.Sprachen.Sprache AktuelleSprache
        {
            get
            {
                return this.AppKontext.Sprachen.AktuelleSprache;
            }
            set
            {

                if (this.AppKontext.Sprachen.AktuelleSprache.Code != value.Code)
                {
                    System.Windows.MessageBox.Show(
                        Properties.Texte.Sprachwechsel,
                        Properties.Texte.Anwendungstitel,

                        System.Windows.MessageBoxButton.OK,
                        System.Windows.MessageBoxImage.Information);

                    this.AppKontext.Protokoll.Eintragen(
                        Properties.Texte.Sprachwechsel,
                        Core.Protokoll.ProtokollEintragTyp.Fehler);
                }
                
                this.AppKontext.Sprachen.AktuelleSprache = value;

                this.OnPropertyChanged();
            }
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private Models.DTOs.Aufgaben _Aufgaben = null;

        /// <summary>
        /// Ruft die Anwendungspunkte ab.
        /// </summary>
        public Models.DTOs.Aufgaben Aufgaben
        {
            get
            {
                if (this._Aufgaben == null)
                {
                    this.AktiviereBeschäftigt();

                    var AufgabenManager = this.AppKontext.Erzeuge<Models.Manager.AufgabenManager>();
                    this._Aufgaben = AufgabenManager.Liste;

                    this.AppKontext.Protokoll.Eintragen("Das Fenster hat die Aufgaben gecachet...");

                    //Die Standardaufgabe aktivieren...
                    var i = Wolfi.Core.WPF.Anwendung.Properties.Settings.Default.AktuelleAufgabe;
                    if (this._Aufgaben != null && i < this._Aufgaben.Count)
                    {
                        this.AktuelleAufgabe = this._Aufgaben[i];
                        this.AppKontext.Protokoll.Eintragen($"Die Aufgabe \"{this.AktuelleAufgabe.Name}\" wurde aktiviert...");
                    }

                    this.DeaktiviereBeschäftigt();
                }

                return this._Aufgaben;
            }
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private Models.DTOs.Aufgabe _AktuelleAufgabe = null;

        /// <summary>
        /// Ruft den aktuellen Anwendungspunkt
        /// ab oder legt diesen fest.
        /// </summary>
        public Models.DTOs.Aufgabe AktuelleAufgabe
        {
            get
            {
                return this._AktuelleAufgabe;
            }
            set
            {
                this._AktuelleAufgabe = value;

                if (this._AktuelleAufgabe.Arbeitsbereich == null
                    && this._AktuelleAufgabe.ArbeitsbereichTyp != null)
                {
                    this._AktuelleAufgabe.Arbeitsbereich
                        = System.Activator.CreateInstance(this._AktuelleAufgabe.ArbeitsbereichTyp)
                        as System.Windows.Controls.UserControl;
                }

                this.OnPropertyChanged();
            }
        }
    }
}
