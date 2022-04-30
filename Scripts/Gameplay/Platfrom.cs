using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TiltBalance.Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class Platfrom : MonoBehaviour
    {
        Rigidbody Body;
        public float RotSpeed;
        Quaternion Rot;
        float X;
        float Y;
        float Z;

        // Start is called before the first frame update
        void Start()
        {
            Body = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            X = Input.GetAxisRaw("Horizontal") * RotSpeed;
            Y = Input.GetAxisRaw("Vertical") * RotSpeed;
            Z = Input.GetAxisRaw("AltHorizontal") * RotSpeed;
        }

        void FixedUpdate()
        {
            Vector3 UpRotVel = transform.up * Z;
            Vector3 RotVel = new Vector3(Y, 0f, -X) + UpRotVel;
            Body.angularVelocity = RotVel;
        }
    }
}