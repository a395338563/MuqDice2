using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

namespace MuqDice.UI.Battle
{
    public partial class UI_Battle
    {
        public MuqDice.Battle Battle;
        public UI_Unit[] Units;
        public List<int> SelectDice = new List<int>();
        Skill tempSkill;
        float magicChangeTime = 0;
        public int mouseOverPos;
        partial void Init()
        {
            Units = new UI_Unit[10];
            for (int i = 0; i < 10; i++)
            {
                Units[i] = GetChild("Unit" + i) as UI_Unit;
                int k = i;
                Units[i].onRollOver.Add((x) =>
                {
                    mouseOverPos = k;
                });
                Units[i].onRollOut.Add((x) =>
                {
                    mouseOverPos = -1;
                });
                Units[i].onClick.Add(() =>
                {
                    if (tempSkill != null)
                    {
                        Battle.Throw(SelectDice, k);
                        SelectDice.Clear();
                        changeMagic();
                    }
                });
            }
            m_DiceList.onClickItem.Add(x =>
            {
                var item = x.data as GObject;
                int index = m_DiceList.GetChildIndex(item);
                if (SelectDice.Contains(index)) SelectDice.Remove(index);
                else SelectDice.Add(index);
                magicChangeTime = 0.5f;
            });
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            flushView();
            if (magicChangeTime > 0)
            {
                magicChangeTime -= Time.deltaTime;
                if (magicChangeTime <= 0)
                {
                    changeMagic();
                }
            }
        }

        public void SetBattle(MuqDice.Battle battle)
        {
            this.Battle = battle;
            for (int i = 0; i < Units.Length; i++)
            {
                Units[i].visible = (i < battle.Units.Length);
            }
            m_MiniMap.RemoveChildrenToPool();
            for (int i = 0; i < battle.Units.Length; i++)
            {
                m_MiniMap.AddItemFromPool();
            }
            battle.Turn();
            //flushView();
        }

        public void flushView()
        {
            if (Battle == null) return;
            for (int i = 0; i < m_Timeline.numChildren; i++)
            {
                m_Timeline.GetChildAt(i).text = "";
            }

            for (int i = 0; i < Battle.Units.Length; i++)
            {
                Unit unit = Battle.Units[i];
                UI_MiniMap _MiniMap = m_MiniMap.GetChildAt(i) as UI_MiniMap;
                if (unit == null)
                {
                    _MiniMap.m_info.selectedIndex = 0;
                }
                else
                {
                    m_Timeline.GetChildAt(m_Timeline.numChildren - unit.Sp - 1).text = unit.Config._Id;
                    if (unit == Battle.Muq) _MiniMap.m_info.selectedIndex = 2;
                    else _MiniMap.m_info.selectedIndex = 1;
                }
                Units[i].SetUnit(unit);
            }

            m_DiceList.RemoveChildrenToPool();
            for (int i = 0; i < Battle.Muq.Dices.Count; i++)
            {
                Dice dice = (Dice)Battle.Muq.Dices[i];
                var uiDice = m_DiceList.AddItemFromPool() as UI_Dice;
                uiDice.SetDice(dice);
                uiDice.selected = SelectDice.Contains(i);
            }

            for (int i = 0; i < Units.Length; i++)
            {
                Units[i].m_Effected.selectedIndex = 0;
                m_tip.selectedIndex = 0;
            }
            if (tempSkill != null)
            {
                m_tipText.SetVar("s", tempSkill.Config._Id).FlushVars();
                if (mouseOverPos != -1)
                {
                    var effectArea = tempSkill.GetEffectArea(mouseOverPos);
                    for (int i = 0; i < Units.Length; i++)
                    {
                        Units[i].m_Effected.selectedIndex = effectArea.Contains(i) ? 1 : 0;
                    }
                    m_tip.selectedIndex = 2;
                }
                else
                {
                    var effectRange = tempSkill.GetUseRange(Battle.Muq.Pos);
                    for (int i = 0; i < Units.Length; i++)
                    {
                        Units[i].m_Effected.selectedIndex = effectRange.Contains(i) ? 1 : 0;
                    }
                    m_tip.selectedIndex = 1;
                }
            }
            else if (mouseOverPos != -1)
            {
                var watchingUnit = Units[mouseOverPos].unit;
                if (watchingUnit != null && watchingUnit is Enemy enemy)
                {
                    var effectArea = enemy.NextSkill.GetEffectArea(enemy.NextSkillPos);
                    for (int i = 0; i < Units.Length; i++)
                    {
                        Units[i].m_Effected.selectedIndex = effectArea.Contains(i) ? 1 : 0;
                    }
                }
            }
        }

        void changeMagic()
        {
            Battle.Muq.predictMagic(SelectDice);
            tempSkill = Battle.Muq.TempSkill;
            if (Battle.Muq.TempSkill != null)
            {
                m_SkillName.text = tempSkill.Config._Id;
                m_SkillDesc.text = tempSkill.Config.Description;
            }
            else
            {
                m_SkillName.text = "";
                m_SkillDesc.text = "";
            }
        }
    }
}