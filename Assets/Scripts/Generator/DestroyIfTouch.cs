using UnityEngine;

public class DestroyIfTouch : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Stable")
        {
            Destroy(gameObject);
        }
    }
}
