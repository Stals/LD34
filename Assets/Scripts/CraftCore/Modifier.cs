using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraftCore
{
    interface IModifier
    {
        void Modify(Motherboard board, int x, int y, Card card);
    }


}
