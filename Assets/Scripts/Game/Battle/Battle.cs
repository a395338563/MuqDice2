using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuqDice
{
    public class Battle
    {
        public Unit[] Units;

        public Random Random;

        public Battle(BattleConfig battleConfig)
        {
            Units = new Unit[battleConfig.TeamConfig.MapSize];
            Muq muq = new Muq(this, battleConfig.Player);
            Units[battleConfig.TeamConfig.PlayerPos] = muq;
            for (int i = 0; i < battleConfig.TeamConfig.UnitIds.Length; i++)
            {
                Units[battleConfig.TeamConfig.UnitPos[i]] = new Enemy(this, battleConfig.TeamConfig.UnitPos[i]);
            }
        }

        public void Turn()
        {
            var maxSp = Units.Max(x => x == null ? -1 : x.Sp);
            Unit unit = Units.FirstOrDefault(x => x != null && x.Sp == maxSp);
            unit.DoTurn();
        }

        public int RandomRange(int min,int max)
        {
            return Random.Next(min, max);
        }
    }
}
