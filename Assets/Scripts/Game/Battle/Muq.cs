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
        public override UnitConfig Config => Database.Instance.Get<UnitConfig>(0);

        public Muq(Battle battle, Player player):base(battle)
        {
            Dices = player.Dices.Select(x => new Dice(battle, x)).ToList();
            MaxHp = Config.MaxHp;
            Hp = MaxHp;
        }

        public List<int> GetSkillRange()
        {
            return TempSkill.GetUseRange(Pos);
        }

        public List<int> GetSkillArea(int pos)
        {
            return TempSkill.GetEffectArea(pos);
        }

        public void predictMagic(List<int> dice)
        {
            if (dice.Count == 0)
            {
                TempSkill = null;
                return;
            }
            List<ElementEnum> elements = new List<ElementEnum>();
            foreach (var index in dice)
            {
                elements.AddRange(Dices[index].NowFaceConfig.Elements);
            }
            var magicConfig = MagicHelper.GetMagic(elements);
            if (magicConfig != null)
            {
                TempSkill = GetSkill(magicConfig.SkillId);
            }
            else
            {
                TempSkill = null;
            }
        }

        public void Cast(List<int> dice,int pos)
        {
            if (dice.Count == 0)
            {
                return;
            }
            Sp -= dice.Count;
            predictMagic(dice);
            foreach (var index in dice)
            {
                Dices[index].ReRoll();
            }
            TempSkill.Cast(pos);
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
