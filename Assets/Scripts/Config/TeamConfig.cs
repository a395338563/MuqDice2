using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuqDice
{
    public class TeamConfig : IConfig
    {
        public string _Id { get; set; }
        public int MapSize;
        public int[] UnitIds;
        public int[] UnitPos;
        public int PlayerPos;
        public string[] Events;
    }
}
