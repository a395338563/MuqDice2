/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace MuqDice.UI.Battle
{
    public partial class UI_Battle : GComponent
    {
        public Controller m_tip;
        public GImage m_PlayerImage;
        public GList m_DiceList;
        public GTextField m_SkillName;
        public GTextField m_SkillDesc;
        public GList m_Timeline;
        public UI_Unit m_Unit0;
        public UI_Unit m_Unit1;
        public UI_Unit m_Unit2;
        public UI_Unit m_Unit3;
        public UI_Unit m_Unit4;
        public UI_Unit m_Unit5;
        public UI_Unit m_Unit6;
        public UI_Unit m_Unit7;
        public UI_Unit m_Unit8;
        public UI_Unit m_Unit9;
        public GList m_MiniMap;
        public GTextField m_tipText;
        public const string URL = "ui://9wo5736qief90";

        public static UI_Battle CreateInstance()
        {
            return (UI_Battle)UIPackage.CreateObject("Battle", "Battle");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_tip = GetControllerAt(0);
            m_PlayerImage = (GImage)GetChildAt(2);
            m_DiceList = (GList)GetChildAt(4);
            m_SkillName = (GTextField)GetChildAt(5);
            m_SkillDesc = (GTextField)GetChildAt(6);
            m_Timeline = (GList)GetChildAt(7);
            m_Unit0 = (UI_Unit)GetChildAt(8);
            m_Unit1 = (UI_Unit)GetChildAt(9);
            m_Unit2 = (UI_Unit)GetChildAt(10);
            m_Unit3 = (UI_Unit)GetChildAt(11);
            m_Unit4 = (UI_Unit)GetChildAt(12);
            m_Unit5 = (UI_Unit)GetChildAt(13);
            m_Unit6 = (UI_Unit)GetChildAt(14);
            m_Unit7 = (UI_Unit)GetChildAt(15);
            m_Unit8 = (UI_Unit)GetChildAt(16);
            m_Unit9 = (UI_Unit)GetChildAt(17);
            m_MiniMap = (GList)GetChildAt(19);
            m_tipText = (GTextField)GetChildAt(20);
            Init();
        }
        partial void Init();
    }
}