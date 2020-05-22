using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuqDice
{
    public class Skill
    {
        public Skill(Unit unit)
        {
            this.Unit = unit;
        }
        public Battle Battle => Unit.Battle;
        public Unit Unit;
        public int ConfigId;
        public SkillConfig Config => Database.Instance.Get<SkillConfig>(ConfigId);

        public int TargetPos;

        public void Cast(int pos)
        {
            TargetPos = pos;
            if (Config.CastTime == 0)
            {
                DoEffect();
            }
            else
            {
                Unit.CastSkill(this);
            }
        }

        public List<int> GetUseRange(int pos)
        {
            List<int> result = new List<int>();
            for (int i = -Config.Range; i <= Config.Range; i++)
            {
                if (pos + i < 0 || pos + i >= Battle.Units.Length) continue;
                result.Add(pos + i);
            }
            return result;
        }

        public List<int> GetEffectArea(int pos)
        {
            List<int> result = new List<int>();
            for (int i = -Config.Area; i <= Config.Area; i++)
            {
                if (pos + i < 0 || pos + i >= Battle.Units.Length) continue;
                result.Add(pos + i);
            }
            return result;
        }

        public bool Useable(int pos)
        {
            return GetEffectArea(pos).Any(x => Battle.Units[x] != null && Effective(Battle.Units[x]));
        }

        public void DoEffect()
        {
            foreach (var pos in GetEffectArea(TargetPos))
            {
                Unit target = Battle.Units[pos];
                if (target == null || !Effective(target)) continue;
                effect(target);
            }
        }

        protected void effect(Unit target)
        {
            target.Damage(Config.Damage);
        }

        public bool Effective(Unit unit)
        {
            return true;
        }
    }
}
