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
            for (int i = Config.RangMin; i <= Config.RangeMax; i++)
            {
                if (pos + i >= 0 && pos + i < Battle.Units.Length)
                {
                    result.Add(pos + i);
                }
                if (pos - i >= 0 && pos - i < Battle.Units.Length)
                {
                    result.Add(pos - i);
                }
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
            if (Config.Displacement != 0)
            {
                int forward = Math.Sign(TargetPos - Unit.Pos);
                Unit.MoveTo(Unit.Pos + forward * Config.Displacement);
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

        public int DecideUsePos(Muq muq)
        {
            int muqPos = muq.Pos;
            List<int> useRange = GetUseRange(Unit.Pos);
            switch (Config.Decision)
            {
                case SkillDecisionEnum.Fit:
                    useRange.Sort((x, y) => Math.Abs(x - muqPos) - Math.Abs(y - muqPos));
                    foreach (var pos in useRange)
                    {
                        if (GetEffectArea(pos).Contains(muqPos))
                        {
                            return pos;
                        } 
                    }
                    break;
                case SkillDecisionEnum.Far:
                    useRange.Sort((x, y) => Math.Abs(y - muqPos) - Math.Abs(x - muqPos));
                    return useRange[0];
                case SkillDecisionEnum.Close:
                    useRange.Sort((x, y) => Math.Abs(x - muqPos) - Math.Abs(y - muqPos));
                    return useRange[0];
            }
            return -1;
        }
    }
}
