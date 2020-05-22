/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace MuqDice.UI.Battle
{
    public partial class UI_Dice : GComponent
    {
        public Controller m_element;
        public const string URL = "ui://9wo5736qief91";

        public static UI_Dice CreateInstance()
        {
            return (UI_Dice)UIPackage.CreateObject("Battle", "Dice");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_element = GetControllerAt(0);
            Init();
        }
        partial void Init();
    }
}