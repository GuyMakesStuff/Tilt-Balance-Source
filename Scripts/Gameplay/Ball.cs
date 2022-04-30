using TiltBalance.Visuals;
using TiltBalance.Managers;
using UnityEngine;

namespace TiltBalance.Gameplay
{
    public class Ball : MonoBehaviour
    {
        public float MinHeight;
        public bool RandPosOnStart;
        Material Mat;

        void Start()
        {
            if(RandPosOnStart) { transform.position = (Vector3.up * 1.5f) + Random.insideUnitSphere; }
            Mat = GetComponent<MeshRenderer>().sharedMaterial;
        }

        void Update()
        {
            Mat.color = (SkinManager.IsInstanced) ? SkinManager.Instance.Skins[SkinManager.Instance.DisplaySkinIndex].SkinColor : new Color(0f, (152f / 255f), 1f);

            if(transform.position.y <= MinHeight && !GameManager.Instance.IsDead)
            {
                FindObjectOfType<CameraShake>().LookDown();
                GameManager.Instance.Die();
            }
        }

        void OnCollisionEnter(Collision other)
        {
            if(other.collider.tag == "Obstacles")
            {
                GameManager.Instance.Die();
                FXManager.Instance.SpawnFX("Player Die", transform.position);
                FindObjectOfType<CameraShake>().Shake(3.5f);
                Destroy(gameObject);
            }
        }
    }
}