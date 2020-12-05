using System;
using System.Collections.Generic;
using System.Text;

namespace Wolfi.Core.Generisch
{
    /// <summary>
    /// Stellt einen Dienst zum Speichern und Lesen
    /// von Auflistungen in und aus einer 
    /// Xml-Datei bereit.
    /// </summary>
    public class XmlController<T> : Core.Kontext.Anwendungsobjekt
    {

        /// <summary>
        /// Schreibt die Daten im Xml Format
        /// in die angegebene Datei.
        /// </summary>
        /// <param name="pfad">Vollständiger Name der Datei,
        /// die benutzt werden soll.</param>
        /// <param name="daten">Die Liste mit den Informationen,
        /// die als Xml gespeichert werden sollen.</param>
        /// <exception cref="System.Exception">Wird ausgelöst,
        /// wenn das Speichern nicht erfolgreich war.</exception>
        public void Speichern(string pfad, T daten)
        { 
            using (var Schreiber = new System.IO.StreamWriter(pfad))
            {
                var Serialisierer = new System.Xml.Serialization.XmlSerializer(daten.GetType());
                Serialisierer.Serialize(Schreiber, daten);

            }
        }

        /// <summary>
        /// Gibt die Daten aus der Xml Datei zurück.
        /// </summary>
        /// <param name="pfad">Vollständiger Name der Datei,
        /// die benutzt werden soll.</param>
        /// <exception cref="System.Exception">Wird ausgelöst, wenn
        /// das Lesen nicht erfolgreich ist.</exception>
        public T Lesen(string pfad)
        {

            T Ergebnis = default(T);

            using (var Leser = new System.IO.StreamReader(pfad))
            {
                var Serialisierer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                Ergebnis = (T)Serialisierer.Deserialize(Leser);
            }

            return Ergebnis;
        }

    }

}
