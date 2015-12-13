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
            if (type == EnergyType.Red)
            {
                return "GPU";
            }
            if (type == EnergyType.Green)
            {
                return "RAM";
            }
            return "error";
        }
    }
}
