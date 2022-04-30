using System.Collections;
using TiltBalance.Managers;
using UnityEngine;
using TiltBalance.Audio;
using TMPro;

namespace TiltBalance.Interface
{
    public class HowToPlayMenu : MonoBehaviour
    {
        [System.Serializable]
        public class Item
        {
            public string Name;
            [TextArea(3, 10)]
            public string Description;
            public GameObject Object;
        }
        public Item[] Items;
        public PrevNextMenu ItemViewer;
        public TMP_Text ItemNameText;
        public TMP_Text ItemDescriptionText;

        // Start is called before the first frame update
        void Start()
        {
            AudioManager.Instance.SetMusicTrack("Skylevator");

            ItemViewer.MinValue = 0;
            ItemViewer.MaxValue = Items.Length - 1;
        }

        // Update is called once per frame
        void Update()
        {
            for (int I = 0; I < Items.Length; I++)
            {
                Items[I].Object.SetActive(I == ItemViewer.Value);
            }
            ItemNameText.text = Items[ItemViewer.Value].Name;
            ItemDescriptionText.text = Items[ItemViewer.Value].Description;
        }

        public void Back()
        {
            ProgressManager.Instance.PlaySelectSound();
            FadeManager.Instance.FadeTo("Menu");
        }
    }
}