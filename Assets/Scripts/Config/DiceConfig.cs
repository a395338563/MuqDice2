using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuqDice
{
    public class DiceConfig : IConfig
    {
        public string _Id { get; set; }
        public int QualityAll;
        public int[] Faces;
        public int[] Qualitys;
    }
}
