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

        public List<DiceFaceConfig> GetFaces()
        {
            return FaceIds.Select(x => Database.Instance.Get<DiceFaceConfig>(x)).ToList();
        }

        public void ReRoll()
        {
            NowFace = Battle.RandomRange(0, 6);
        }
    }
}
