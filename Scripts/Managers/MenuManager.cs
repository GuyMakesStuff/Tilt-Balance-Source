using TiltBalance.Interface;
using System.Collections.Generic;
using UnityEngine.UI;
using TiltBalance.Audio;
using TiltBalance.IO;
using UnityEngine;
using TMPro;

namespace TiltBalance.Managers
{
    public class MenuManager : Manager<MenuManager>
    {
        [Header("Achievements Menu")]
        public RectTransform AchievementsContainer;
        public GameObject AchievementDisplayPrefab;
        public GameObject NewAchievementsIcon;

        [Header("Settings Menu")]
        public Slider MusicSlider;
        public Slider SFXSlider;
        public TMP_Dropdown QualityDropdown;
        public TMP_Dropdown ResolutionDropdown;
        Resolution[] Resolutions;
        public Toggle FSToggle;
        public Toggle PPToggle;
        public Toggle MLToggle;
        [System.Serializable]
        public class Settings : SaveFile
        {
            [Space]
            public float MusicVol;
            public float SFXVol;
            public int QualityLevel;
            public int ResIndex;
            public bool FullScreen;
            public bool PP;
            public bool ML;
        }
        public Settings settings;

        // Start is called before the first frame update
        void Start()
        {
            Init(this);
            SkinManager.Instance.OverrideSelectedSkin = false;
            AudioManager.Instance.SetMusicTrack("On The Platfrom");

            for (int A = 0; A < ProgressManager.Instance.Achievements.Length; A++)
            {
                GameObject NewAchievementDisplay = Instantiate(AchievementDisplayPrefab, Vector3.zero, Quaternion.identity, AchievementsContainer);
                Vector2 Pos = new Vector3(0f, -77.5f - (140 * A));
                NewAchievementDisplay.GetComponent<RectTransform>().anchoredPosition = Pos;
                NewAchievementDisplay.GetComponent<AchievementDisplay>().AchievementName = ProgressManager.Instance.Achievements[A].Name;
            }
            AchievementsContainer.sizeDelta = new Vector2(AchievementsContainer.sizeDelta.x, (140 * ProgressManager.Instance.Achievements.Length) + 15f);
            AchievementsContainer.anchoredPosition = new Vector2(0f, AchievementsContainer.sizeDelta.y / 2f);

            Settings LoadedSettings = Saver.Load(settings) as Settings;
            int CurResIndex = InitRes();
            if(LoadedSettings != null) { settings = LoadedSettings; }
            else
            {
                settings.MusicVol = AudioManager.Instance.GetMusicVolume();
                settings.SFXVol = AudioManager.Instance.GetSFXVolume();
                settings.QualityLevel = QualitySettings.GetQualityLevel();
                settings.ResIndex = CurResIndex;
                settings.FullScreen = Screen.fullScreen;
                settings.PP = PPManager.Instance.PPEnabled;
                settings.ML = MouseManager.Instance.LockMouse;
            }

            MusicSlider.value = settings.MusicVol;
            SFXSlider.value = settings.SFXVol;
            QualityDropdown.value = settings.QualityLevel;
            ResolutionDropdown.value = settings.ResIndex;
            FSToggle.isOn = settings.FullScreen;
            PPToggle.isOn = settings.PP;
            MLToggle.isOn = settings.ML;
            ApplySettings();
            settings.Save();
        }
        int InitRes()
        {
            Resolutions = Screen.resolutions;
            ResolutionDropdown.ClearOptions();
            List<string> Res2String = new List<string>();
            Resolution CurRes = Screen.currentResolution;
            int Result = 0;
            for (int R = 0; R < Resolutions.Length; R++)
            {
                Resolution Res = Resolutions[R];
                string String = Res.ToString();
                Res2String.Add(String);

                if(Res.width == CurRes.width && Res.height == CurRes.height)
                {
                    Result = R;
                }
            }
            ResolutionDropdown.AddOptions(Res2String);
            return Result;
        }

        // Update is called once per frame
        void Update()
        {
            NewAchievementsIcon.SetActive(ProgressManager.Instance.NewAchievementsList.Count > 0);

            settings.MusicVol = MusicSlider.value;
            settings.SFXVol = SFXSlider.value;
            settings.QualityLevel = QualityDropdown.value;
            settings.ResIndex = ResolutionDropdown.value;
            settings.FullScreen = FSToggle.isOn;
            settings.PP = PPToggle.isOn;
            settings.ML = MLToggle.isOn;
            ApplySettings();
            settings.Save();
        }
        void ApplySettings()
        {
            AudioManager.Instance.SetMusicVolume(MusicSlider.value);
            AudioManager.Instance.SetSFXVolume(SFXSlider.value);
            QualitySettings.SetQualityLevel(QualityDropdown.value);
            PPManager.Instance.PPEnabled = PPToggle.isOn;
            MouseManager.Instance.LockMouse = MLToggle.isOn;
        }
        public void UpdateScreen()
        {
            Resolution Res = Resolutions[ResolutionDropdown.value];
            Screen.SetResolution(Res.width, Res.height, FSToggle.isOn);
        }

        public void Play()
        {
            FadeManager.Instance.FadeTo("Main");
        }
        public void Shop()
        {
            FadeManager.Instance.FadeTo("Shop");
        }
        public void QuitGame()
        {
            Application.Quit();
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.ExitPlaymode();
            #endif
        }

        public void BackFromAchievements()
        {
            ProgressManager.Instance.NewAchievementsList.Clear();
        }

        public void HowToPlay()
        {
            FadeManager.Instance.FadeTo("HowToPlay");
        }
        public void ResetProgress()
        {
            AudioManager.Instance.InteractWithSFX("Reset", SoundEffectBehaviour.Play);
            ProgressManager.Instance.ResetProgress();
        }
    }
}