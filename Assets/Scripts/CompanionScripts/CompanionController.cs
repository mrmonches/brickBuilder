using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompanionController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent Agent;
    [SerializeField] private CompanionStates _currentState;

    [SerializeField] private float DistanceFromBrick;

    [SerializeField] private GameObject CameraTarget;

    private GameObject currentTarget;
    private BrickController currentController;

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

        if (brick.GetComponent<BrickController>().IsHeld)
        {
            brick = currentBricks[1];
        }

        for (int i = 1; i < currentBricks.Count; i++)
        {
            if (Vector3.Distance(transform.position, currentBricks[i].transform.position) <= 
                Vector3.Distance(transform.position, brick.transform.position) && !CurrentBricks[i].GetComponent<BrickController>().IsHeld)
            {
                brick = currentBricks[i];
            }
        }

        currentTarget = brick;
        currentController = currentTarget.GetComponent<BrickController>();
    }

    /// <summary>
    /// A function that returns if companion is close to current target
    /// </summary>
    /// <returns></returns>
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
        if(currentTarget == brick)
        {
            currentTarget = null;
        }

        currentBricks.Remove(brick);
    }

    private void ClearCompanionConditions()
    {
        currentTarget = null;

        _currentState = CompanionStates.Idle;

        Agent.ResetPath();
    }

    private void HoldBrick()
    {
        currentTarget.transform.position = HoldingPos.position;
    }

    /// <summary>
    /// A function that is called every frame
    /// Responsible for managing companion's current state and actions
    /// </summary>
    private void Update()
    {
        switch (_currentState)
        {
            case CompanionStates.Moving:
                if (!IsCloseToBrick())
                {
                    Agent.SetDestination(currentTarget.transform.position);
                }
                else if (IsCloseToBrick() && Agent.velocity == Vector3.zero)
                {
                    _currentState = CompanionStates.PickingUp;
                }

                if (currentController.IsHeld)
                {
                    ClearCompanionConditions();
                }
                break;
            case CompanionStates.PickingUp:
                if (!currentController.IsHeld)
                {
                    HoldBrick();
                }
                else if (currentController.IsHeld || currentController.IsPlaced)
                {
                    ClearCompanionConditions();
                }
                break;
            case CompanionStates.HoldingMoving:
                Agent.SetDestination(CameraTarget.transform.position);

                //if (transform.position ==  CameraTarget.transform.position)
                //{
                //    Agent.ResetPath();

                //    CurrentState = CompanionStates.HoldingIdle;
                //}

                if (!currentController.IsHeld)
                {
                    HoldBrick();
                }
                else if (currentController.IsHeld || currentController.IsPlaced)
                {
                    ClearCompanionConditions();
                }
                break;
            //case CompanionStates.HoldingIdle:
            //    if (transform.position != CameraTarget.transform.position)
            //    {
            //        CurrentState = CompanionStates.HoldingMoving;
            //    }
            //    break;
            default: break;
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
