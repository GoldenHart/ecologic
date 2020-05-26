using UnityEngine;

public class MapGen : MonoBehaviour
{
    public int xNum;
    public int zNum;
    public GameObject[] objects;
    void Start()
    {        
        for (int x = 0; x < xNum; x += 10)
        {
            for (int z = 0; z < zNum; z += 10)
            {
                int rand = Random.Range(0, objects.Length);
                Instantiate(objects[rand], new Vector3(x,0,z), Quaternion.identity);
            }
        }
        
        Destroy(gameObject);
    }
}
