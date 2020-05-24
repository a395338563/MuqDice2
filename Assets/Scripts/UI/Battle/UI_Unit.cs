using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using FairyGUI;

namespace MuqDice.UI.Battle
{
    partial class UI_Unit
    {
        public Unit unit;
        public void SetUnit(Unit unit)
        {
            this.unit = unit;
            if (unit != null)
            {
                m_showUnit.selectedIndex = 0;
                ResourcesManager.Instance.LoadBundle(PathHelper.UnitPath + unit.Config.Model);
                m_Image.texture = new NTexture(ResourcesManager.Instance.GetAsset<Texture>(PathHelper.UnitPath + unit.Config.Model, unit.Config.Model));
                m_Hp.max = unit.MaxHp;
                m_Hp.value = unit.Hp;
                if (unit is Enemy enemy)
                {
                    m_enemy.selectedIndex = 0;
                    m_CastingSkill.SetVar("s", enemy.NextSkill.Config._Id).FlushVars();
                }
                else
                {
                    m_enemy.selectedIndex = 1;
                }
            }
            else
            {
                m_showUnit.selectedIndex = 1;
            }
        }
    }
}
