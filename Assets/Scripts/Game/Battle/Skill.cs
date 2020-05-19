using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuqDice
{
    public class Skill
    {
        public int ConfigId;
        public SkillConfig Config => Database.Instance.Get<SkillConfig>(ConfigId);

        public void Cast(int pos)
        {

        }

        public void Effect(int pos)
        {

        }
    }
}
