﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuqDice
{
    public class Factory
    {
        public static Skill Get(Unit unit, int configId)
        {
            Skill skill = new Skill(unit)
            {
                ConfigId = configId,
            };
            return skill;
        }
    }
}
