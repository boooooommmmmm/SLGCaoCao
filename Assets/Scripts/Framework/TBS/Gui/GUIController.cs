using Framework.TBS.Grid;
using Framework.TBS.Grid.GridStates;
using System;
using UnityEngine;

namespace Framework.TBS.Gui
{
    public class GUIController : MonoBehaviour
    {
        public CellGrid CellGrid;

        void Awake()
        {
            CellGrid.LevelLoading += onLevelLoading;
            CellGrid.LevelLoadingDone += onLevelLoadingDone;
        }

        private void onLevelLoading(object sender, EventArgs e)
        {
            Debug.Log("Level is loading");
        }

        private void onLevelLoadingDone(object sender, EventArgs e)
        {
            Debug.Log("Level loading done");
            Debug.Log("Press 'n' to end turn");
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.N) && !(CellGrid.CellGridState is CellGridStateAiTurn))
            {
                CellGrid.EndTurn();//User ends his turn by pressing "n" on keyboard.
            }
        }
    }
}