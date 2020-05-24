/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace MuqDice.UI.Battle
{
    public partial class UI_MiniMap : GComponent
    {
        public Controller m_info;
        public const string URL = "ui://9wo5736qp9w67";

        public static UI_MiniMap CreateInstance()
        {
            return (UI_MiniMap)UIPackage.CreateObject("Battle", "MiniMap");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_info = GetControllerAt(0);
            Init();
        }
        partial void Init();
    }
}