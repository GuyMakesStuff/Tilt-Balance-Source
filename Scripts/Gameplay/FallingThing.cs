using TiltBalance.Audio;
using TiltBalance.Managers;
using UnityEngine;

namespace TiltBalance.Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class FallingThing : MonoBehaviour
    {
        Rigidbody Body;
        [Min(float.Epsilon)]
        public float FallSpeed;
        public float RotSpeed;
        public float LastTime;
        bool IsFalling;

        // Start is called before the first frame update
        void Start()
        {
            Body = GetComponent<Rigidbody>();
            IsFalling = true;
        }

        // Update is called once per frame
        void Update()
        {
            if(IsFalling) { transform.Rotate(Vector3.forward * RotSpeed * 360f * Time.deltaTime); }
            else { transform.localScale = new Vector3(0.1f, 0.1f, 10f);
                transform.localRotation = Quaternion.Euler(90f, 0f, -180f); }
        }
        void FixedUpdate()
        {
            if(IsFalling) Body.velocity = Vector3.down * FallSpeed;
        }

        void OnCollisionEnter(Collision other)
        {
            if(other.collider.tag == "Platform")
            {
                IsFalling = false;
                Body.constraints = RigidbodyConstraints.FreezeAll;
                transform.SetParent(GameObject.Find("StuckObjectsContainer").transform);
                transform.localPosition = new Vector3(transform.localPosition.x, 0f, transform.localPosition.z);
                FXManager.Instance.SpawnFX("Explotion", transform.position);
                AudioManager.Instance.InteractWithSFX("Rocket Explode", SoundEffectBehaviour.Play);
                Invoke("Explode", LastTime);
            }
            else
            {
                Explode();
            }
        }

        void Explode()
        {
            FXManager.Instance.SpawnFX("Falling Thing Destroy", transform.position);
            AudioManager.Instance.InteractWithSFX("Disappear", SoundEffectBehaviour.Play);
            Destroy(gameObject);
        }
    }
}