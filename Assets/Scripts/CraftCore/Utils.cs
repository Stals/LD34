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
            if (type == EnergyType.Black)
            {
                return "COOL";
            }
            return "error";
        }

		public static string getColorDescription(string description)
		{
			description = description.Replace("GPU", "[FF0000][c][b]●[/b][/c][-]");
			description = description.Replace("RAM", "[00FF00][c][b]●[/b][/c][-]");
			description = description.Replace("CPU", "[19C6FF][c][b]●[/b][/c][-]");
			description = description.Replace("UTI", "[888888][c][b]●[/b][/c][-]");
			
			return description;
		}
    }
}
