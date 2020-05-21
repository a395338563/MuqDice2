using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuqDice
{
    public class Unit
    {
        public Unit(Battle battle)
        {
            this.Battle = battle;
        }
        public Battle Battle;
        public int MaxHp;
        public int Hp;
        public int Sp;

        public List<Skill> Skills;
        public List<Buff> Buffs;
        public Skill CastingSkill;

        public void CastSkill(Skill skill)
        {
            Sp -= skill.Config.CastTime;
            CastingSkill = skill;
        }

        public void DoTurn()
        {
            if (CastingSkill != null)
            {
                CastingSkill.DoEffect();   
            }
            else
            {
                doAction();
            }
        }

        protected virtual void doAction()
        {

        }

        public int Damage(int damage)
        {
            Hp -= damage;
            if (Hp < 0) Hp = 0;
            return damage;
        }
    }
}
