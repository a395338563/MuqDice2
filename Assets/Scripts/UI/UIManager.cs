using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using FairyGUI;
using MuqDice.UI.Battle;

namespace MuqDice
{
    public class UIManager : MonoBehaviour
    {
        const string PackagePath = "Assets/Bundles/UI/";
        List<string> LoadedPackages = new List<string>();

        public string TeamId;
        public List<int> Dices = new List<int>();

        private void Awake()
        {
            BattleBinder.BindAll();
            LoadPackge("Battle");
        }

        private async void Start()
        {
            try
            {
                UI_Battle uI_Battle = UIPackage.CreateObject("Battle", "Battle") as UI_Battle;
                uI_Battle.SetSize(GRoot.inst.x, GRoot.inst.y);
                uI_Battle.AddRelation(GRoot.inst, RelationType.Size);
                GRoot.inst.AddChild(uI_Battle);
                await Database.Instance.Init();
                BattleConfig config = new BattleConfig()
                {
                    Player = new Player()
                    {
                        Dices=Dices.Select(x=>new WorldDice(x)).ToArray(),
                    },
                    TeamConfig = Database.Instance.Get<TeamConfig>(TeamId),
                };
                Battle battle = new Battle(config);
                uI_Battle.SetBattle(battle);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        public void LoadPackge(string PackageName)
        {
            if (LoadedPackages.Contains(PackageName))
                return;
            LoadedPackages.Add(PackageName);
#if UNITY_EDITOR
            UIPackage.AddPackage(PackagePath + PackageName);
#else
            string p = Path.Combine(PathHelper.AppHotfixResPath, PathHelper.UIPath, PackageName.ToLower());
            AssetBundle res = null;
            string resPath = p + "res";
            if (File.Exists(resPath))
                res = AssetBundle.LoadFromFile(resPath);
            var desc = AssetBundle.LoadFromFile(p + "desc");
            if (res != null)
                UIPackage.AddPackage(desc, res);
            else
            {
                UIPackage.AddPackage(desc);
            }
#endif
        }
    }
}
