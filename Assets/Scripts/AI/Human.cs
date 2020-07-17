using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class Human : MonoBehaviour
{
    // Variables
    
    public int hunger, thirst, hungerLimit, thirstLimit;
    bool isHungry = false;
    bool isThirsty = false;
    bool canBuild = false;
    bool isHouse = false;
    bool hasTree = false;

    int WoodCt = 0;
    
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

        #region Boolean Food and Water and Buildability
        // Hunger
        if (hunger <= hungerLimit)
        {
            isHungry = true;
        }
        else if (hunger >= hungerLimit)
        {
            isHungry = false;
        }

        // Thirst
        if (thirst <= thirstLimit)
        {
            isThirsty = true;
        }
        else if (thirst >= thirstLimit)
        {
            isThirsty = false;
        }

        // Can I build?
        if (thirst >= thirstLimit && hunger >= hungerLimit && isHouse == false && hasTree == false)
        {
            canBuild = true;
        }
        else if (thirst <= thirstLimit && hunger <= hungerLimit || hasTree == true)
        {
            canBuild = false;
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
        
        if (canBuild && WoodCt <= 5)
        {
            FindClosestTree();
        }

    }
    #region Location Functions
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
    #endregion
    #region Collisions FoodNTree
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(("Food")) && isHungry)
        {
            Destroy(collision.gameObject);
            hunger = hunger += 25;
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = home;
        }
        if(collision.gameObject.CompareTag(("Tree")) && canBuild)
        {
            Destroy(collision.gameObject);
            canBuild = false;
            hasTree = true;
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = home;
        }
    }
    #endregion
    
    #region Triggers WaterNTree
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water") && isThirsty)
        {
            thirst = thirst += 25;
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = home;
        }

        if (other.gameObject.CompareTag("Spawner") && WoodCt <= 5 && WoodCt >= -1 && hasTree)
        {
            WoodCt++;
            hasTree = false;
        }
        
        if (other.gameObject.CompareTag("Spawner") && WoodCt == 5)
        {
            Instantiate(homeBuilding, home, Quaternion.identity);
            isHouse = true;
            WoodCt = -1;  
        }
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
