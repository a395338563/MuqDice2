using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuqDice 
{
    public class Enemy : Unit
    {
        public Enemy(Battle battle, int configId) : base(battle)
        {
            this.ConfigId = configId;
            MaxHp = Config.MaxHp;
            Hp = MaxHp;
            Skills = Config.Skills.Select(x => Factory.Get(this, x)).ToList();
        }

        public Skill NextSkill;

        public int NextSkillPos;

        public void DecideNextSkill()
        {
            Dictionary<int, Skill> skills = new Dictionary<int, Skill>();
            foreach (var skill in Skills)
            {
                var pos = skill.DecideUsePos(Battle.Muq);
                if (pos != -1) skills.Add(pos, skill);
            }
            if (skills.Count == 0) throw new Exception($"{Config._Id}没有能放的技能");
            var p = skills.Max(x => x.Value.Config.Priority);
            var maxSkills = skills.Where(x => x.Value.Config.Priority == p);
            int sum = maxSkills.Sum(x => x.Value.Config.Weight);
            int r = Battle.Random.Next(0, sum);
            foreach (var kv in maxSkills)
            {
                if (r < kv.Value.Config.Weight)
                {
                    NextSkillPos = kv.Key;
                    NextSkill = kv.Value;
                    return;
                }
                r -= kv.Value.Config.Weight;
            }
            throw new Exception("所有技能权重为0");
        }
    }
}
