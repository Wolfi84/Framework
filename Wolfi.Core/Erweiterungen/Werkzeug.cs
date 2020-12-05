using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Wolfi.Core.Erweiterungen
{
    /// <summary>
    /// Stellt diverse Erweiterungsmethoden bereit.
    /// </summary>
    public static class Werkzeug
    {
        /// <summary>
        /// Prüft, ob im Pfad ein Unterordner für die
        /// aktuelle Kultur existiert und hängt diesen
        /// an den Pfad an und gibt das Ergebnis zurück.
        /// </summary>
        /// <param name="pfad">Verzeichnis, in dem geprüft 
        /// werden soll, ob ein lokalisierter Unterordner existiert.</param>
        /// <remarks>Es kann nicht davon ausgegangen werden,
        /// dass der Pfad existiert.</remarks>
        public static string HoleLokalisiertenPfad(this string pfad)
        {
            var AktuelleKultur = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;

            while (!System.IO.Directory.Exists(System.IO.Path.Combine(pfad, AktuelleKultur)) && AktuelleKultur != string.Empty)
            {
                //Fallback Lokalisierung
                var LetzterBindestrich = AktuelleKultur.LastIndexOf('-');

                if (LetzterBindestrich > -1)
                {
                    AktuelleKultur = AktuelleKultur.Substring(0, LetzterBindestrich);
                }
                else
                {
                    AktuelleKultur = string.Empty;
                }
            }

            return System.IO.Path.Combine(pfad, AktuelleKultur);
        }



        /// <summary>
        /// Gibt eine absolute Pfadangabe zurück.
        /// </summary>
        /// <param name="endeTeil">Eine relative Pfadangabe</param>
        /// <param name="basisTeil">Der absolute Pfad, mit dem der Pfad beginnen soll.</param>
        /// <returns>Eine absolute Pfadangabe bestehend aus Basis- und Endeteil.</returns>
        public static string ErzeugePfad(this string endeTeil, string basisTeil)
        {

            var EndeTeile = endeTeil.Split(System.IO.Path.DirectorySeparatorChar);
            var BasisTeile = basisTeil.Split(System.IO.Path.DirectorySeparatorChar);

            var AnzahlEbenenZurück = (from t in EndeTeile where t == ".." select t).Count();

            var NeueTeile
                = (from t in BasisTeile
                   select t).Take(BasisTeile.Length - AnzahlEbenenZurück)
                .Union(
                    (from t in EndeTeile
                     select t).Skip(AnzahlEbenenZurück)
                ).ToArray();

            if (NeueTeile.Length > 0 && NeueTeile[0].EndsWith(":"))
            {
                NeueTeile[0] += System.IO.Path.DirectorySeparatorChar;
            }

            return System.IO.Path.Combine(NeueTeile);
        }

    }
}
