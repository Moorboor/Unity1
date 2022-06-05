using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    private State state;
    // change that later
    private bool activeCharacter;
    private bool hostileCharacter;
    private bool playerCharacter;

    private enum State
    {
        Idle,
        Busy,
        Moving,
        WaitingForPlayer,
    } 

    private void Start()
    {
        SetActiveCharacter(playerCharacter);
        SpawnCharacter(true);
        SpawnCharacter(false);
        
    }

    private void Update()
    {
        if (state == State.WaitingForPlayer)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                state = State.Busy;
            }
        }
    }
    private void SpawnCharacter(bool isPlayer)
    {
        Vector3 position;
        if (isPlayer)
        {
            position = new Vector3(20, 0, 20);
        } else
        {
            position = new Vector3(150, 0, 150);
        }
        //Instantiate();
    }

    private void SetActiveCharacter(bool Character)
    {
        
    }

    private void ChooseNextActiveCharacter()
    {
        if (activeCharacter == playerCharacter)
        {
            SetActiveCharacter(hostileCharacter);
        } else
        {
            SetActiveCharacter(playerCharacter);
            state = State.WaitingForPlayer;
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
    public void Move()
    {
        
    }

}
