using TiltBalance.Managers;
using System.Collections.Generic;
using UnityEngine;

namespace TiltBalance.Gameplay
{
    [CreateAssetMenu(fileName = "Achievement", menuName = "Achievement", order = 0)]
    public class Achievement : ScriptableObject
    {
        public string Name;
        [TextArea(0, 3)]
        public string Description;
        public Sprite Icon;
        public enum AchievementType { Score, Deaths, SkinsBought}
        public AchievementType Type;
        public int Value;

        public bool CheckIfBeat(ProgressManager progressManager)
        {
            switch (Type)
            {
                case AchievementType.Score:
                {
                    return progressManager.progress.HIScore >= Value;
                }
                case AchievementType.Deaths:
                {
                    return progressManager.progress.Deaths >= Value;
                }
                case AchievementType.SkinsBought:
                {
                    List<bool> SkinsBought = new List<bool>();
                    foreach (bool B in progressManager.progress.SkinsUnlocked)
                    {
                        if(B == true) { SkinsBought.Add(B); }
                    }
                    return (SkinsBought.Count - 1) >= Value;
                }
            }

            return false;
        }
    }
}