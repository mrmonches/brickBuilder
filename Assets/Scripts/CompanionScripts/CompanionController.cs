using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompanionController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent Agent;
    [SerializeField] private CompanionStates _currentState;

    [SerializeField] private List<GameObject> currentBricks = new List<GameObject>();

    public CompanionStates CurrentState { get => _currentState; set => _currentState = value; }
    public List<GameObject> CurrentBricks { get => currentBricks; set => currentBricks = value; }

    /// <summary>
    /// A function that calculates the brick closest to companion character
    /// </summary>
    /// <returns></returns>
    private GameObject ClosestBrick()
    {
        GameObject brick = currentBricks[0];

        for (int i = 1; i < currentBricks.Count; i++)
        {
            if (Vector3.Distance(transform.position, currentBricks[i].transform.position) < 
                Vector3.Distance(transform.position, brick.transform.position))
            {
                brick = currentBricks[i];
            }
        }

        return brick;
    }

    private void Update()
    {
        if (_currentState == CompanionStates.Moving)
        {
            Agent.destination = ClosestBrick().transform.position;
        }
        else if (transform.position == Agent.destination)
        {
            _currentState = CompanionStates.HoldingIdle;
        }
    }


}

public enum CompanionStates
{
    Idle, 
    Moving,
    HoldingIdle, 
    HoldingMoving
}
