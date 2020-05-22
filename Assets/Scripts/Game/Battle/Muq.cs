using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuqDice
{
    public class Muq : Unit
    {
        public Skill TempSkill;
        public List<Dice> Dices;
        public int Pos => Array.IndexOf(Battle.Units, this);

        public Muq(Battle battle, Player player):base(battle)
        {
            Dices = new List<Dice>();
        }

        public List<int> GetSkillRange()
        {
            return TempSkill.GetUseRange(Pos);
        }

        public List<int> GetSkillArea(int pos)
        {
            return TempSkill.GetEffectArea(pos);
        }

        public void Throw(List<int> dice)
        {
            Sp -= dice.Count;
            List<ElementEnum> elements = new List<ElementEnum>();
            foreach (var index in dice)
            {
                elements.AddRange(Dices[index].NowFaceConfig.Elements);
                Dices[index].ReRoll();
            }
            var magicConfig = MagicHelper.GetMagic(elements);
            if (magicConfig != null)
            {
                TempSkill = GetSkill(magicConfig.SkillId);
            }
        }

        protected Skill GetSkill(int skillId)
        {
            var skill = Skills.Find(x => x.Config == Database.Instance.Get<SkillConfig>(skillId));
            if (skill == null)
            {
                skill = Factory.Get(this, skillId);
                Skills.Add(skill);
            }
            return skill;
        }
    }
}
