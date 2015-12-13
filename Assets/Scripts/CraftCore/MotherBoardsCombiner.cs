using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CraftCore
{
    class MotherBoardsCombiner
    {
        List<EnergyType[,]> matrices = new List<EnergyType[,]>();

        public List<EnergyType[,]> Matrices
        {
            get
            {
                return matrices;
            }
        }

        EnergyType typeByString(string s)
        {
            if (s == "r") return EnergyType.Red;
            if (s == "g") return EnergyType.Green;
            if (s == "b") return EnergyType.Blue;
            if (s == "u") return EnergyType.Black;
            return EnergyType.Empty;
        }

        public MotherBoardsCombiner()
        {
            var file = Resources.Load("motherboards_layouts") as TextAsset;

            string[] separatingChars = { " ", "\r\n", "\n" };
            var chars = file.text.Split(separatingChars, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < chars.Length / 16; ++i)
            {
                EnergyType[,] types = new EnergyType[4, 4];
                for (int ii = 0; ii < 4; ++ii)
                    for (int jj = 0; jj < 4; ++jj)
                        types[ii, jj] = typeByString(chars[i * 16 + ii * 4 + jj]);
                matrices.Add(types);                
            }
            
        }
    }
}
