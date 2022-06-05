using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestingPathfinding : MonoBehaviour
{

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
        pathfinding = new Pathfinding(20, 20, 10);
        state = State.WaitingForPlayer;
    }

    private void Update()
    {
        if (state == State.WaitingForPlayer)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                state = State.Busy;
                Vector3 moveX = new Vector3 (pathfinding.GetGrid().GetCellSize(), 0, 0);
                //pathfinding.GetGrid().GetXZ(GetPosition()+moveZ, out int x, out int z);
                testPlayerPathfinding.SetTargetPosition(testPlayerPathfinding.GetPosition() + moveX);
  
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                state = State.Busy;
                Vector3 moveX = new Vector3(-pathfinding.GetGrid().GetCellSize(), 0, 0);
                testPlayerPathfinding.SetTargetPosition(testPlayerPathfinding.GetPosition() + moveX);
            }
            if (Input.GetAxis("Vertical") > 0)
            {
                state = State.Busy;
                Vector3 moveZ = new Vector3(0, 0, pathfinding.GetGrid().GetCellSize());
                testPlayerPathfinding.SetTargetPosition(testPlayerPathfinding.GetPosition() + moveZ);
            }
            if (Input.GetAxis("Vertical") < 0)
            {
                state = State.Busy;
                Vector3 moveZ = new Vector3(0, 0, -pathfinding.GetGrid().GetCellSize());
                testPlayerPathfinding.SetTargetPosition(testPlayerPathfinding.GetPosition() + moveZ);

            }

        } else
        {
            testingHostilePathfinding.SetTargetPosition(testPlayerPathfinding.GetPosition());
            state = State.WaitingForPlayer;
        }
    }   
}
