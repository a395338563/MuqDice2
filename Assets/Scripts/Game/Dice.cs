using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuqDice
{
    public class Dice
    {
        public Battle Battle;
        public int[] FaceIds = new int[6];
        public int NowFace;

        public DiceFaceConfig NowFaceConfig => Database.Instance.Get<DiceFaceConfig>(FaceIds[NowFace]);

        public Dice()
        {

        }

        public Dice(Battle battle, WorldDice worldDice)
        {
            this.Battle = battle;
            for (int i = 0; i < 6; i++)
            {
                FaceIds[i] = worldDice.FaceIds[i];
            }
            ReRoll();
        }

        public Dice(Battle battle, int configId)
        {
            this.Battle = battle;
            DiceConfig diceConfig = Database.Instance.Get<DiceConfig>(configId);
            if (diceConfig.QualityAll != 0)
            {
                RandomDiceConfig randomConfig = null;
                var all = Database.Instance.GetAll<RandomDiceConfig>().Where(x => x.Sum == diceConfig.QualityAll).ToList();
                var sum = all.Sum(x => x.Weight);
                int r = battle.Random.Next(0, sum);
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
                    FaceIds[i] = Database.Instance.GetIndex<DiceFaceConfig>(getByQuality(randomConfig.FaceQualitys[i]));
                }
            }
            else
            {
                for (int i = 0; i < diceConfig.Faces.Length; i++)
                {
                    FaceIds[i] = diceConfig.Faces[i];
                }
                for (int i= diceConfig.Faces.Length; i < 6; i++)
                {
                    FaceIds[i] = Database.Instance.GetIndex<DiceFaceConfig>(getByQuality(diceConfig.Faces[i - diceConfig.Faces.Length]));
                }
            }
        }

        private DiceFaceConfig getByQuality(int quality)
        {
            var all = Database.Instance.GetAll<DiceFaceConfig>().Where(x => x.Quality == quality).ToList();
            return all[Battle.Random.Next(0, all.Count)];
        }

        public List<DiceFaceConfig> GetFaces()
        {
            return FaceIds.Select(x => Database.Instance.Get<DiceFaceConfig>(x)).ToList();
        }

        public DiceFaceConfig GetFrontFace()
        {
            return Database.Instance.Get<DiceFaceConfig>(FaceIds[NowFace]);
        }

        public void ReRoll()
        {
            NowFace = Battle.RandomRange(0, 6);
        }
    }
}
