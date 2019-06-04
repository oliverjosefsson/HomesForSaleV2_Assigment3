using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManager
{
    /// <summary>
    /// EstateManager that inherites Listmanager, with the generic type property
    /// </summary>
    [Serializable]
    public class EstateManager : ListManager<Property>
    {
    }
}
