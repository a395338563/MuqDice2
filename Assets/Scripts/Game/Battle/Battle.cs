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

        public int Round;

        public Battle(BattleConfig battleConfig)
        {
            Random = new Random();
            Units = new Unit[battleConfig.TeamConfig.MapSize];
            Muq = new Muq(this, battleConfig.Player);
            Units[battleConfig.TeamConfig.PlayerPos] = Muq;
            for (int i = 0; i < battleConfig.TeamConfig.UnitIds.Length; i++)
            {
                Units[battleConfig.TeamConfig.UnitPos[i]] = new Enemy(this, battleConfig.TeamConfig.UnitIds[i]);
            }
            NextRound();
            foreach (var unit in Units)
            {
                if (unit!=null && unit is Enemy enemy)
                {
                    enemy.DecideNextSkill();
                }
            }
        }

        public void Throw(List<int> dices,int pos)
        {
            Muq.Cast(dices, pos);
            Turn();
        }

        public void Turn()
        {
            var maxSp = Units.Max(x => x == null ? -1 : x.Sp);
            if (maxSp <= 0)
            {
                NextRound();
            }
            Unit unit = Units.FirstOrDefault(x => x != null && x.Alve() && x.Sp == maxSp);
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

        void NextRound()
        {
            Round++;
            foreach (var unit in Units)
            {
                if (unit!=null && unit.Alve())
                {
                    unit.Sp += unit.Config.StartSp;
                }
            }
        }
    }
}
