﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Wolfi.Core.Erweiterungen
{    /// <summary>
     /// Stellt Erweiterungsmethoden zum Abrufen
     /// der AssemblyInfo Attribute bereit.
     /// </summary>
    public static class AssemblyInfo
    {

        /// <summary>
        /// Gibt die Einstellung des 
        /// System.Reflection.AssemblyCompanyAttributes 
        /// der Hauptassembly zurück.
        /// </summary>
        /// <param name="aktuellesObjekt">Verweis auf das Objekt, für
        /// das diese Erweiterung benutzt wird.</param>
        public static string HoleFirmenname(this object aktuellesObjekt)
        {

            var Daten = System.Reflection.Assembly.GetEntryAssembly()
                .GetCustomAttributes(typeof(System.Reflection.AssemblyCompanyAttribute), inherit: false);
            if (Daten.Length > 0)
            {
                return ((System.Reflection.AssemblyCompanyAttribute)Daten[0]).Company;
            }

            return string.Empty;
        }


        /// <summary>
        /// Gibt die Einstellung des 
        /// System.Reflection.AssemblyProductAttributes 
        /// der Hauptassembly zurück.
        /// </summary>
        /// <param name="aktuellesObjekt">Verweis auf das Objekt, für
        /// das diese Erweiterung benutzt wird.</param>
        public static string HoleProdukt(this object aktuellesObjekt)
        {

            var Daten = System.Reflection.Assembly.GetEntryAssembly()
                .GetCustomAttributes(
                    typeof(System.Reflection.AssemblyProductAttribute),
                    inherit: false);

            if (Daten.Length > 0)
            {
                return ((System.Reflection.AssemblyProductAttribute)Daten[0]).Product;
            }

            return string.Empty;
        }

        /// <summary>
        /// Gibt die Haupt- und Nebenversion der Hauptassembly zurück.
        /// </summary>
        /// <param name="aktuellesObjekt">Verweis auf das Objekt, für
        /// das diese Erweiterung benutzt wird.</param>
        public static string HoleVersion(this object aktuellesObjekt)
        {

            var Version = System.Reflection.Assembly
                .GetEntryAssembly().GetName().Version;

            return $"{Version.Major}.{Version.Minor}";
        }

        /// <summary>
        /// Gibt die Einstellung des 
        /// System.Reflection.AssemblyTitleAttributes 
        /// der Hauptassembly zurück.
        /// </summary>
        /// <param name="aktuellesObjekt">Verweis auf das Objekt, für
        /// das diese Erweiterung benutzt wird.</param>
        public static string HoleTitel(this object aktuellesObjekt)
        {

            var Daten = System.Reflection.Assembly.GetEntryAssembly()
                .GetCustomAttributes(
                    typeof(System.Reflection.AssemblyTitleAttribute),
                    inherit: false);

            if (Daten.Length > 0)
            {
                return ((System.Reflection.AssemblyTitleAttribute)Daten[0]).Title;
            }

            return string.Empty;
        }

        /// <summary>
        /// Gibt die Einstellung des 
        /// System.Reflection.AssemblyCopyrightAttributes 
        /// der Hauptassembly zurück.
        /// </summary>
        /// <param name="aktuellesObjekt">Verweis auf das Objekt, für
        /// das diese Erweiterung benutzt wird.</param>
        public static string HoleCopyright(this object aktuellesObjekt)
        {

            var Daten = System.Reflection.Assembly.GetEntryAssembly()
                .GetCustomAttributes(
                    typeof(System.Reflection.AssemblyCopyrightAttribute),
                    inherit: false);

            if (Daten.Length > 0)
            {
                return ((System.Reflection.AssemblyCopyrightAttribute)Daten[0]).Copyright;
            }

            return string.Empty;
        }
    }

}
