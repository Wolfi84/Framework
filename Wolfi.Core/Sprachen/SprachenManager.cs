﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Wolfi.Core.Sprachen
{
    /// <summary>
    /// Stellt einen Dienst zum Verwalten der Anwendungssprachen bereit.
    /// </summary>
    public class SprachenManager : Core.Kontext.Anwendungsobjekt
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private Core.Sprachen.SprachenXmlController _Controller = null;


        /// <summary>
        /// Ruft den Dienst zum Verwalten der
        /// externen Sprachen ab.
        /// </summary>
        private Core.Sprachen.SprachenXmlController Controller
        {
            get
            {
                if (this._Controller == null)
                {
                    this._Controller
                        = this.AppKontext.Erzeuge<Core.Sprachen.SprachenXmlController>();

                }

                return this._Controller;
            }
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private Core.Sprachen.Sprache[] _Liste = null;
        /// <summary>
        /// Ruft die unterstützten Sprachen der Anwendung ab.
        /// </summary>
        public Sprachen.Sprache[] Liste
        {

            get
            {
                if (this._Liste == null)
                {
                    this._Liste = (from s in this.Controller.HoleStandardsprachen()
                                   orderby s.Name
                                   select s).ToArray();
                }

                return this._Liste;
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private Sprachen.Sprache _AktuelleSprache = null;

        /// <summary>
        /// Ruft die derzeitige Anwendungssprache
        /// ab oder legt diese fest.
        /// </summary>
        /// <remarks>Als Standard wird die erste Sprache
        /// der Liste benutzt.</remarks>
        public Sprachen.Sprache AktuelleSprache
        {
            get
            {
                if (this._AktuelleSprache == null)
                {
                    this._AktuelleSprache = this.Liste[0];
                }

                return this._AktuelleSprache;
            }
            set
            {
                this._AktuelleSprache = value;
            }
        }

        /// <summary>
        /// Setzt die aktuelle Sprache auf die
        /// Sprache mit dem angegeben Code.
        /// </summary>
        /// <param name="code">Microsoft Kürzel der Sprache,
        /// die zur aktuellen Sprache werden soll.</param>
        /// <remarks>Wird die Sprache nicht gefunden,
        /// wird die erste Sprache der Liste benutzt.
        /// Die Groß-/Kleinschreibung beim Suchen wird
        /// nicht berücksichtigt.</remarks>
        public void Festlegen(string code)
        {
            this.AktuelleSprache = (from s in this.Liste where string.Compare(s.Code, code, ignoreCase: true) == 0 select s).FirstOrDefault();
        }

        /// <summary>
        /// Stellt sicher, dass aktuelle Daten geliefert werden.
        /// </summary>
        public void Aktualisieren()
        {
            //Den Cache Leeren...
            this._Liste = null;

        }
    }
}
