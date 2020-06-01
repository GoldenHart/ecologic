using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RTNodeGen : MonoBehaviour
{
    private void Update()
    {
        Tree[] allTree = GameObject.FindObjectsOfType<Tree>();
        Food[] allFood = GameObject.FindObjectsOfType<Food>();
        NodeGen[] allGrassslands = GameObject.FindObjectsOfType<NodeGen>();
        if (allTree.Length == 0 || allFood.Length == 0)
        {
            for (int i = 0; i < allGrassslands.Length; i++)
            {
                GameObject go;
                go = allGrassslands[i].gameObject;
                go.GetComponent<NodeGen>().Plant();
            }
            
        }   
    }
    
}
