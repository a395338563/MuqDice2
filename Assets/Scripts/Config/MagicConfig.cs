﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuqDice
{
    public class MagicConfig : IConfig
    {
        public string _Id { get; set; }
        public ElementEnum[] Elements;
        public int SkillId;
    }
}
