using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace MuqDice
{
    public class Database
    {
        public static Database Instance => instance == null ? instance = new Database() : instance;
        private static Database instance;

        Dictionary<Type, IConfig[]> dic = new Dictionary<Type, IConfig[]>();

        public async Task Init()
        {
            try
            {
                await ResourcesManager.Instance.LoadBundleAsync(PathHelper.DataPath);
                Add<MagicConfig>("magic");
                //Add<UnitConfig>("UnitConfig");
                //Add<SkillConfig>("SkillConfig");
                //Add<SkillEffectConfig>("SkillEffectConfig");
                //Add<BuffConfig>("BuffConfig");
                //Add<BuffEffectConfig>("BuffEffectConfig");
                //Add<TeamConfig>("TeamConfig");
                //Add<EquipConfig>("EquipConfig");
                //Add<EquipSkillConfig>("EquipSkillConfig");
                ResourcesManager.Instance.UnloadBundle(PathHelper.DataPath);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        public T Get<T>(int id) where T : class, IConfig
        {
            dic.TryGetValue(typeof(T), out IConfig[] r);
            if (r == null) throw new Exception();
            return r[id] as T;
        }

        public T Get<T>(string id) where T : class, IConfig
        {
            dic.TryGetValue(typeof(T), out IConfig[] r);
            return r.FirstOrDefault(x => x._Id == id) as T;
        }

        public T[] GetAll<T>() where T : class, IConfig
        {
            return dic[typeof(T)].Select(x => x as T).ToArray();
        }

        public T Get<T>(Func<T, bool> match) where T : class, IConfig
        {
            dic.TryGetValue(typeof(T), out IConfig[] r);
            return r.FirstOrDefault(x => match(x as T)) as T;
        }

        public int GetIndex<T>(T t) where T : class, IConfig
        {
            dic.TryGetValue(typeof(T), out IConfig[] r);
            return Array.IndexOf(r, t);
        }

        public int GetIndex<T>(string id) where T : class, IConfig
        {
            dic.TryGetValue(typeof(T), out IConfig[] r);
            return Array.FindIndex(r, x => x._Id == id);
        }

        private void Add<T>(string name) where T : IConfig
        {
            var text = ResourcesManager.Instance.GetAsset<TextAsset>(PathHelper.DataPath, name).text;
            T[] values = MongoHelper.FromJson<T[]>(text);
            //foreach (var v in values)
            //{
            //    Debug.Log(MongoHelper.ToJson(v));
            //}
            dic.Add(typeof(T), values.Select(x => x as IConfig).ToArray());
        }
    }
}