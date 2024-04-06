using System.Collections;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    private CompanionController companionController;
    private bool stateActive;

    [SerializeField] private float IdleTimer;

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
        if (companionController.CurrentState == CompanionStates.Idle)
        {
            stateActive = true;
            StartCoroutine("IdleCountdown");
        }
        else if (companionController.CurrentState == CompanionStates.HoldingIdle)
        {
            stateActive = true;
            StartCoroutine("HoldingIdleCountdown");
        }
    }

    /// <summary>
    /// A coroutine that waits a specified amount of time until marking the companion as moving
    /// </summary>
    /// <returns></returns>
    private IEnumerator IdleCountdown()
    {
        if (stateActive)
        {
            yield return new WaitForSeconds(IdleTimer);
            companionController.CurrentState = CompanionStates.Moving;
            stateActive = false;
        }
    }

    /// <summary>
    /// A coroutine that waits a specified amount of time until marking the companion as holding and moving
    /// </summary>
    /// <returns></returns>
    private IEnumerator HoldingIdleCountdown()
    {
        if (stateActive)
        {
            yield return new WaitForSeconds(IdleTimer);
            companionController.CurrentState = CompanionStates.HoldingMoving;
            stateActive = false;
        }
    }

    private void Update()
    {
        if (!stateActive)
        {
            StateControl();
        }
    }
}
