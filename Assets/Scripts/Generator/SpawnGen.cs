using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGen : MonoBehaviour
{
    // Variables
    public GameObject[] objects;
    public float xMin, xMax, zMin, zMax;

    void Start()
    {
        // Spawns the herbivore...simple. Calls SolveForPosition to Determine Location of the Ground Being Spawned on.
        SolveForPosition();
        for (float x = xMin; x < xMax; x++)
        {
            for (float z = zMin; z < zMax; z++)
            {
                int rand = Random.Range(0, objects.Length);
                Instantiate(objects[rand], new Vector3(x, 1, z), Quaternion.identity);
            }
        }
    }

    void SolveForPosition()
    {
        // Math to Determine Position. Pretty Straightforward.
        xMin = gameObject.transform.position.x;
        xMin = xMin -= 4.1f;
        xMax = gameObject.transform.position.x;
        xMax = xMax += 4.1f;
        zMin = gameObject.transform.position.z;
        zMin = zMin -= 4.1f;
        zMax = gameObject.transform.position.z;
        zMax = zMax += 4.1f;

    }
}
