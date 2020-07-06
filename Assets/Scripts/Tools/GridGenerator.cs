using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridGenerator : MonoBehaviour
{
    public GameObject CellPrefab;
    public Transform CellsParent;

    public Vector2 V2CellSize = new Vector2(3, 3);

    /// <summary>
    /// Snaps unit objects to the nearest cell.
    /// </summary>
    public void GenerateGrid()
    {
        //clear chid 
        foreach(Transform t in CellsParent)
        {
            Destroy(t);
        }

        for (int i = 0; i < V2CellSize.x; i++)
        {
            for (int j = 0; j < V2CellSize.y; j++)
            {
                var _go = Instantiate(CellPrefab, CellsParent);
                _go.transform.localPosition = new Vector3(i, 0, j);
                _go.transform.localRotation = Quaternion.identity;
            }
        }
    }

    public void SnapToGrid()
    { 

    }
}
