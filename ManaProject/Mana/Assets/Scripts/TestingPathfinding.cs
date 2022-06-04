using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingPathfinding : MonoBehaviour
{
    private Pathfinding pathfinding;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;

    private void Start()
    {
        pathfinding = new Pathfinding(10, 10);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = GetMouseWorldPosition();
            pathfinding.GetGrid().GetXZ(mouseWorldPosition, out int x, out int z);
            List<PathNode> path = pathfinding.FindPath(0, 0, x, z);
            if (path != null)
            {
                for (int i = 0; i < path.Count-1; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x, -1f, path[i].z) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x,-1f, path[i + 1].z) * 10f + Vector3.one * 5f, Color.green, 100f);
                }
            }
        }
    }
    public Vector3 GetMouseWorldPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, layerMask))
        {
            transform.position = raycastHit.point;
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
