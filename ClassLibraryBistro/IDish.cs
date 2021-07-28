using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryBistro
{
    interface IDish
    {
        string Name { get; }
        Manager.Menu Type { get; }
    }
}
