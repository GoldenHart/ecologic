using UnityEngine;

public class MapGen : MonoBehaviour
{
    public int xNum, zNum;

    public GameObject[] objects;
    public GameObject[] underground;

    void Start()
    {   
        // Top Level Requires Different Generation Code than Lower Layers
        // Top Level
        for (int x = 0; x < xNum; x += 10)
        {
            for (int z = 0; z < zNum; z += 10)
            {
                int rand = Random.Range(0, objects.Length);
                Instantiate(objects[rand], new Vector3(x,0,z), Quaternion.identity);
            }
        }
        // Lower Layers
        for (int x = -5; x < 50; x += 5)
        {
            for (int y = -1; y > -2; y -= 1)
            {
                for (int z = -5; z < 50; z += 5)
                {
                    int rand = Random.Range(0, underground.Length);
                    Instantiate(underground[rand], new Vector3(x, y, z), Quaternion.identity);
                }
            }
            
        }
    }
}
