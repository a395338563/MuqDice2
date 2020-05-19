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
        public string MaxHp;
        public string StartSp;
        public int[] Skills;
    }
}
