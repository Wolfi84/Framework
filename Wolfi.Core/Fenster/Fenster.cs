using System;
using System.Collections.Generic;
using System.Text;

namespace Wolfi.Core.Fenster
{

    /// <summary>
    /// Stellt eine Liste von Anwendungsfenstern bereit.
    /// </summary>
    /// <remarks>Hier wird eine generische Liste vom .Net
    /// für "Fenster"-Objekte "typisiert.</remarks>
    public class FensterListe : System.Collections.Generic.List<Core.Fenster.Fenster>
    {

        /// <summary>
        /// Gibt das Fenster mit dem gesuchten Namen zurück.
        /// </summary>
        /// <param name="name">Bezeichnung des Fensters.</param>
        /// <returns>Null, falls das Fenster nicht exisitert.</returns>
        /// <remarks>Demonstriert den Einsatz anonymer Methoden.</remarks>
        public Fenster Suchen(string name)
        {
            return this.Find(f => f.Name == name);
        }

    }


    /// <summary>
    /// Stellt Information über ein Anwendungsfenster bereit.
    /// </summary>
    /// <remarks>Hier handelt es sich um ein Datentransferobjekt.</remarks>
    public class Fenster
    {

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string _Name = string.Empty; 

        
        /// <summary>
        /// Ruft die Bezeichnung des Fensters
        /// ab oder legt diese fest.
        /// </summary>
        /// <remarks>Wird als Schlüssel zum Wiederfinden benutzt.</remarks>
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }


        /// <summary>
        /// Ruft den Zustand des Fensters ab
        /// oder legt diesen fest.
        /// </summary>
        public int Zustand { get; set; }


        /// <summary>
        /// Ruft die linke Fensterposition ab 
        /// oder legt diese fest.
        /// </summary>
        /// <remarks>Standardwert null</remarks>
        public int? Links { get; set; }

        
        /// <summary>
        /// Ruft die obere Fensterposition ab
        /// oder legt diese fest.
        /// </summary>
        /// <remarks>Standardwert null</remarks>
        public int? Oben { get; set; }

        /// <summary>
        /// Ruft die Breite des Fensters ab
        /// oder legt diese fest.
        /// </summary>
        /// <remarks>Standardwert null</remarks>
        public int? Breite { get; set; }

        /// <summary>
        /// Ruft die Höhe des Fensters ab
        /// oder legt diese fest.
        /// </summary>
        /// <remarks>Standardwert null</remarks>
        public int? Höhe { get; set; }

        /// <summary>
        /// Gibt eine Zeichenfolge zurück, die dieses Fenster beschreibt.
        /// </summary>
        public override string ToString()
        {
            return $"{this.GetType().Name}(Name=\"{this.Name}\")";
        }
    }
}
