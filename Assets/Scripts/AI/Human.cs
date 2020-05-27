using UnityEngine;
using UnityEngine.AI;
public class Human : MonoBehaviour
{
    // Variables
    bool canBuild = true;
    bool isHungry = false;
    bool isThirsty = false;
    bool canIdle = false;

    int WoodCt = 0;
    Vector3 home;

    public GameObject homeBuilding;

    // Home is where you started. Never forget it.
    private void Start()
    {
        home = gameObject.transform.position;
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
            if(gameObject.transform.position == home && WoodCt <= 5)
            {
                canIdle = false;
                canBuild = true;
            }
            if (gameObject.transform.position == home && WoodCt == 5)
            {
                Instantiate(homeBuilding, home, Quaternion.identity);
                WoodCt = 0;
            }

        }
        else if (canBuild)
        {
            FindClosestTree();
        }
    }

    private void FindClosestTree()
    {
        float distanceToClosestTree = Mathf.Infinity;
        Tree closestTree = null;
        Tree[] allTree = GameObject.FindObjectsOfType<Tree>();

        foreach (Tree currentTree in allTree)
        {
            float distanceToTree = (currentTree.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToTree < distanceToClosestTree)
            {
                distanceToClosestTree = distanceToTree;
                closestTree = currentTree;
            }
        }
        // Find Tree
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = closestTree.transform.position;
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
            isHungry = false;
            isThirsty = true;
        }
        if(collision.gameObject.CompareTag(("Tree")) && canBuild)
        {
            canBuild = false;
            Destroy(collision.gameObject);
            canIdle = true;
            WoodCt++;
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
