using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuqDice
{
    public class Unit
    {
        public int MaxHp;
        public int Hp;
        public int Sp;

        public List<Skill> Skills;
        public List<Buff> Buffs;

        public void CastSkill(Skill skill)
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
