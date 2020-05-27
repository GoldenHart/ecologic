using UnityEngine;
using UnityEngine.AI;

public class CarnivoreMovement : MonoBehaviour
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
            if (gameObject.transform.position == home)
            {
                Destroy(gameObject);
            }
        }
    }
    void FindClosestFood()
    {
        float distanceToClosestMovement = Mathf.Infinity;
        Movement closestMovement = null;
        Movement[] allMovement = GameObject.FindObjectsOfType<Movement>();

        foreach (Movement currentMovement in allMovement)
        {
            float distanceToMovement = (currentMovement.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToMovement < distanceToClosestMovement)
            {
                distanceToClosestMovement = distanceToMovement;
                closestMovement = currentMovement;
            }
        }
        // Find Movement
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = closestMovement.transform.position;
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
        if (collision.gameObject.CompareTag(("Species")) && isHungry)
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

