using Framework.TBS.Cells;
using Framework.TBS.Units;
using System.Collections.Generic;

namespace Framework.TBS.Grid.UnitGenerators
{
    public interface IUnitGenerator
    {
        List<Unit> SpawnUnits(List<Cell> cells);
    }
}
