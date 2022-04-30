using System.Collections;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

namespace TiltBalance.Managers
{
    [RequireComponent(typeof(PostProcessVolume))]
    public class PPManager : Manager<PPManager>
    {
        [Space]
        public bool PPEnabled;
        public PostProcessProfile Profile;
        PostProcessVolume Vol;

        [Header("Death Effect")]
        public float DeathVignetteIntensityMuliplier;
        float StartVignetteIntensity;
        public Color DeathColor;
        Vignette VignetteFX;
        float DeathFX;
        

        // Start is called before the first frame update
        void Awake()
        {
            Init(this);
            Vol = GetComponent<PostProcessVolume>();
            Vol.profile = Profile;

            VignetteFX = Profile.GetSetting<Vignette>();
            StartVignetteIntensity = 0.175f;
        }

        // Update is called once per frame
        void Update()
        {
            Vol.enabled = PPEnabled;

            DeathFX = Mathf.Clamp01(DeathFX);
            VignetteFX.intensity.value = Mathf.Lerp(StartVignetteIntensity, StartVignetteIntensity * DeathVignetteIntensityMuliplier, DeathFX);
            VignetteFX.color.value = Color.Lerp(Color.black, DeathColor, DeathFX);
        }

        public void SetDeathFX(float Value)
        {
            DeathFX = Mathf.Clamp01(Value);
        }
    }
}