using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodAccess : MonoBehaviour
{
    CharacterMovement movement;
    // Start is called before the first frame update
    void Start()
    {
        
        GameObject player = GameObject.Find("Player");
        movement = player.GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("hej " + movement.ToString());
        if (Input.GetKeyDown(KeyCode.DownArrow)){
            movement.delay = 50;
            Debug.Log("då");
        }
    }
}
