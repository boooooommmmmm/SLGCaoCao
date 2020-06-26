using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.Core;
using Framework.Data;

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

        public event Action<float> OnUpdate = delegate { };
        public event Action<float> OnLateUpdate = delegate { };
        public event Action OnOnDestroy = delegate { };


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

        private void Start()
        {
            //load static data
            var moduleInstance = ModuleManager.Get();
            moduleInstance.CreateModule<StaticDataModule>("StaticData");
        }

        private void OnDestroy()
        {
            OnOnDestroy();
        }

        private void Update()
        {
            OnUpdate(Time.deltaTime);
        }

        private void LateUpdate()
        {
            OnLateUpdate(Time.deltaTime);
        }

        #region public APIs

        #endregion
    }
}
