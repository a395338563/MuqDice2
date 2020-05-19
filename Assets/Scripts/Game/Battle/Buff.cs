using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuqDice
{
    public class Buff
    {
        public int ConfigId;
        public BuffConfig Config => Database.Instance.Get<BuffConfig>(ConfigId);
    }
}
