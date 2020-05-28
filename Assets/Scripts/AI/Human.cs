using UnityEngine;
using UnityEngine.AI;
public class Human : MonoBehaviour
{
    // Variables
    bool canBuild = true;
    bool canStay = false;
    bool isHungry = false;
    bool isThirsty = false;

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
        if (isThirsty)
        {
            FindClosestWater();
        }
        if (canBuild && WoodCt <= 5)
        {
            FindClosestTree();
        }
    }

    // Find Stuff Functions
    void FindClosestTree()
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

    // Is stuff being done?
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(("Food")) && isHungry)
        {
            Destroy(collision.gameObject);
            isHungry = false;
            isThirsty = true;
        }
        if(collision.gameObject.CompareTag(("Tree")) && canBuild)
        {
            Destroy(collision.gameObject);
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = home;
            WoodCt++;
            canBuild = false;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water") && isThirsty)
        {
            isThirsty = false;
            canStay = true;
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = home;
        }

        if (other.gameObject.CompareTag("Spawner") && WoodCt <= 5 && WoodCt >= -1)
        {
            canBuild = true;
        }

        if (other.gameObject.CompareTag("Spawner") && WoodCt == 5)
        {
            canBuild = false;
            isHungry = true;
            Instantiate(homeBuilding, home, Quaternion.identity);
            WoodCt = -1;
            
        }
        if (other.gameObject.CompareTag("Spawner") && WoodCt == -1 && canStay)
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
        }

    }
}
