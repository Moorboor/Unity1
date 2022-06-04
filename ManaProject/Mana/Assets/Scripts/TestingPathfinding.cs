using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestingPathfinding : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private TestingHostilePathfinding testingHostilePathfinding;
    [SerializeField] private CharacterController controller;

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
                Vector3 moveY = new Vector3 (pathfinding.GetGrid().GetCellSize(), 0, 0);
                pathfinding.GetGrid().GetXZ(GetPosition()+moveY, out int x, out int z);

                //pathfinding.GetGrid().SetGridObject(new Vector3(x, ))
                controller.Move(moveY * Time.deltaTime);
                state = State.Busy;
            }


            

        } else
        {
            testingHostilePathfinding.SetTargetPosition(GetMouseWorldPosition());
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
