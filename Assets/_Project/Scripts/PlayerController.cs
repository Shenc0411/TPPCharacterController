namespace Lake
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerController
    {
        [SerializeField]
        private Character character;

        private List<KeyValuePair<KeyCode, Action>> keyHoldCallbacks = new List<KeyValuePair<KeyCode, Action>>();
        private List<KeyValuePair<KeyCode, Action>> keyDownCallbacks = new List<KeyValuePair<KeyCode, Action>>();
        private List<KeyValuePair<KeyCode, Action>> keyUpCallbacks = new List<KeyValuePair<KeyCode, Action>>();

        public Character Character
        {
            get => character;
            set
            {
                character = value;
                this.SetupInputCallbacks();
            }
        }

        public PlayerController(Character character)
        {
            this.Character = character;
        }

        private void CleanUpInputCallbacks()
        {
            foreach (KeyValuePair<KeyCode, Action> pair in keyHoldCallbacks)
            {
                InputManager.Instance.UnRegisterKeyHoldCallBack(pair.Key, pair.Value);
            }

            foreach (KeyValuePair<KeyCode, Action> pair in keyDownCallbacks)
            {
                InputManager.Instance.UnRegisterKeyDownCallBack(pair.Key, pair.Value);
            }

            foreach (KeyValuePair<KeyCode, Action> pair in keyUpCallbacks)
            {
                InputManager.Instance.UnRegisterKeyUpCallBack(pair.Key, pair.Value);
            }

            keyHoldCallbacks.Clear();
            keyDownCallbacks.Clear();
            keyUpCallbacks.Clear();
        }

        private void SetupInputCallbacks()
        {
            CleanUpInputCallbacks();

            if(character == null)
            {
                Debug.Log("Cannot register control callbacks for null character");
                return;
            }

            keyHoldCallbacks.Add(new KeyValuePair<KeyCode, Action>(KeyCode.W, () => character.MoveForward()));
            keyHoldCallbacks.Add(new KeyValuePair<KeyCode, Action>(KeyCode.S, () => character.MoveBackward()));
            keyHoldCallbacks.Add(new KeyValuePair<KeyCode, Action>(KeyCode.A, () => character.RotateLeft()));
            keyHoldCallbacks.Add(new KeyValuePair<KeyCode, Action>(KeyCode.D, () => character.RotateRight()));
            keyHoldCallbacks.Add(new KeyValuePair<KeyCode, Action>(KeyCode.LeftShift, () => character.Sprint()));

            #region Register Key Callbacks
            foreach (KeyValuePair<KeyCode, Action> pair in keyHoldCallbacks)
            {
                InputManager.Instance.RegisterKeyHoldCallBack(pair.Key, pair.Value);
            }

            foreach (KeyValuePair<KeyCode, Action> pair in keyDownCallbacks)
            {
                InputManager.Instance.RegisterKeyDownCallBack(pair.Key, pair.Value);
            }

            foreach (KeyValuePair<KeyCode, Action> pair in keyUpCallbacks)
            {
                InputManager.Instance.RegisterKeyUpCallBack(pair.Key, pair.Value);
            } 
            #endregion
        }

    }

}