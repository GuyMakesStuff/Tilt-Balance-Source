using TiltBalance.Audio;
using TiltBalance.Managers;
using UnityEngine;

namespace TiltBalance.Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class Rocket : MonoBehaviour
    {
        Rigidbody Body;
        [Min(float.Epsilon)]
        public float FallSpeed;
        public string ExplotionFXName;

        // Start is called before the first frame update
        void Start()
        {
            Body = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Body.velocity = Vector3.down * FallSpeed;
        }

        void OnCollisionEnter(Collision other)
        {
            FXManager.Instance.SpawnFX(ExplotionFXName, other.GetContact(0).point);
            AudioManager.Instance.InteractWithSFX("Rocket Explode", SoundEffectBehaviour.Play);
            Destroy(gameObject);
        }
    }
}