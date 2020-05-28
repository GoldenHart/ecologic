using UnityEngine;

public class DestroyIfTouch : MonoBehaviour
{
    // Prevents trees overlapping by simply removing them if they do at runtime.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Stable")
        {
            Destroy(gameObject);
        }
    }
}
