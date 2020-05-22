using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuqDice
{
    public class Battle
    {
        public Muq Muq;

        public Unit[] Units;

        public Random Random;

        public bool End;

        public bool Win;

        public Battle(BattleConfig battleConfig)
        {
            Units = new Unit[battleConfig.TeamConfig.MapSize];
            Muq = new Muq(this, battleConfig.Player);
            Units[battleConfig.TeamConfig.PlayerPos] = Muq;
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

        public void CheckEnd()
        {
            if (Units.All(x => x == null ? true : !x.Alve())) { End = true; Win = true; }
            if (!Muq.Alve()) { End = true; Win = false; }
        }
    }
}
