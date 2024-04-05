using System.Collections;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    private CompanionController companionController;
    private bool stateActive;

    [SerializeField] private float IdleTimer;

    private void Awake()
    {
        companionController = GetComponent<CompanionController>();
    }

    private void StateControl()
    {
        if (companionController.CurrentState == CompanionStates.Idle)
        {
            stateActive = true;
            StartCoroutine("IdleCountdown");
        }
    }

    private IEnumerator IdleCountdown()
    {
        if (stateActive)
        {
            yield return new WaitForSeconds(IdleTimer);
            companionController.CurrentState = CompanionStates.Moving;
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
