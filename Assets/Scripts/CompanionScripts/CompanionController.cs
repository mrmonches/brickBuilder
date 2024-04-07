using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompanionController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent Agent;
    [SerializeField] private CompanionStates _currentState;

    private GameObject currentTarget;

    [SerializeField] private List<GameObject> currentBricks = new List<GameObject>();

    public CompanionStates CurrentState { get => _currentState; set => _currentState = value; }
    public List<GameObject> CurrentBricks { get => currentBricks; set => currentBricks = value; }

    /// <summary>
    /// A function that calculates the brick closest to companion character
    /// </summary>
    /// <returns></returns>
    public void ClosestBrick()
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

        currentTarget = brick;
    }

    public void RemoveBrickFromRange(GameObject brick)
    {
        currentBricks.Remove(brick);
    }

    private void Update()
    {
        if (_currentState == CompanionStates.Moving && transform.position != currentTarget.transform.position)
        {
            Agent.SetDestination(currentTarget.transform.position);
        }
        else if (_currentState == CompanionStates.Moving && transform.position == currentTarget.transform.position)
        {
            _currentState = CompanionStates.PickingUp;
        }
    }
}

public enum CompanionStates
{
    Idle, 
    Moving,
    PickingUp,
    HoldingIdle, 
    HoldingMoving
}
