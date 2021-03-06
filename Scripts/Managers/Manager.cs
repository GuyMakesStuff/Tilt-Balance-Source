using System.Collections;
using TiltBalance.Audio;
using UnityEngine;

namespace TiltBalance.Managers
{
    public class Manager<T> : MonoBehaviour
    {
        public static T Instance { get; private set; }
        public static bool IsInstanced
        {
            get
            {
                return Instance != null;
            }
        }
        public bool IsGlobal;

        protected void Init(T OBJ)
        {
            Instance = OBJ;

            if(IsGlobal)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        public void PlaySelectSound()
        {
            AudioManager.Instance.InteractWithSFX("Select", SoundEffectBehaviour.Play);
        }
    }
}