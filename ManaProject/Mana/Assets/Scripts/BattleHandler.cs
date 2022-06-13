using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleHandler : MonoBehaviour
{
    // Agents
    [SerializeField] private CharacterBattle hostilePathfinding;
    [SerializeField] private CharacterBattle playerPathfinding;

    private State state;
    private Pathfinding pathfinding;

    private enum State
    {
        WaitingForPlayer,
        Busy,
        WaitingForHostile,
    }

    private void Start()
    {
        // Initialize Movement system
        pathfinding = new Pathfinding(50, 50, 10f);
        state = State.WaitingForPlayer;
    }

    private void Update()
    {
        if (state == State.WaitingForPlayer)
        {
            pathfinding.GetGrid().GetXZ(playerPathfinding.GetPosition(), out int x, out int z);
            //neighbourNodeList = pathfinding.GetNeighboursList(pathfinding.GetNode(x, z));

            if (Input.GetAxis("Horizontal") > 0)
            {
                state = State.Busy;
                PathNode pathNode = pathfinding.GetGrid().GetGridObject(x, z);
                pathNode.ClearAgent("Player");
                Vector3 moveX = new Vector3(pathfinding.GetGrid().GetCellSize(), 0, 0);
                playerPathfinding.SetPlayerPosition(playerPathfinding.GetPosition() + moveX, () =>
                {
                    pathfinding.GetGrid().GetXZ(playerPathfinding.GetPosition(), out int x, out int z);
                    PathNode pathNode = pathfinding.GetGrid().GetGridObject(x, z);
                    pathNode.SetAgent("Player");
                });
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                state = State.Busy;
                Vector3 moveX = new Vector3(-pathfinding.GetGrid().GetCellSize(), 0, 0);
                playerPathfinding.SetPlayerPosition(playerPathfinding.GetPosition() + moveX, () => { });
            }
            if (Input.GetAxis("Vertical") > 0)
            {
                state = State.Busy;
                Vector3 moveZ = new Vector3(0, 0, pathfinding.GetGrid().GetCellSize());
                playerPathfinding.SetPlayerPosition(playerPathfinding.GetPosition() + moveZ, () => { });
            }
            if (Input.GetAxis("Vertical") < 0)
            {
                state = State.Busy;
                Vector3 moveZ = new Vector3(0, 0, -pathfinding.GetGrid().GetCellSize());
                playerPathfinding.SetPlayerPosition(playerPathfinding.GetPosition() + moveZ, () => { });
            }
        }
        if (state == State.Busy)
        {
            state = State.WaitingForHostile;

            pathfinding.GetGrid().GetXZ(hostilePathfinding.GetPosition(), out int startX, out int startZ);
            PathNode pathNode = pathfinding.GetGrid().GetGridObject(startX, startZ);
            pathNode.ClearAgent("Hostile");

            pathfinding.GetGrid().GetXZ(playerPathfinding.GetPosition(), out int endX, out int endZ);
            hostilePathfinding.SetTargetPosition("Player", playerPathfinding.GetPosition(), startX, startZ, endX, endZ, () =>
            {
                pathfinding.GetGrid().GetXZ(hostilePathfinding.GetPosition(), out int x, out int z);
                PathNode pathNode = pathfinding.GetGrid().GetGridObject(x, z);
                pathNode.SetAgent("Hostile");
                state = State.WaitingForPlayer;
            });
        }
    }
}