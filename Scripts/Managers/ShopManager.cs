using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TiltBalance.Interface;
using TiltBalance.Audio;
using TMPro;

namespace TiltBalance.Managers
{
    public class ShopManager : Manager<ShopManager>
    {
        [Space]
        public TMP_Text MoneyText;
        public TMP_Text PriceText;
        public PrevNextMenu SkinSelector;
        public GameObject BuyButton;
        public GameObject SelectButton;

        // Start is called before the first frame update
        void Start()
        {
            Init(this);
            AudioManager.Instance.SetMusicTrack("Skylevator");

            SkinManager.Instance.OverrideSelectedSkin = false;
            SkinSelector.Value = ProgressManager.Instance.progress.SelectedSkinIndex;
            SkinSelector.MinValue = 0;
            SkinSelector.MaxValue = SkinManager.Instance.Skins.Length - 1;
        }

        // Update is called once per frame
        void Update()
        {
            SkinManager.Instance.DisplaySkinIndex = SkinSelector.Value;
            MoneyText.text = "Money:" + ProgressManager.Instance.progress.Money.ToString("000");
            PriceText.text = "Price:" + SkinManager.Instance.Skins[SkinSelector.Value].Cost.ToString("000");
            BuyButton.SetActive(!ProgressManager.Instance.progress.SkinsUnlocked[SkinSelector.Value]);
            SelectButton.SetActive(ProgressManager.Instance.progress.SkinsUnlocked[SkinSelector.Value] && SkinSelector.Value != SkinManager.Instance.SelectedSkinIndex);
        }

        public void Buy()
        {
            if(ProgressManager.Instance.progress.Money >= SkinManager.Instance.Skins[SkinSelector.Value].Cost)
            {
                ProgressManager.Instance.progress.Money -= SkinManager.Instance.Skins[SkinSelector.Value].Cost;
                ProgressManager.Instance.progress.SkinsUnlocked[SkinSelector.Value] = true;
                AudioManager.Instance.InteractWithSFX("Buy", SoundEffectBehaviour.Play);
            }
            else
            {
                AudioManager.Instance.InteractWithSFX("No Money", SoundEffectBehaviour.Play);
            }
        }
        public void Select()
        {
            PlaySelectSound();
            SkinManager.Instance.SelectedSkinIndex = SkinSelector.Value;
        }

        public void Back()
        {
            PlaySelectSound();
            FadeManager.Instance.FadeTo("Menu");
        }
    }
}