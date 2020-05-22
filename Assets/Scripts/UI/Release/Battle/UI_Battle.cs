/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace MuqDice.UI.Battle
{
    public partial class UI_Battle : GComponent
    {
        public GImage m_PlayerImage;
        public GList m_DiceList;
        public GTextField m_SkillName;
        public GTextField m_SkillDesc;
        public GList m_Timeline;
        public const string URL = "ui://9wo5736qief90";

        public static UI_Battle CreateInstance()
        {
            return (UI_Battle)UIPackage.CreateObject("Battle", "Battle");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_PlayerImage = (GImage)GetChildAt(1);
            m_DiceList = (GList)GetChildAt(3);
            m_SkillName = (GTextField)GetChildAt(4);
            m_SkillDesc = (GTextField)GetChildAt(5);
            m_Timeline = (GList)GetChildAt(6);
            Init();
        }
        partial void Init();
    }
}