using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryBistro
{
    internal interface IDish
    {
        string Name { get; }
        Manager.Menu Type { get; }
    }
}
