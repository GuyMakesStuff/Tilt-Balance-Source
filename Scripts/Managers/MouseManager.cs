using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace TiltBalance.Managers
{
    public class MouseManager : Manager<MouseManager>
    {
        [Space]
        public bool LockMouse;
        [HideInInspector]
        public bool InGame;
        bool IsPaused;

        // Start is called before the first frame update
        void Start()
        {
            Init(this);
        }

        // Update is called once per frame
        void Update()
        {
            InGame = SceneManager.GetActiveScene().name == "Main";
            if(InGame) { IsPaused = GameManager.Instance.ShowMouse; }
            else { IsPaused = false; }
            bool MouseHidden = (LockMouse && InGame) && !IsPaused;

            Cursor.lockState = (MouseHidden) ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !MouseHidden;
        }
    }
}