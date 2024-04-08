using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompanionController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent Agent;
    [SerializeField] private CompanionStates _currentState;

    [SerializeField] private float DistanceFromBrick;

    private GameObject currentTarget;
    private BrickController currentController;

    private bool holdingBrick;

    [SerializeField] private Transform HoldingPos;

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
        currentController = currentTarget.GetComponent<BrickController>();
    }

    private void PickupTargetBrick()
    {
        holdingBrick = true;
    }

    private bool IsCloseToBrick()
    {
        if (Vector3.Distance(transform.position, currentTarget.transform.position) > DistanceFromBrick)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// A function that removes a brick from the companion's range
    /// </summary>
    /// <param name="brick"></param>
    public void RemoveBrickFromRange(GameObject brick)
    {
        currentBricks.Remove(brick);
    }

    private void Update()
    {
        if (_currentState == CompanionStates.Moving && !IsCloseToBrick())
        {
            Agent.SetDestination(currentTarget.transform.position);
        }
        else if (_currentState == CompanionStates.Moving && IsCloseToBrick() && Agent.velocity == Vector3.zero)
        {
            _currentState = CompanionStates.PickingUp;

            PickupTargetBrick();
        }

        if (holdingBrick && !currentController.IsHeld)
        {
            currentTarget.transform.position = HoldingPos.position;
        }
        else if (holdingBrick && currentController.IsHeld)
        {
            currentTarget = null;

            holdingBrick = false;
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
