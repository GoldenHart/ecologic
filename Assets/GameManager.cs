using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private void FixedUpdate()
    {
        CheckFood();
    }

    void CheckFood()
    {
        Food[] allFood = GameObject.FindObjectsOfType<Food>();
        if (allFood.Length == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
