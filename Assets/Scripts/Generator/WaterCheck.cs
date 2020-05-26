using UnityEngine;

public class WaterCheck : MonoBehaviour
{
    public int colNum;
    public GameObject pond;
    Vector3 thisPos;
    bool canDestroy = false;
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

    private void Update()
    {
        if (canDestroy == true)
        {
            Destroy(gameObject);
        }
    }
}
