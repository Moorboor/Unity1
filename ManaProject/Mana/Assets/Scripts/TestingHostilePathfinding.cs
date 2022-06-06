using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingHostilePathfinding : MonoBehaviour
{

    private const float speed = 20f;
    private List<Vector3> pathVectorList;
    private Action onMovementComplete;
    private State state;

    private enum State
    {
        Idle,
        Moving,
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
        case State.Moving:
            HandleMovement(onMovementComplete);
            break;
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void HandleMovement(Action onMovementComplete)
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


    public void SetTargetPosition(Vector3 targetPosition, Action onMovementComplete)
    {
        // Move one node towards target
        this.onMovementComplete = onMovementComplete;
        pathVectorList = Pathfinding.Instance.FindPath(GetPosition(), targetPosition);
        Debug.Log(pathVectorList.Count);
        state = State.Moving;


        // if player not in sight, move in direction to node with highest fCost --> Exploring

    }

}
