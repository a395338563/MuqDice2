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

        public void DoEffect()
        {

        }
    }
}
