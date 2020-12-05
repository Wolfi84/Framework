using System;
using System.Collections.Generic;
using System.Text;

namespace Wolfi.Core.Kontext
{
    /// <summary>
    /// Bestätigt, dass das Objekt die Infrastruktur bereitstellt.
    /// </summary>
    public interface IAppKontext
    {
        /// <summary>
        /// Ruft das Infrastruktur Objekt ab oder liegt dieses fest.
        /// </summary>
        Anwendungskontext AppKontext { get; set; }
    }
}
