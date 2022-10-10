using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    [SerializeField] private List<Vector2Int> unwalkableNodes;
    [SerializeField] private MeshRenderer canPlaceIndicator;
    [SerializeField] private GameObject objectToPlace;

    public MeshRenderer CanPlaceIndicator { get => canPlaceIndicator; }

    public void OnObjectPlaced(CustomGrid grid, Vector3 pos)
    {
        foreach (Vector2Int tile in unwalkableNodes)
        {
            grid.GetTile((int) pos.x + tile.y, (int) pos.z + tile.x).walkable = false;
            //TODO tell all units to recalc their path
        }
        
        //Inverse this because grid has Z as X and X as Z
        GameObject building = Instantiate(objectToPlace);
        building.transform.position = pos;
        building.GetComponent<Building>().OnBuildingPlaced();
    }

    public bool CanBePlaced(CustomGrid grid, Vector3 pos)
    {
        foreach (Vector2Int tile in unwalkableNodes)
        {
            //Inverse this because grid has Z as X and X as Z
            if (!grid.GetTile((int) pos.x + tile.y, (int) pos.z + tile.x).walkable) { return false;  }
        }
        return true;
    }
}
