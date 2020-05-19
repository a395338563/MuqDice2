using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuqDice
{
    public class DiceFaceConfig : IConfig
    {
        public string _Id { get; set; }
        public int Quality;
        public ElementEnum[] Elements;
    }
}
