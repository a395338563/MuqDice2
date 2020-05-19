using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuqDice
{
    public class SkillConfig:IConfig
    {
        public string _Id { get; set; }
        public string Type;
        public int Range;
        public int Area;
        public int Damage;
    }
}
