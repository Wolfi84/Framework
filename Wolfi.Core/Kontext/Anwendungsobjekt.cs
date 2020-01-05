using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Wolfi.Core.Kontext
{
    /// <summary>
    /// Basisobjekt einer Anwendung
    /// </summary>
    public abstract class Anwendungsobjekt :  IAppKontext, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Ruft das Infrastrukturobjekt ab oder legt dieses fest.
        /// </summary>
        public Anwendungskontext AppKontext { get; set; }

        /// <summary>
        /// Wird ausgelöst, wenn eine Ausnahme 
        /// in der Anwendung aufgetreten ist.
        /// </summary>
        public event FehlerAufgetretenEventHandler FehlerAufgetreten;


        /// <summary>
        /// Löst das Ereignis FehlerAufgetreten aus.
        /// </summary>
        /// <param name="e">Das Objekt mit den Ereignisdaten</param>
        protected virtual void OnFehlerAufgetreten(FehlerAufgetretenEventArgs e)
        {
            if (this.FehlerAufgetreten != null)
            {
                this.FehlerAufgetreten(this, e);
            }
        }

        /// <summary>
        /// Wird ausgelöst, wenn sich der Inhalt einer
        /// Eigenschaft geändert hat.
        /// </summary>
        /// <remarks>Notwendig für die WPF Datenbindung.</remarks>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Löst das Ereignis PropertyChanged aus.
        /// </summary>
        /// <param name="name">Die Bezeichnung der Eigenschaft,
        /// deren Inhalt geändert wurde.</param>
        /// <remarks>Eine fehlende name-Einstellung wird mit dem
        /// Aufrufernamen voreingestellt.</remarks>
        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName]string name = "")
        {
            //Wegen Multithreadings, damit
            //der Garbage Collector das Objekt mit
            //dem Behandler nicht wegräumt...
            var BehandlerKopie = this.PropertyChanged;

            if (BehandlerKopie != null)
            {
                BehandlerKopie(this, new PropertyChangedEventArgs(name));
            }
        }

        /// <summary>
        /// Wird vom AktiviereBeschäftigt erhöht
        /// und vom DeaktivereBeschäftigt vermindert.
        /// </summary>
        private int _BinBeschäftigtEbene = 0;

        /// <summary>
        /// Schreibt in das Protokoll, dass eine
        /// Methode zu laufen beginnt...
        /// </summary>
        protected virtual void AktiviereBeschäftigt([System.Runtime.CompilerServices.CallerMemberName]string methode = "")
        {

            this.BinBeschäftigt = true;
            this._BinBeschäftigtEbene++;

            this.AppKontext.Protokoll.Eintragen($"{methode} startet...", Protokoll.ProtokollEintragTyp.Beschäftigt);
        }

        /// <summary>
        /// Schreibt in das Protokoll, dass eine
        /// Methode beendet ist.
        /// </summary>
        protected virtual void DeaktiviereBeschäftigt([System.Runtime.CompilerServices.CallerMemberName]string methode = "")
        {
            this.AppKontext.Protokoll.Eintragen($"{methode} beendet.");

            this._BinBeschäftigtEbene--;

            if (this._BinBeschäftigtEbene <= 0)
            {
                this.BinBeschäftigt = false;
                this._BinBeschäftigtEbene = 0;
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private bool _BinBeschäftigt = false;

        /// <summary>
        /// Ruft einen Wahrheitswert ab,
        /// der angibt, ob die Anwendung gerade arbeitet,
        /// oder legt diesen fest.
        /// </summary>
        public bool BinBeschäftigt
        {
            get
            {
                return this._BinBeschäftigt;
            }
            set
            {
                this._BinBeschäftigt = value;
                this.OnPropertyChanged();
            }
        }
    }
}

