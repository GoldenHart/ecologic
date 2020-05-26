using UnityEngine;

public class Food: MonoBehaviour {
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(("Species")))
        {
            Destroy(gameObject);
        }
    }
}

