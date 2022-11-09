using UnityEngine;
using UnityEngine.AI;
public class Herbivore: MonoBehaviour
{
    // Variables
    public bool isHungry = false;
    public bool isThirsty = false;
    public bool canIdle = false;

    public GameObject homeTrigger;

    Vector3 home;

    // Home is where you started. Never forget it.
    private void Start()
    {
        home = gameObject.transform.position;
        Instantiate(homeTrigger, home, Quaternion.identity);
    }

    // AI Logic (Boolean)
    private void Update()
    {
        
        // Go to Food
        if (isHungry)
        {
            FindClosestFood();
        }
        // Go to Water
        else if (isThirsty)
        {
            FindClosestWater();
        }
        // Go home.
        else if (canIdle)
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = home;
        }
    }

    // Find Food and Water Functions

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

    // Have I eaten?

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(("Food")) && isHungry)
        {
            Destroy(collision.gameObject);
            isHungry = false;
        }
        /*
        if (collision.gameObject.CompareTag(("Carnivore")))
        {
            Destroy(gameObject);
        }
        */
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water") && isThirsty)
        {
            isThirsty = false;
        }
        if (other.gameObject.CompareTag("UnitHome") && canIdle)
        {
            canIdle = false;
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
        }
    }
}

