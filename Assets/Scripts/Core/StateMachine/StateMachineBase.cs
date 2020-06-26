using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;

namespace Framework.Core
{
    public interface IState
    {
        void Enter();
        void Exit();
    }

    public abstract class StateMachineBase : IState
    {
        protected void _enter()
        {
            GameKernel.GetInstance().OnUpdate += OnUpdate;
            GameKernel.GetInstance().OnLateUpdate += OnLateUpdate;
        }

        protected void _exit()
        {
            GameKernel.GetInstance().OnUpdate -= OnUpdate;
            GameKernel.GetInstance().OnLateUpdate -= OnLateUpdate;
        }

        public virtual void Create(string stateName)
        {
        }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void OnUpdate(float deltaTime)
        { }

        public virtual void OnLateUpdate(float deltaTime)
        { }

        public virtual void SetChild(StateMachineBase _state)
        {
            
        }
    }

}
