/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace MuqDice.UI.Battle
{
    public partial class UI_Dice : GButton
    {
        public Controller m_count;
        public GTextField m_t0;
        public GTextField m_t1;
        public const string URL = "ui://9wo5736qief91";

        public static UI_Dice CreateInstance()
        {
            return (UI_Dice)UIPackage.CreateObject("Battle", "Dice");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_count = GetControllerAt(0);
            m_t0 = (GTextField)GetChildAt(1);
            m_t1 = (GTextField)GetChildAt(2);
            Init();
        }
        partial void Init();
    }
}