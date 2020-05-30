using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class Human : MonoBehaviour
{
    // Variables
    // bool canBuild = false;
    // bool canStay = false;
    public int hunger, thirst;
    bool isHungry = false;
    bool isThirsty = false;

    // int WoodCt = 0;
    
    Vector3 home;

    public GameObject homeBuilding;

    private IEnumerator coroutine;

    // Home is where you started. Never forget it.
    private void Start()
    {
        hunger = 100;
        thirst = 105;
        
        coroutine = DecreaseVitals(5.0f);
        StartCoroutine(coroutine);
        
        home = gameObject.transform.position;
    }

    // AI Logic (Boolean)
    private void Update()
    {  

        #region Boolean Food and Water
        // Hunger
        if (hunger <= 99)
        {
            isHungry = true;
        }
        else if (hunger >= 99)
        {
            isHungry = false;
        }

        // Thirst
        if (thirst <= 99)
        {
            isThirsty = true;
        }
        else if (thirst >= 99)
        {
            isThirsty = false;
        }
        #endregion
        #region Eating Out
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
        #endregion
        
        /*
        if (canBuild && WoodCt <= 5)
        {
            FindClosestTree();
        }
        */
    }
    #region Location Functions
    /*
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
    */
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
    #endregion
    #region Collisions FoodNTree
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(("Food")) && isHungry)
        {
            Destroy(collision.gameObject);
            hunger = hunger += 25;
        }
        /*
        if(collision.gameObject.CompareTag(("Tree")) && canBuild)
        {
            Destroy(collision.gameObject);
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = home;
            WoodCt++;
            canBuild = false;
        }
        */
    }
    #endregion
    
    #region Triggers WaterNTree
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water") && isThirsty)
        {
            thirst = thirst += 25;
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = home;
            print("I wont go home");
        }
        /*
        if (other.gameObject.CompareTag("Spawner") && WoodCt <= 5 && WoodCt >= -1)
        {
            // canBuild = true;
        }

        if (other.gameObject.CompareTag("Spawner") && WoodCt == 5)
        {
            canBuild = false;
            Instantiate(homeBuilding, home, Quaternion.identity);
            WoodCt = -1;  
        }
        */
    }
    #endregion

    private IEnumerator DecreaseVitals(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            hunger = hunger -= 5;
            thirst = thirst -= 5;
        }
    }


}
