
using Framework.TBS.Grid;
using Framework.TBS.Grid.GridStates;

namespace Framework.TBS.Players
{
    /// <summary>
    /// Class representing a human player.
    /// </summary>
    public class HumanPlayer : Player
    {
        public override void Play(CellGrid cellGrid)
        {
            cellGrid.CellGridState = new CellGridStateWaitingForInput(cellGrid);
        }
    }
}
