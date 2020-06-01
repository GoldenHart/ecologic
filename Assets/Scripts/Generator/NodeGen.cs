using UnityEngine;

public class NodeGen : MonoBehaviour
{
    // Variables
    public GameObject[] objects;

    private float xMin, xMax, zMin, zMax;

    private void Start()
    {
        // Initial Planting
        Plant();
    }

    public void Plant()
    {
        // Plants the plants...simple. Calls SolveForPosition to Determine Location of the Ground Being Spawned on.
        SolveForPosition();
        for (float x = xMin; x < xMax; x++)
        {
            for (float z = zMin; z < zMax; z++)
            {
                int rand = Random.Range(0, objects.Length);
                GameObject go = Instantiate(objects[rand], new Vector3(x, 1, z), Quaternion.identity);
                go.transform.parent = gameObject.transform;
            }
        }
    }
    public void SolveForPosition()
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
