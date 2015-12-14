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

		public static string getColorDescription(string description)
		{
			description = description.Replace("GPU", "[B98A3A][c][b]●[/b][/c][-]");
			description = description.Replace("RAM", "[419772][c][b]●[/b][/c][-]");
			description = description.Replace("CPU", "[03AED9][c][b]●[/b][/c][-]");
			description = description.Replace("UTI", "[888888][c][b]●[/b][/c][-]");
			description = description.Replace("COOL","[888888][c][b]❆[/b][/c][-]");
			
			return description;
		}
    }
}
