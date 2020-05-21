using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuqDice 
{
    public class Enemy : Unit
    {
        public Enemy(Battle battle, int configId) : base(battle)
        {

        }

        public int ConfigId;
        public UnitConfig Config => Database.Instance.Get<UnitConfig>(ConfigId);
    }
}
