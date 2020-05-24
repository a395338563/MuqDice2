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

        public int ConfigId;
        public virtual UnitConfig Config => Database.Instance.Get<UnitConfig>(ConfigId);

        public int MaxHp;
        public int Hp;
        public int Sp;

        public List<Skill> Skills = new List<Skill>();
        public List<Buff> Buffs = new List<Buff>();
        public Skill CastingSkill;

        public int Pos => Array.IndexOf(Battle.Units, this);

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

        public bool Alve()
        {
            return Hp > 0;
        }
        public void MoveTo(int pos)
        {
            int selfPos = this.Pos;
            var targetUnit = Battle.Units[pos];
            Battle.Units[pos] = this;
            Battle.Units[selfPos] = targetUnit;
        }

    }
}
