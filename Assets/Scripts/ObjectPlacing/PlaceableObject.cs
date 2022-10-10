using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    [SerializeField] private List<Vector2Int> unwalkableNodes;
    
    public void OnObjectPlaced()
    {
        Debug.Log("Object is placed");
    }
}
