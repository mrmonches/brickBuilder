using UnityEngine;
using UnityEngine.AI;

public class CompanionController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent Agent;
    [SerializeField] private CompanionStates _currentState;
    [SerializeField] private bool IsMoving;

    [SerializeField] private GameObject Brick;

    public CompanionStates CurrentState { get => _currentState; set => _currentState = value; }

    private void Update()
    {
        if (_currentState == CompanionStates.Moving && !IsMoving)
        {
            Agent.destination = Brick.transform.position;
        }
    }
}

public enum CompanionStates
{
    Idle, 
    Moving,
    Holding
}
