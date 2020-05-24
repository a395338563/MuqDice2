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
        public int RangMin;
        public int RangeMax;
        public int AreaMin;
        public int AreaMax;
        public int Damage;
        public int Displacement;
        public int CastTime;
        public int[] Buffs;
        public string Description;
        public SkillDecisionEnum Decision;
        public int Priority;
        public int Weight;
    }
}
