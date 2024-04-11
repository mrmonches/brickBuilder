using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickCatcher : MonoBehaviour
{
    [SerializeField] private List<Transform> SpawnPoints = new List<Transform>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Brick"))
        {
            other.gameObject.transform.transform.position = SpawnPoints[0].position;
        }
    }
}
