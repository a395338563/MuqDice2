using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuqDice
{
    public class Player
    {
        public WorldDice[] Dices;
    }

    public class WorldDice
    {
        public int[] FaceIds = new int[6];
        public WorldDice(int configId)
        {
            Random random = new Random();
            DiceConfig diceConfig = Database.Instance.Get<DiceConfig>(configId);
            if (diceConfig.QualityAll != 0)
            {
                RandomDiceConfig randomConfig = null;
                var all = Database.Instance.GetAll<RandomDiceConfig>().Where(x => x.Sum == diceConfig.QualityAll).ToList();
                var sum = all.Sum(x => x.Weight);
                int r = random.Next(0, sum);
                for (int i = 0; i < all.Count; i++)
                {
                    if (r < all[i].Weight)
                    {
                        randomConfig = all[i];
                        break;
                    }
                    r -= all[i].Weight;
                }
                for (int i = 0; i < 6; i++)
                {
                    FaceIds[i] = Database.Instance.GetIndex<DiceFaceConfig>(getByQuality(random,randomConfig.FaceQualitys[i]));
                }
            }
            else
            {
                if (diceConfig.Faces != null)
                    for (int i = 0; i < diceConfig.Faces.Length; i++)
                    {
                        FaceIds[i] = diceConfig.Faces[i];
                    }
                if (diceConfig.Qualitys != null)
                    for (int i = diceConfig.Qualitys.Length; i < 6; i++)
                    {
                        FaceIds[i] = Database.Instance.GetIndex<DiceFaceConfig>(getByQuality(random, diceConfig.Qualitys[i - (diceConfig.Faces == null ? 0 : diceConfig.Faces.Length)]));
                    }
            }
        }

        private DiceFaceConfig getByQuality(Random random, int quality)
        {
            var all = Database.Instance.GetAll<DiceFaceConfig>().Where(x => x.Quality == quality).ToList();
            return all[random.Next(0, all.Count)];
        }
    }
}
