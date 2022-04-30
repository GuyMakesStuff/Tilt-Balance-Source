using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TiltBalance.Visuals
{
    [RequireComponent(typeof(Camera))]
    public class CameraShake : MonoBehaviour
    {
        public Vector2 BaseRot;
        public float ShakeDrainTime;
        public float ShakeRangeMultiplier;
        float ShakeIntensity;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            float MinMaxRot = ShakeIntensity * ShakeRangeMultiplier;
            float Rot = Random.Range(-MinMaxRot, MinMaxRot);
            transform.rotation = Quaternion.Euler(BaseRot.x, BaseRot.y, Rot);

            if (ShakeIntensity > 0f)
            {
                ShakeIntensity -= Time.deltaTime * ShakeDrainTime;
            }
            else
            {
                Freeze();
            }
        }

        public void Shake(float Amount)
        {
            ShakeIntensity = Amount;
        }
        public void Freeze()
        {
            ShakeIntensity = 0f;
        }
        public void LookDown()
        {
            GetComponent<Animator>().SetTrigger("Look Down");
        }
    }
}