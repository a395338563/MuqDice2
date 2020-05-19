using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuqDice
{
    public class RandomDiceConfig:IConfig
    {
        public string _Id { get; set; }
        public int Sum;
        public int[] FaceQualitys;
        public int Weight;
    }
}
