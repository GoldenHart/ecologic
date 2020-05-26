using UnityEngine;

public class NodeGen : MonoBehaviour
{
    public GameObject[] objects;
    public float xMin, xMax, zMin, zMax;

    void Start()
    {
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

    void SolveForPosition()
    {
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
