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
    [SerializeField] private Color placingAllowedColor;
    [SerializeField] private Color placingProhibitedColor;

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
                Vector3 pos = SnapCoordinateToGrid(GetMouseWorldPos());
                if (target.CanBePlaced(GridManager.instance.GetGrid(), pos))
                {
                    target.transform.position = pos;
                    target.OnObjectPlaced(GridManager.instance.GetGrid(), pos);
                    DestroyTarget();
                }
            }
        }

        if (target != null)
        {
            Vector3 pos = SnapCoordinateToGrid(GetMouseWorldPos());
            target.transform.position = new Vector3(pos.x, target.transform.position.y, pos.z);

            target.CanPlaceIndicator.material.color =
                target.CanBePlaced(GridManager.instance.GetGrid(), pos)
                    ? placingAllowedColor
                    : placingProhibitedColor;
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