using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public sealed class GameKernel : MonoBehaviour
    {
        [SerializeField][NotNull]
        private UIKernel uiKernel;

        private static GameKernel _instance;

        public static GameKernel GetInstance()
        {
            return _instance;
        }


        private void Awake()
        {
            // init game kernel
            if (_instance != null)
            {
                throw new SystemException("Duplicate mono kernel");
            }
            _instance = this;
            DontDestroyOnLoad(this);

            //check sub kernels
            if (uiKernel == null)
            {
                throw new SystemException("UI kernel not serialized!");
            }

        }
    }
}
