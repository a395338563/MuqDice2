/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace MuqDice.UI.Battle
{
    public partial class UI_Unit : GComponent
    {
        public Controller m_showUnit;
        public Controller m_enemy;
        public Controller m_Effected;
        public GLoader m_Image;
        public GProgressBar m_Hp;
        public GTextField m_CastingSkill;
        public const string URL = "ui://9wo5736qief95";

        public static UI_Unit CreateInstance()
        {
            return (UI_Unit)UIPackage.CreateObject("Battle", "Unit");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_showUnit = GetControllerAt(0);
            m_enemy = GetControllerAt(1);
            m_Effected = GetControllerAt(2);
            m_Image = (GLoader)GetChildAt(1);
            m_Hp = (GProgressBar)GetChildAt(2);
            m_CastingSkill = (GTextField)GetChildAt(3);
            Init();
        }
        partial void Init();
    }
}