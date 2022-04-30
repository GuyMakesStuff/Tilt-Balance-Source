using System.Collections;
using TiltBalance.Audio;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace TiltBalance.Managers
{
    public class FadeManager : Manager<FadeManager>
    {
        [Space]
        public Animator FadePanel;
        [HideInInspector]
        public bool IsFaded;

        void Awake()
        {
            Init(this);
        }

        void Update()
        {
            FadePanel.SetBool("Is Faded", IsFaded);
        }

        public void FadeTo(string SceneName)
        {
            StartCoroutine(fadeTo(SceneName));
        }
        IEnumerator fadeTo(string scene)
        {
            IsFaded = true;
            AudioManager.Instance.InteractWithSFX("Fade In", SoundEffectBehaviour.Play);

            yield return new WaitForSeconds(0.5f);

            AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
            while(!operation.isDone)
            {
                yield return null;
            }

            PPManager.Instance.SetDeathFX(0f);
            IsFaded = false;
            AudioManager.Instance.InteractWithSFX("Fade Out", SoundEffectBehaviour.Play);
        }
    }
}