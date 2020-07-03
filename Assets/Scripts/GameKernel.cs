using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.Core;
using Framework.Data;
using Framework.StateMachine;
using Framework.Record;

namespace Framework
{
    public sealed class GameKernel : MonoBehaviour
    {
        [NotNull]
        public UIKernel UIKernel;

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
            if (UIKernel == null)
            {
                throw new SystemException("UI kernel not serialized!");
            }
        }

        private void Start()
        {
            //load static data
            var moduleInstance = ModuleManager.Get();
            moduleInstance.CreateModule<StaticDataModule>("StaticData");
            moduleInstance.CreateModule<RecordModule>("Record");

            //init state machine
            var stateMachine = StateMachineManager.Get();
            //stateMachine.CreateState<RootStateMachine>("Root");
            stateMachine.CreateState<StartStateMachine>("Start");
            //Login
            //Lobby
            stateMachine.CreateState<BattleStateMachine>("Battle");

            stateMachine.SwitchState("Start");
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
