using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.Core;
using Framework.Record;

namespace Framework.StateMachine
{
    public class BattleStateMachine : StateMachineBase
    {
        ModuleManager moduleInstance = ModuleManager.Get();
        int battleID = 0;
        public override void Enter()
        {
            base.Enter();

            Debug.Log("Enter battle state");

            //add round sates

            //init
            Init();
        }

        public override void Exit()
        {
            base.Exit();


        }

        void Init()
        {
            battleID = moduleInstance.GetModule<RecordModule>("Record").GetCurrentBattle();

            //load battle config by static data module

            GenerateChess();
        }

        void GenerateChess()
        {

        }
    }
}
