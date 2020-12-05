using System;
using System.Collections.Generic;
using System.Text;

namespace Wolfi.Core.Sprachen
{
    /// <summary>
    /// Stellt einen Dienst zum Lesen und Schreiben
    /// von Anwendungssprachen aus bzw. in eine Xml Datei bereit.
    /// </summary>
    internal class SprachenXmlController : Generisch.XmlController<Sprachen.SpracheListe>
    {

        /// <summary>
        /// Gibt die Sprachen aus den Ressourcen zurück.
        /// </summary>
        public Sprachen.SpracheListe HoleStandardsprachen()
        {
            var Xml = new System.Xml.XmlDocument();
            var Ergebnis = new Sprachen.SpracheListe();

            Xml.LoadXml(Core.Properties.Resources.Sprachen);

            foreach (System.Xml.XmlNode s in Xml.DocumentElement.ChildNodes)
            {
                Ergebnis.Add(
                    new Core.Sprachen.Sprache
                    {
                        Code = s.Attributes["code"].Value,
                        Name = s.Attributes["name"].Value
                    }
                    );

            }

            return Ergebnis;
        }

    }

}
