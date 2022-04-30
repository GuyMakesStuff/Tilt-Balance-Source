using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TiltBalance.IO;
using TiltBalance.Audio;
using TiltBalance.Gameplay;
using UnityEngine;
using TMPro;

namespace TiltBalance.Managers
{
    public class ProgressManager : Manager<ProgressManager>
    {
        [System.Serializable]
        public class Progress : SaveFile
        {
            [Space]
            public int HIScore;
            [HideInInspector]
            public string[] Achievments;
            [HideInInspector]
            public string[] NewAchievments;
            public int Money;
            public bool[] SkinsUnlocked;
            public int SelectedSkinIndex;
            public int Deaths;
        }
        [Space]
        public Progress progress;
        [HideInInspector]
        public List<string> AchievementsList;
        [HideInInspector]
        public List<string> NewAchievementsList;
        public Achievement[] Achievements;

        [Header("Achievements UI")]
        public Animator AchievementUI;
        public TMP_Text AchievementNameText;
        public Image AchievementIcon;

        // Start is called before the first frame update
        void Start()
        {
            Init(this);

            Progress LoadedProgress = Saver.Load(progress) as Progress;
            if(LoadedProgress != null)
            {
                progress = LoadedProgress; 
                AchievementsList = new List<string>(progress.Achievments);
                NewAchievementsList = new List<string>(progress.NewAchievments);
            }
            else { ResetProgress(); }

            SkinManager.Instance.AltStart();

            FadeManager.Instance.FadeTo("Menu");
        }

        // Update is called once per frame
        void Update()
        {
            foreach (Achievement A in Achievements)
            {
                if(A.CheckIfBeat(this) && !AchievementsList.Contains(A.Name))
                {
                    AchievementsList.Add(A.Name);
                    AudioManager.Instance.InteractWithSFX("New Achievement", SoundEffectBehaviour.Play);
                    NewAchievementsList.Add(A.Name);
                    StartCoroutine(DisplayAchievementUI(A));
                }
            }

            progress.Achievments = AchievementsList.ToArray();
            progress.NewAchievments = NewAchievementsList.ToArray();
            progress.Save();
        }

        IEnumerator DisplayAchievementUI(Achievement A)
        {
            AchievementUI.SetBool("On Screen", true);
            AchievementNameText.text = A.Name;
            AchievementIcon.sprite = A.Icon;
            yield return new WaitForSeconds(5.5f);
            AchievementUI.SetBool("On Screen", false);
        }

        public void ResetProgress()
        {
            progress.HIScore = 0;
            AchievementsList = new List<string>();
            progress.Achievments = AchievementsList.ToArray();
            NewAchievementsList = new List<string>();
            progress.NewAchievments = NewAchievementsList.ToArray();
            progress.SkinsUnlocked = new bool[8];
            progress.SkinsUnlocked[0] = true;
            progress.SelectedSkinIndex = 0;
            SkinManager.Instance.SelectedSkinIndex = progress.SelectedSkinIndex;
            progress.Money = 0;
            progress.Deaths = 0;
            progress.Save();
        }
    }
}