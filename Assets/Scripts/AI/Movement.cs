﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
            print("I need sustenance and I am going to find it.");
            FindClosestFood();
        }
        else if (isThirsty)
        {
            print("I need liquid sustenance now.");
            FindClosestWater();
        }
        else if (canIdle)
        {
            print("All my sustenance has been procured. I'm going home now!");
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
        if (closestFood.transform.position == null)
        {
            print("There is no sustenance. I am dead.");
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

