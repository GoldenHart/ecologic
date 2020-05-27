using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHuman : MonoBehaviour
{
    public GameObject human;

    private void Start()
    {
        Instantiate(human, gameObject.transform.position, Quaternion.identity);
    }
}
