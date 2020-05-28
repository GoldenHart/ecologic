using UnityEngine;

public class WaterCheck : MonoBehaviour
{
    // Variables
    Vector3 thisPos;
    public int colNum;
    public GameObject pond;
    bool canDestroy = false;
    
    // Checks the map generation to see if water should become a pond. 
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            colNum++;
            if(colNum == 4)
            {
                thisPos = gameObject.transform.position;
                Instantiate(pond, thisPos, Quaternion.identity);
                canDestroy = true;
            }
        }
    }

    // Destroys the ocean tile if a pond tile should be placed instead.
    private void Update()
    {
        if (canDestroy == true)
        {
            Destroy(gameObject);
        }
    }
}
