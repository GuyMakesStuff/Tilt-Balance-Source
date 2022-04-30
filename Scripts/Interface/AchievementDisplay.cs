using UnityEngine.UI;
using UnityEngine;
using TiltBalance.Managers;
using TiltBalance.Gameplay;
using TMPro;

namespace TiltBalance.Interface
{
    public class AchievementDisplay : MonoBehaviour
    {
        public string AchievementName;
        public TMP_Text NameText;
        public TMP_Text DescriptionText;
        public Image IconImage;
        public Sprite DefaultIcon;
        public GameObject NewIcon;
        Achievement achievement;
        bool AchievemntRecieved;

        // Start is called before the first frame update
        void Start()
        {
            achievement = System.Array.Find(ProgressManager.Instance.Achievements, Achievement => Achievement.Name == AchievementName);
        }

        // Update is called once per frame
        void Update()
        {
            AchievemntRecieved = ProgressManager.Instance.AchievementsList.Contains(AchievementName);

            NameText.text = (AchievemntRecieved) ? achievement.Name : "???";
            DescriptionText.text = achievement.Description;
            IconImage.sprite = (AchievemntRecieved) ? achievement.Icon : DefaultIcon;
            NewIcon.SetActive(ProgressManager.Instance.NewAchievementsList.Contains(achievement.Name));
        }
    }
}