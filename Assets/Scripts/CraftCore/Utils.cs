using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraftCore
{
    static class Utils
    {
        static public string NameFromType(EnergyType type)
        {
            if (type == EnergyType.Blue)
            {
                return "CPU";
            }
            if (type == EnergyType.Green)
            {
                return "GPU";
            }
            if (type == EnergyType.Blue)
            {
                return "RAM";
            }
            return "error";
        }
    }
}
