namespace Lake.Utilities
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class SingletonBehaviour<T> : MonoBehaviour
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
                    instance = value;
                }
            }
        }
    }
}