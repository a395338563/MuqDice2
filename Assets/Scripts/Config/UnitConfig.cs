using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuqDice
{
    public class UnitConfig : IConfig
    {
        public string _Id { get; set; }
        public string Model;
        public int MaxHp;
        public int StartSp;
        public int[] Skills;
    }
}
