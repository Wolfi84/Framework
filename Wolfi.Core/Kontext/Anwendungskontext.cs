using System;
using System.Collections.Generic;
using System.Text;

//Erweiterungen Einbinden...
using Wolfi.Core.Erweiterungen;


namespace Wolfi.Core.Kontext
{
    /// <summary>
    /// Stellt die Infrastruktur für eine Anwendung bereit.
    /// </summary>
    public class Anwendungskontext 
    {

        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private Wolfi.Core.Fenster.FensterManager _Fenster = null;

        
        /// <summary>
        /// Ruft den Dienst zum Verwalten der 
        /// Anwendungsfenster ab.
        /// </summary>
        public Wolfi.Core.Fenster.FensterManager Fenster
        {
            get
            {

                if (this._Fenster == null)
                {

                    this._Fenster = this.Erzeuge<Wolfi.Core.Fenster.FensterManager>();
                }

                return this._Fenster;
            }
        }


        /// <summary>
        /// Gibt ein initialisiertes Anwendungsobjekt zurück.
        /// </summary>
        /// <typeparam name="T">Der Anwendungstyp, der benötigt wird.</typeparam>
        /// <returns>Ein Objekt, wo die AppKontext Eigenschaft
        /// bereits voreingestellt ist und weitere Vorbereitungen getroffen wurden.</returns>
        public virtual T Erzeuge<T>() where T : Wolfi.Core.Kontext.IAppKontext, new()
        {
            var NeuesObjekt = new T();
            NeuesObjekt.AppKontext = this;


            if (!(NeuesObjekt is Protokoll.ProtokollManager))
            {
                this.Protokoll.Eintragen($"{NeuesObjekt} wurde initialisiert...", Core.Protokoll.ProtokollEintragTyp.NeueInstanz);
            }

            //Casten als Anwendungsobjekt um den Fehlerbehandler anzuhängen...
            var Anwendungsobjekt = NeuesObjekt as Anwendungsobjekt;

            if (Anwendungsobjekt != null)
            {
                //Ausgabemuster felstlegen...
                const string FehlerMuster = "Ausnahme in {0}:\r\n{1}";

                Anwendungsobjekt.FehlerAufgetreten += (sender, e)
                    => this.Protokoll.Eintragen(
                        string.Format(FehlerMuster, sender, e.Ausnahme.Message),
                        Core.Protokoll.ProtokollEintragTyp.Fehler);
                if (!(NeuesObjekt is Protokoll.ProtokollManager))
                {
                    this.Protokoll.Eintragen($"{Anwendungsobjekt} behandelt FehlerAufgetreten...");
                }
            }

            return NeuesObjekt;
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private Wolfi.Core.Sprachen.SprachenManager _Sprachen = null;


        /// <summary>
        /// Ruft den Dienst zum Verwalten der
        /// Anwendungssprachen ab.
        /// </summary>
        public Wolfi.Core.Sprachen.SprachenManager Sprachen
        {
            get
            {

                if (this._Sprachen == null)
                {
                    this._Sprachen = this.Erzeuge<Wolfi.Core.Sprachen.SprachenManager>();
                }

                return this._Sprachen;
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private string _AnwendungsdatenPfadLokal = null;


        /// <summary>
        /// Ruft das lokale Datenverzeichnis für
        /// die Anwendung im Benutzerprofil ab.
        /// Appdata.Local
        /// </summary>
        /// <remarks>Es ist sichergestellt, dass das 
        /// Verzeichnis exisitert.</remarks>
        public string AnwendungsdatenPfadLokal
        {
            get
            {
                if (this._AnwendungsdatenPfadLokal == null)
                {
                    this._AnwendungsdatenPfadLokal
                        = System.IO.Path.Combine(
                            System.Environment.GetFolderPath(
                                Environment.SpecialFolder.LocalApplicationData),
                            this.HoleFirmenname(),
                            this.HoleProdukt(),
                            this.HoleVersion()
                    );
                }

                System.IO.Directory.CreateDirectory(this._AnwendungsdatenPfadLokal);

                return this._AnwendungsdatenPfadLokal;
            }
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private string _AnwendungsdatenPfadRoaming = null;


        /// <summary>
        /// Ruft das Datenverzeichnis für
        /// die Anwendung im Benutzerprofil ab.
        /// Appdata.Roaming
        /// </summary>
        /// <remarks>Es ist sichergestellt, dass das 
        /// Verzeichnis exisitert.</remarks>
        public string AnwendungsdatenPfadRoaming
        {
            get
            {
                if (this._AnwendungsdatenPfadRoaming == null)
                {
                    this._AnwendungsdatenPfadRoaming
                        = System.IO.Path.Combine(
                            System.Environment.GetFolderPath(
                                Environment.SpecialFolder.ApplicationData),
                            this.HoleFirmenname(),
                            this.HoleProdukt(),
                            this.HoleVersion()
                    );
                }

                System.IO.Directory.CreateDirectory(this._AnwendungsdatenPfadRoaming);

                return this._AnwendungsdatenPfadRoaming;
            }
        }

        /// <summary>
        /// Beendet den Infrastrukturdienst.
        /// </summary>
        /// <remarks>Speichert die Fensterpositon</remarks>
        public virtual void Herunterfahren()
        {
            if (this._Fenster != null)
            {
                this.Fenster.Speichern();
            }

        }


        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        /// <remarks>Die Anwendung kann nur genau 
        /// eine Anwendungsverzeichnis besitzen, deshalb "statisch".</remarks>
        private static string _Anwendungspfad = null;


        /// <summary>
        /// Ruft das Verzeichnis ab, aus dem die Anwendung gestartet wurde.
        /// </summary>
        public string Anwendungspfad
        {
            get
            {

                if (Anwendungskontext._Anwendungspfad == null)
                {
                    Anwendungskontext._Anwendungspfad
                        = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                }

                return Anwendungskontext._Anwendungspfad;
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private Protokoll.ProtokollManager _Protokoll = null;

        /// <summary>
        /// Ruft den Dienst zum Verwalten des
        /// Anwendungsprotokolls ab.
        /// </summary>
        public Protokoll.ProtokollManager Protokoll
        {
            get
            {
                if (this._Protokoll == null)
                {
                    this._Protokoll = this.Erzeuge<Protokoll.ProtokollManager>();
                }

                return this._Protokoll;
            }
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private static System.Random _Zufallsgenerator = null;

        /// <summary>
        /// Ruft den Zufallsgenerator der Anwendung ab.
        /// </summary>
        public System.Random Zufallsgenerator
        {
            get
            {
                if (Anwendungskontext._Zufallsgenerator == null)
                {
                    Anwendungskontext._Zufallsgenerator = new System.Random();
                    this.Protokoll.Eintragen(
                            "Die Anwendung hat den Zufallsgenerator erstellt...",
                            Core.Protokoll.ProtokollEintragTyp.NeueInstanz);
                }

                return Anwendungskontext._Zufallsgenerator;
            }
        }

        /// <summary>
        /// Ruft den Thread-Dienst einer
        /// WPF Anwendung ab oder legt diesen fest.
        /// </summary>
        /// <remarks>Notwendig, damit das Hinzufügen
        /// von Protokolleinträgen für WPF threadsicher wird.</remarks>
        public object Dispatcher { get; set; }

    }
}
