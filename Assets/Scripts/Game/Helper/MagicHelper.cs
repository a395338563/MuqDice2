using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuqDice
{
    public class MagicHelper
    {
        public static MagicConfig GetMagic(List<ElementEnum> elements)
        {
            //var es = elements.Distinct();
            List<MagicConfig> list = new List<MagicConfig>();
            foreach (var config in Database.Instance.GetAll<MagicConfig>())
            {
                bool canUse = true;
                foreach (var kv in config.Elements.GroupBy(x => x).Select(x=>(x.Key,x.Count())))
                {
                    if (elements.Count(x => x == kv.Key) < kv.Item2)
                    {
                        canUse = false;
                        break;
                    }
                }
                if (canUse) list.Add(config);
            }
            if (list.Count == 0) return null;
            var maxCount = list.Max(x => x.Elements.Length);
            list = list.Where(x => x.Elements.Length == maxCount).ToList();
            var c = list.Select(x =>
            {
                for (int i = 0; i < x.Elements.Length; i++)
                {
                    if (x.Elements[i] != elements[i]) return i;
                }
                return elements.Count;
            }).ToList();
            int index = c.IndexOf(c.Max());
            return list[index];
        }
    }
}
