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

            StateMachineManager.Get().SwitchToBrother("Battle");
        }

        public override void Exit()
        {
            base.Exit();
            Debug.Log("Exit start state");
        }
    }
}