using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace TiltBalance.Managers
{
    public class SkinManager : Manager<SkinManager>
    {
        [System.Serializable]
        public class Skin
        {
            public Color SkinColor;
            public int Cost;
        }
        public Skin[] Skins = new Skin[10];
        [HideInInspector]
        public int SelectedSkinIndex;
        [HideInInspector]
        public int DisplaySkinIndex;
        [HideInInspector]
        public bool OverrideSelectedSkin;

        // Start is called before the first frame update
        void Awake()
        {
            Init(this);
        }

        public void AltStart()
        {
            SelectedSkinIndex = ProgressManager.Instance.progress.SelectedSkinIndex;
        }

        // Update is called once per frame
        void Update()
        {
            if(!OverrideSelectedSkin)
            {
                DisplaySkinIndex = SelectedSkinIndex;
            }
            ProgressManager.Instance.progress.SelectedSkinIndex = SelectedSkinIndex;
        }
    }
}