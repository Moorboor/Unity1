using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingHostilePathfinding : MonoBehaviour
{

    private const float speed = 20f;
    private List<Vector3> pathVectorList;
    private List<PathNode> pathNodeList;
    private Action onMovementComplete;
    private State state;
    private string target;

    private enum State
    {
        Idle,
        MovingToPlayer,
        PlayerMoving,
    }

    private void Start()
    {
        State state = State.Idle;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.MovingToPlayer:
                HandleHostileMovement(onMovementComplete);
                break;
            case State.PlayerMoving:
                HandlePlayerMovement(onMovementComplete);
                break;
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void HandleHostileMovement(Action onMovementComplete)
    {

        if (pathVectorList.Count > 2)
        {
            Vector3 targetPosition = new Vector3(pathVectorList[1].x, transform.position.y, pathVectorList[1].z);
            float reachedDistance = Vector3.Distance(targetPosition, GetPosition());

            if (reachedDistance > 1f)
            {
                Vector3 moveDir = (targetPosition - GetPosition()).normalized;
                transform.position = GetPosition() + moveDir * speed * Time.deltaTime;
            }
            else
            {
                transform.position = targetPosition;
                onMovementComplete();
                state = State.Idle;

            }
        }
        else
        {
            onMovementComplete();
            state = State.Idle;
        }
    }

    private void HandlePlayerMovement(Action onMovementComplete)
    {

        if (pathVectorList.Count > 1)
        {
            Vector3 targetPosition = new Vector3(pathVectorList[1].x, transform.position.y, pathVectorList[1].z);
            float reachedDistance = Vector3.Distance(targetPosition, GetPosition());

            if (reachedDistance > 1f)
            {
                Vector3 moveDir = (targetPosition - GetPosition()).normalized;
                transform.position = GetPosition() + moveDir * speed * Time.deltaTime;
            }
            else
            {
                transform.position = targetPosition;
                onMovementComplete();
                state = State.Idle;

            }
        }

    }
    private void Attack(Action onMovementComplete)
    {

    }


    public void SetTargetPosition(string target, Vector3 targetPosition, int startX, int startZ, int endX, int endZ,  Action onMovementComplete)
    {
        this.onMovementComplete = onMovementComplete;
        this.target = target;
        pathVectorList = Pathfinding.Instance.FindPath(GetPosition(), targetPosition);
        pathNodeList = Pathfinding.Instance.FindPath(startX, startZ, endX, endZ);

        // Attack Player:
        //currentNode = pathNodeList[0];
        for (int i=0; i<2; i++)
        {
        }
        // Move to Player
        state = State.MovingToPlayer;


        // if player not in sight, move in direction to node with highest fCost --> Exploring

    }
    public void SetPlayerPosition(Vector3 targetPosition, Action onMovementComplete)
    {
        this.onMovementComplete = onMovementComplete;
        pathVectorList = Pathfinding.Instance.FindPath(GetPosition(), targetPosition);
        state = State.PlayerMoving;
    }

}
