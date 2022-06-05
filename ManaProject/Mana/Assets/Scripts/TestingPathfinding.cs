using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestingPathfinding : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private TestingHostilePathfinding testingHostilePathfinding;
    [SerializeField] private TestingHostilePathfinding testPlayerPathfinding;

    private State state;
    private Pathfinding pathfinding;

    private enum State
    {
        WaitingForPlayer,
        Busy,
    }

    private void Start()
    {
        pathfinding = new Pathfinding(20, 20);
        state = State.WaitingForPlayer;
    }

    private void Update()
    {
        if (state == State.WaitingForPlayer)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                Vector3 moveX = new Vector3 (pathfinding.GetGrid().GetCellSize(), 0, 0);
                //pathfinding.GetGrid().GetXZ(GetPosition()+moveZ, out int x, out int z);
                testPlayerPathfinding.SetTargetPosition(testPlayerPathfinding.GetPosition() + moveX);
                state = State.Busy;
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                Vector3 moveX = new Vector3(-pathfinding.GetGrid().GetCellSize(), 0, 0);
                testPlayerPathfinding.SetTargetPosition(testPlayerPathfinding.GetPosition() + moveX);
                state = State.Busy;
            }
            if (Input.GetAxis("Vertical") > 0)
            {
                Vector3 moveZ = new Vector3(0, 0, pathfinding.GetGrid().GetCellSize());
                testPlayerPathfinding.SetTargetPosition(testPlayerPathfinding.GetPosition() + moveZ);
                state = State.Busy;
            }
            if (Input.GetAxis("Vertical") < 0)
            {
                Vector3 moveZ = new Vector3(0, 0, -pathfinding.GetGrid().GetCellSize());
                testPlayerPathfinding.SetTargetPosition(testPlayerPathfinding.GetPosition() + moveZ);
                state = State.Busy;
            }




        } else
        {
            testingHostilePathfinding.SetTargetPosition(testPlayerPathfinding.GetPosition());
            state = State.WaitingForPlayer;
        }
    }   

    public Vector3 GetPosition()
    {
        return transform.position;
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
