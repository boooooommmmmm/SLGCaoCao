using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.Core;
using Framework.Record;
using UnityEngine.SceneManagement;

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
            Debug.Log("Exit battle state");
        }

        void Init()
        {
            battleID = moduleInstance.GetModule<RecordModule>("Record").GetCurrentBattle();

            //load battle config by static data module

            GenerateChess();
            CreateUI();
        }

        void GenerateChess()
        {
            //if is realse, load scene will change to single mode
            SceneManager.LoadScene("Battle001", LoadSceneMode.Additive);
        }

        void CreateUI()
        {
            //
        }
    }
}
