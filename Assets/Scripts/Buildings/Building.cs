using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public virtual void OnBuildingPlaced()
    {
        Debug.Log("Building is placed");
    }
}
