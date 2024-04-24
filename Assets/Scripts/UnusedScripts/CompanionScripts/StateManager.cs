/*****************************************************************************
// File Name : StateManager.cs
// Author : Nolan J. Stein
// Creation Date : April 15, 2024
//
// Brief Description : This is another script that handles the behaviors
for the companion in game. Without this, the other script would not run.
*****************************************************************************/
using System.Collections;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    private CompanionController companionController;
    [SerializeField] private bool StateActive;

    [SerializeField] private float IdleTimer;
    [SerializeField] private float HoldingIdleTimer;

    /// <summary>
    /// A function that gets necessary references to components
    /// </summary>
    private void Awake()
    {
        companionController = GetComponent<CompanionController>();
    }

    /// <summary>
    /// A function responsible for the companion's different states
    /// </summary>
    private void StateControl()
    {
        switch (companionController.CurrentState)
        {
            case CompanionStates.Idle:
                StateActive = true;
                StartCoroutine("IdleCountdown");
                break;
            case CompanionStates.PickingUp:
                StateActive = true;
                StartCoroutine("HoldingIdleCountdown");
                break;
            default: break;
        }
    }

    /// <summary>
    /// A coroutine that waits a specified amount of time until marking the companion as moving
    /// </summary>
    /// <returns></returns>
    private IEnumerator IdleCountdown()
    {
        if (StateActive)
        {
            yield return new WaitForSeconds(IdleTimer);
            companionController.ClosestBrick();
            companionController.CurrentState = CompanionStates.Moving;
            StateActive = false;
        }
    }

    /// <summary>
    /// A coroutine that waits a specified amount of time until marking the companion as holding and moving
    /// </summary>
    /// <returns></returns>
    private IEnumerator HoldingIdleCountdown()
    {
        if (StateActive)
        {
            yield return new WaitForSeconds(HoldingIdleTimer);
            companionController.CurrentState = CompanionStates.HoldingIdle;
            StateActive = false;
        }
    }

    /// <summary>
    /// A function that is called every frame.
    /// </summary>
    private void Update()
    {
        if (!StateActive)
        {
            StateControl();
        }
    }
}
