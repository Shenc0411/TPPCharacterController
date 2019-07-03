namespace Lake.Utilities
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class SingletonBehaviour<T> : MonoBehaviour 
        where T : MonoBehaviour
    {
        private static T instance;
        private static object mutex = new object();

        public static T Instance
        {
            get { return instance; }
            set
            {
                lock(mutex)
                {
                    if(instance != null)
                    {
                        Debug.Log("Switching instance of " + typeof(T).ToString() + " from " + instance.name + " to " + (value == null ? "null" : value.name));
                        Destroy(instance);
                    }

                    instance = value;
                }
            }
        }

        protected virtual void Awake()
        {
            SingletonBehaviour<T>.Instance = this as T;
            DontDestroyOnLoad(this);
        }

        protected virtual void OnDestroy()
        {
            SingletonBehaviour<T>.Instance = null;
        }
    }
}