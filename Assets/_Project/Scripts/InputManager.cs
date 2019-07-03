namespace Lake
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Lake.Utilities;

    public class InputManager : SingletonBehaviour<InputManager>
    {
        private Dictionary<KeyCode, Action> keyHoldCallBackMap = new Dictionary<KeyCode, Action>();
        private Dictionary<KeyCode, Action> keyDownCallBackMap = new Dictionary<KeyCode, Action>();
        private Dictionary<KeyCode, Action> keyUpCallBackMap = new Dictionary<KeyCode, Action>();

        public void RegisterKeyHoldCallBack(KeyCode key, Action callback)
        {
            if (keyHoldCallBackMap.ContainsKey(key))
            {
                keyHoldCallBackMap[key] += callback;
            }
            else
            {
                keyHoldCallBackMap.Add(key, callback);
            }
        }

        public void UnRegisterKeyHoldCallBack(KeyCode key, Action callback)
        {
            if (keyHoldCallBackMap.ContainsKey(key))
            {
                keyHoldCallBackMap[key] -= callback;
            }
        }

        public void RegisterKeyDownCallBack(KeyCode key, Action callback)
        {
            if (keyDownCallBackMap.ContainsKey(key))
            {
                keyDownCallBackMap[key] += callback;
            }
            else
            {
                keyDownCallBackMap.Add(key, callback);
            }
        }

        public void UnRegisterKeyDownCallBack(KeyCode key, Action callback)
        {
            if (keyDownCallBackMap.ContainsKey(key))
            {
                keyDownCallBackMap[key] -= callback;
            }
        }

        public void RegisterKeyUpCallBack(KeyCode key, Action callback)
        {
            if (keyUpCallBackMap.ContainsKey(key))
            {
                keyUpCallBackMap[key] += callback;
            }
            else
            {
                keyUpCallBackMap.Add(key, callback);
            }
        }

        public void UnRegisterKeyUpCallBack(KeyCode key, Action callback)
        {
            if (keyUpCallBackMap.ContainsKey(key))
            {
                keyUpCallBackMap[key] -= callback;
            }
        }

        protected override void Awake()
        {
            base.Awake();
        }

        private void Update()
        {
            foreach(KeyCode code in keyHoldCallBackMap.Keys)
            {
                if (Input.GetKey(code))
                {
                    keyHoldCallBackMap[code]?.Invoke();
                }
            }

            foreach (KeyCode code in keyDownCallBackMap.Keys)
            {
                if (Input.GetKeyDown(code))
                {
                    keyDownCallBackMap[code]?.Invoke();
                }
            }

            foreach (KeyCode code in keyUpCallBackMap.Keys)
            {
                if (Input.GetKeyUp(code))
                {
                    keyUpCallBackMap[code]?.Invoke();
                }
            }
        }
    }

}