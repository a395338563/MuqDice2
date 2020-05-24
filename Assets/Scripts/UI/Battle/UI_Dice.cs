using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuqDice.UI.Battle
{
    partial class UI_Dice
    {
        static readonly string[] Text = new string[] { "金", "木", "水", "火", "土", "日", "月" };
        public void SetDice(Dice dice)
        {
            var config = dice.GetFrontFace();
            m_count.selectedIndex = config.Elements.Length - 1;
            for (int i = 0; i < config.Elements.Length; i++)
            {
                GetChild("t" + i).text = Text[(int)config.Elements[i]];
            }
        }
    }
}
