﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraftCore
{
    public interface IModifier
    {
        void Modify(Motherboard board, CardOnBoard card);
    }



}
