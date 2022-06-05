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
        return new Vector3(transform.position.x, 0, transform.position.z);
    }

    private void HandleMovement(Action onMovementComplete)
    {
        this.onMovementComplete = onMovementComplete;
        state = State.Moving;

        if (pathVectorList != null)
        {
            Vector3 targetPosition = pathVectorList[0];
            Vector3 currentPosition = new Vector3(transform.position.x, 0, transform.position.z);
            float reachDistance = Vector3.Distance(targetPosition, currentPosition);
            if (reachDistance > 1f)
            {
                Vector3 moveDir = (targetPosition - currentPosition).normalized;
                transform.position = currentPosition + moveDir * speed * Time.deltaTime;
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
