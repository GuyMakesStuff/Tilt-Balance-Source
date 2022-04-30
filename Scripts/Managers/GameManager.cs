using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TiltBalance.Audio;
using TMPro;

namespace TiltBalance.Managers
{
    public class GameManager : Manager<GameManager>
    {
        public float MinWorldHeight;

        [Header("Score")]
        public TMP_Text ScoreText;
        [HideInInspector]
        public int Score;
        public TMP_Text HIScoreText;
        public GameObject NewHIText;
        int HIScore;
        bool HIScoreBeat;
        float GameTime;

        [Header("Mini Menus")]
        public GameObject PauseMenu;
        bool IsPaused;
        public GameObject GameOverMenu;
        public GameObject GameOverMenuBG;
        public TMP_Text FinalMoneyBonusText;
        [HideInInspector]
        public bool IsDead;
        float PrevGameTime;
        [HideInInspector]
        public bool ShowMouse;

        // Start is called before the first frame update
        void Start()
        {
            Init(this);
            PPManager.Instance.SetDeathFX(0f);
            AudioManager.Instance.SetMusicTrack("The Balancer");
            if(ProgressManager.IsInstanced) { HIScore = ProgressManager.Instance.progress.HIScore; }
        }

        // Update is called once per frame
        void Update()
        {
            for (int G = 0; G < SpawnManager.Instance.ObstacleContainer.childCount; G++)
            {
                Transform Obstacle = SpawnManager.Instance.ObstacleContainer.GetChild(G);
                if(Obstacle.position.y < MinWorldHeight)
                {
                    Destroy(Obstacle.gameObject);
                }
            }

            GameTime += Time.deltaTime;
            if(!IsDead) { Score = Mathf.RoundToInt(GameTime); }
            if(Score > HIScore)
            {
                HIScore = Score;
                if(!HIScoreBeat)
                {
                    HIScoreBeat = true;
                    AudioManager.Instance.InteractWithSFX("New High Score", SoundEffectBehaviour.Play);
                    NewHIText.SetActive(true);
                }
            }
            if(ProgressManager.IsInstanced) { ProgressManager.Instance.progress.HIScore = HIScore; }
            ScoreText.text = "Score:" + Score.ToString("000");
            HIScoreText.text = "High Score:" + HIScore.ToString("000");

            if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("p"))
            {
                if(!IsPaused && !IsDead)
                {
                    PlaySelectSound();
                    IsPaused = true;
                }
            }
            PauseMenu.SetActive(IsPaused);
            Time.timeScale = (IsPaused) ? 0f : 1f;
            SoundEffectBehaviour SFXBehaviour = (IsPaused) ? SoundEffectBehaviour.Pause : SoundEffectBehaviour.Resume;
            AudioManager.Instance.InteractWithAllSFXOneShot(SFXBehaviour);
            AudioManager.Instance.InteractWithMusic(SFXBehaviour);

            if(IsDead)
            {
                PPManager.Instance.SetDeathFX((GameTime - PrevGameTime) * 2f);
            }
            GameOverMenu.SetActive(IsDead);
            GameOverMenuBG.SetActive(!PPManager.Instance.PPEnabled);

            ShowMouse = IsPaused | IsDead;
        }

        public void Resume()
        {
            PlaySelectSound();
            IsPaused = false;
        }
        public void Retry()
        {
            Resume();
            FadeManager.Instance.FadeTo(SceneManager.GetActiveScene().name);
        }
        public void Menu()
        {
            Resume();
            FadeManager.Instance.FadeTo("Menu");
        }

        public void Die()
        {
            PrevGameTime = GameTime;
            int FinalMoneyBonus = Score / 2;
            if(HIScoreBeat) { FinalMoneyBonus += 10; }
            ProgressManager.Instance.progress.Money += FinalMoneyBonus;
            FinalMoneyBonusText.text = "Money Earned:" + FinalMoneyBonus.ToString("000");
            ProgressManager.Instance.progress.Deaths++;
            IsDead = true;
            AudioManager.Instance.InteractWithSFX("Player Die", SoundEffectBehaviour.Play);
            AudioManager.Instance.MuteMusic();
        }
    }
}