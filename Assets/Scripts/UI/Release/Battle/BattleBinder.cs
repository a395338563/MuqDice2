/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;

namespace MuqDice.UI.Battle
{
    public class BattleBinder
    {
        public static void BindAll()
        {
            UIObjectFactory.SetPackageItemExtension(UI_Battle.URL, typeof(UI_Battle));
            UIObjectFactory.SetPackageItemExtension(UI_Dice.URL, typeof(UI_Dice));
            UIObjectFactory.SetPackageItemExtension(UI_Unit.URL, typeof(UI_Unit));
            UIObjectFactory.SetPackageItemExtension(UI_MiniMap.URL, typeof(UI_MiniMap));
        }
    }
}