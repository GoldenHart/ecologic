using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class WaterRecalc : MonoBehaviour
{
    public GameObject grasslands;
    bool continueCalc;
    void Start()
    {
        Vector3 loc = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        float x = gameObject.transform.position.x;
        float z = gameObject.transform.position.z;
        if (x > 30 || x < 10 || z > 30 || z < 10)
        {
            Instantiate(grasslands, loc, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
