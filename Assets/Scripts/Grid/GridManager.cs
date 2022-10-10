using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private Grid gridComponent;

    private CustomGrid grid;

    public static GridManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        grid = new CustomGrid(20, 20, gridComponent.cellSize);
    }
    public CustomGrid GetGrid()
    {
        return grid;
    }
}
