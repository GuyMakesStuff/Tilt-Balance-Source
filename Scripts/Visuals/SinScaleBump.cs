using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TiltBalance.Visuals
{
    public class SinScaleBump : MonoBehaviour
    {
        public float SinRange;
        public float SinSpeed;
        Vector3 BaseScale;

        void Start()
        {
            BaseScale = transform.localScale;
        }

        // Update is called once per frame
        void Update()
        {
            float X = Mathf.Sin((Time.time * SinSpeed) * 90f) * SinRange;
            transform.localScale = BaseScale + (Vector3.one * X);
        }
    }
}