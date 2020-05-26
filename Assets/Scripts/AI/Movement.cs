using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/// <summary>
/// BASE CODE FOR MOVEMENT
/// NavMeshAgent agent = GetComponent<NavMeshAgent>();
/// agent.destination = goal.position;
/// </summary>

public class Movement : MonoBehaviour
{
    bool isHungry = true;
    bool isThirsty = false;
    bool canIdle = false;
    Vector3 home;

    private void Start()
    {
        home = gameObject.transform.position;
    }
    private void Update()
    {
        if (isHungry)
        {
            FindClosestFood();
        }
        else if (isThirsty)
        {
            FindClosestWater();
        }
        else if (canIdle)
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = home;
        }
    }
    void FindClosestFood()
    {
        float distanceToClosestFood = Mathf.Infinity;
        Food closestFood = null;
        Food[] allFood = GameObject.FindObjectsOfType<Food>();

        foreach (Food currentFood in allFood)
        {
            float distanceToFood = (currentFood.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToFood < distanceToClosestFood)
            {
                distanceToClosestFood = distanceToFood;
                closestFood = currentFood;
            }
        }
        // Find Food
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = closestFood.transform.position;
        if (agent.destination == null)
        {
            Destroy(gameObject);
        }
    }
    void FindClosestWater()
    {
        float distanceToClosestWater = Mathf.Infinity;
        Water closestWater = null;
        Water[] allWater = GameObject.FindObjectsOfType<Water>();

        foreach (Water currentWater in allWater)
        {
            float distanceToWater = (currentWater.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToWater < distanceToClosestWater)
            {
                distanceToClosestWater = distanceToWater;
                closestWater = currentWater;
            }
        }
        // Find Water
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = closestWater.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(("Food")) && isHungry)
        {
            isHungry = false;
            isThirsty = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water") && isThirsty)
        {
            isThirsty = false;
            canIdle = true;
        }
    }
}

