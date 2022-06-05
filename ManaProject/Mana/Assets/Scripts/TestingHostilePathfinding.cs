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
            HandleMovement(() => { });
            break;
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void HandleMovement(Action onMovementComplete)
    {
        this.onMovementComplete = onMovementComplete;
        state = State.Moving;

        if (pathVectorList != null)
        {
            Vector3 targetPosition = new Vector3(pathVectorList[0].x, transform.position.y, pathVectorList[0].z);
            float reachDistance = Vector3.Distance(targetPosition, GetPosition());
            if (reachDistance > 1f)
            {
                Vector3 moveDir = (targetPosition - GetPosition()).normalized;
                transform.position = GetPosition() + moveDir * speed * Time.deltaTime;
            } else
            {
                transform.position = targetPosition;
            }

        }
        onMovementComplete();
    }


    public void SetTargetPosition(Vector3 targetPosition)
    {
        // Move one node towards target
        pathVectorList = Pathfinding.Instance.FindPath(GetPosition(), targetPosition);
        // if player not in sight, move in direction to node with highest fCost --> Exploring
        state = State.Moving;
    }

}
