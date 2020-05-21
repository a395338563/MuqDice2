using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuqDice
{
    public class Muq : Unit
    {
        public Muq(Battle battle, Player player):base(battle)
        {
            Dices = new List<Dice>();
        }

        public List<Dice> Dices;

        public List<int> GetSkillRange()
        {
            throw new Exception();
        }

        public List<int> GetSkillArea(int pos)
        {
            throw new Exception();
        }

        public void Throw(List<int> dice)
        {

        }
    }
}
