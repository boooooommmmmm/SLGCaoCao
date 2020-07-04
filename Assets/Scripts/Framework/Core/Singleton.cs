using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Core
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        protected static T _instance;

        public static T GetInstance()
        {
            return _instance;
        }

        private void Awake()
        {
            _instance = this as T;
        }
    }
}
