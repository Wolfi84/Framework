using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wolfi.Core.WPF.Anwendung.ViewModels.BasisViewModel
{
    /// <summary>
    /// Stellt die Basisfunktionalität eines Anwendungs Viewmodels zur
    /// verfügung.
    /// </summary>
    public abstract class BasisViewModel : Core.Kontext.Anwendungsobjekt
    {
        public ViewModels.Hauptfenster.HauptfensterViewModel HauptfensterManager { get; set; }
    
    }
}
