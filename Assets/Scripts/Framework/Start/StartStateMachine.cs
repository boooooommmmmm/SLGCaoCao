using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.Core;

namespace Framework.StateMachine
{
    public class StartStateMachine : StateMachineBase
    {
        public override void Enter()
        {
            base.Enter();
            Debug.Log("Enter start state");

            //push start UI
            GameKernel.GetInstance().UIKernel.PushStartPanel();            
        }

        public override void Exit()
        {
            base.Exit();
            Debug.Log("Exit start state");

            //clear start UI
            GameKernel.GetInstance().UIKernel.PopStartPanel();
        }
    }
}