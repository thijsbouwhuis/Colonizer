using System;
using UnityEngine;
using UnityEngine.Tilemaps;


//This is a singleton
public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem instance;

    public GridLayout gridLayout;
    private Grid grid;
    [SerializeField] private Tilemap mainTileMap;
    [SerializeField] private TileBase occupiedTile;

    public GameObject pref1;


    private PlaceableObject target;

    private void Awake()
    {
        instance = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    private void DestroyTarget()
    {
        if (target != null)
        {
            Destroy(target.gameObject);
            target = null;
        }
    }

    //Handles preview movement and placement
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DestroyTarget();
            InitializeWithObject(pref1);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            DestroyTarget();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (target != null)
            {
                target.transform.position = SnapCoordinateToGrid(GetMouseWorldPos());
                target = null;
            }
        }

        if (target != null)
        {
            Vector3 pos = SnapCoordinateToGrid(GetMouseWorldPos());
            target.transform.position = new Vector3(pos.x, target.transform.position.y, pos.z);
        }
    }

    public Vector3 GetMouseWorldPos()
    {
        return Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit raycastHit)
            ? raycastHit.point
            : Vector3.zero;
    }

    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        return grid.GetCellCenterWorld(gridLayout.WorldToCell(position)) - grid.cellSize / 2f;
    }

    public void InitializeWithObject(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab, SnapCoordinateToGrid(GetMouseWorldPos()), Quaternion.identity);
        target = obj.GetComponent<PlaceableObject>();
    }
}