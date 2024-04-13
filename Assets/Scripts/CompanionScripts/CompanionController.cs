using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompanionController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent Agent;
    [SerializeField] private CompanionStates _currentState;

    [SerializeField] private float DistanceFromBrick;

    [SerializeField] private Transform CameraTarget;

    private GameObject currentTarget;
    private BrickController currentController;

    [SerializeField] private Transform HoldingPos;

    [SerializeField] private List<GameObject> currentBricks = new List<GameObject>();

    private bool isHolding;

    [Header("Companion-Look Variables")]
    [SerializeField] private float LookSpeed;
    private Quaternion lookRotation;
    private Vector3 lookDirection;

    public CompanionStates CurrentState { get => _currentState; set => _currentState = value; }
    public List<GameObject> CurrentBricks { get => currentBricks; set => currentBricks = value; }

    /// <summary>
    /// A function that calculates the brick closest to companion character
    /// </summary>
    /// <returns></returns>
    public void ClosestBrick()
    {
        // If there are more than 0 elements in the currentBricks list
        if (currentBricks.Count != 0)
        {
            GameObject brick = currentBricks[0];

            // If the initial brick is held and there are equal or more than two bricks in the list
            if (brick.GetComponent<BrickController>().IsHeld && currentBricks.Count >= 2)
            {
                brick = currentBricks[1];
            }

            // If there is more than one brick in the list and brick does not equal the second item in the list
            if (currentBricks.Count > 1 && brick != currentBricks[1])
            {
                // For-loop that calculates the closest brick to 
                for (int i = 1; i < currentBricks.Count; i++)
                {
                    if (Vector3.Distance(transform.position, currentBricks[i].transform.position) <=
                        Vector3.Distance(transform.position, brick.transform.position) && 
                        !CurrentBricks[i].GetComponent<BrickController>().IsHeld)
                    {
                        brick = currentBricks[i];
                    }
                }
            }

            currentTarget = brick;
            currentController = currentTarget.GetComponent<BrickController>();
        }
    }

    /// <summary>
    /// A function that returns if companion is close to current target
    /// </summary>
    /// <returns></returns>
    private bool IsCloseToBrick()
    {
        if (currentTarget != null && 
            Vector3.Distance(transform.position, currentTarget.transform.position) > DistanceFromBrick)
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
            ClearCompanionConditions();
        }

        currentBricks.Remove(brick);
    }

    /// <summary>
    /// A function to clear companion conditions
    /// </summary>
    private void ClearCompanionConditions()
    {
        currentTarget = null;

        currentController = null;

        isHolding = false;

        _currentState = CompanionStates.Idle;

        Agent.ResetPath();
    }

    /// <summary>
    /// A function that makes the companion hold the brick above their head
    /// </summary>
    private void HoldBrick()
    {
        if (!isHolding)
        {
            currentTarget.transform.position = HoldingPos.position;
            currentController.DisableGravity();
            isHolding = true;
        }
    }

    /// <summary>
    /// A function that rotates the companion towards the player camera
    /// </summary>
    private void LookTowardsPlayer()
    {
        lookDirection = (CameraTarget.position - transform.position).normalized;

        lookRotation = Quaternion.LookRotation(lookDirection);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, LookSpeed * Time.deltaTime);
    }

    /// <summary>
    /// A function that is called every frame
    /// Responsible for managing companion's current state and actions
    /// </summary>
    private void Update()
    {
        if (currentController == null)
        {
            CurrentState = CompanionStates.Idle;
        }

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
            case CompanionStates.HoldingIdle:
                LookTowardsPlayer();

                if (!currentController.IsHeld)
                {
                    HoldBrick();
                }
                else if (currentController.IsHeld || currentController.IsPlaced)
                {
                    ClearCompanionConditions();
                }
                break;
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
}
