using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TbsFramework.Cells;
using TbsFramework.Grid.UnitGenerators;
using TbsFramework.Units;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;


public class GridGenerator : MonoBehaviour, IUnitGenerator
{
    public GameObject CellPrefab;
    public Transform UnitsParent;
    public Transform CellsParent;

    public Vector2 V2CellSize = new Vector2(3, 3);

    /// <summary>
    /// Snaps unit objects to the nearest cell.
    /// </summary>
    public void GenerateGrid()
    {
        //clear chid 
        foreach (Transform t in CellsParent)
        {
            Destroy(t.gameObject);
        }

        for (int i = 0; i < V2CellSize.x; i++)
        {
            for (int j = 0; j < V2CellSize.y; j++)
            {
                GameObject _go = PrefabUtility.InstantiatePrefab(CellPrefab, CellsParent) as GameObject;
                _go.transform.localPosition = new Vector3(i, 0, j);
                _go.transform.localRotation = Quaternion.identity;
            }
        }
    }

    public void SnapToGrid()
    {

    }

    public List<Unit> SpawnUnits(List<Cell> cells)
    {
        List<Unit> ret = new List<Unit>();
        for (int i = 0; i < UnitsParent.childCount; i++)
        {
            if (UnitsParent.GetChild(i).gameObject.activeSelf == false)
                continue;

            var unit = UnitsParent.GetChild(i).GetComponent<Unit>();
            if (unit != null)
            {
                var cell = cells.OrderBy(h => Math.Abs((h.transform.position - unit.transform.position).magnitude)).First();
                {
                    cell.IsTaken = true;
                    unit.Cell = cell;
                    cell.CurrentUnit = unit;
                    unit.transform.position = cell.transform.position;
                    unit.Initialize();
                    ret.Add(unit);
                }//Unit gets snapped to the nearest cell
            }
            else
            {
                Debug.LogError("Invalid object in Units Parent game object");
            }

        }
        return ret;
    }
}
