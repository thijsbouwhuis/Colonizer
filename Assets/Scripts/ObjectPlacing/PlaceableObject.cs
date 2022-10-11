using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    [SerializeField] private MeshRenderer canPlaceIndicator;
    [SerializeField] private GameObject objectToPlace;

    public MeshRenderer CanPlaceIndicator { get => canPlaceIndicator; }

    public void OnObjectPlaced(CustomGrid grid, Vector3 pos)
    {
        //Inverse this because grid has Z as X and X as Z
        GameObject building = Instantiate(objectToPlace);
        building.transform.position = pos;
        building.GetComponent<Building>().OnBuildingPlaced(grid, pos);
    }

    public bool CanBePlaced(CustomGrid grid, Vector3 pos)
    {
        if (pos.x < 0 || pos.z < 0) { return false;}
        
        foreach (Vector2Int tile in objectToPlace.GetComponent<Building>().UnwalkableNodes)
        {
            Tile curTile = grid.GetTile((int) pos.x + tile.y, (int) pos.z + tile.x);
            if (curTile == null) { return false;}
            //Inverse this because grid has Z as X and X as Z
            if (!curTile.walkable) { return false;  }
        }
        return true;
    }
}
