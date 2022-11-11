using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Human
{
    Ray ray;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         if(Physics.Raycast(ray, out hit))
         {
             if(Input.GetMouseButtonDown(0))
             {
                print(hit.collider.name);
                
             }
         }
    }
}
