using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodAccess : MonoBehaviour
{
    public MeshRenderer enterText;
    EvilMachine[] allMachines;
    EvilMachine touchedMachine = null;
    EvilMachine TouchedMachine {
        get => touchedMachine;
        set {
            if (value != null) enterText.enabled = true;
            else enterText.enabled = false;
            touchedMachine = value;
        }
    }

    public float movementSpeed;
    bool canMove = true;

    CharacterMovement movement;

    const float machineEnterDistance = 5;

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed /= 60;

        enterText.enabled = false;
        allMachines = GameObject.FindObjectsOfType<EvilMachine>();

        GameObject player = GameObject.Find("Player");
        movement = player.GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        var height = 2 * Camera.main.orthographicSize;
        var width = height * Camera.main.aspect;
        //Movement
        if (canMove) {
            if (Input.GetKey(KeyCode.LeftArrow)) transform.position += Vector3.left * movementSpeed;
            if (Input.GetKey(KeyCode.RightArrow)) transform.position -= Vector3.left * movementSpeed;
            float x = transform.localPosition.x, maxX = Camera.main.orthographicSize * Screen.width / Screen.height;
            if (Math.Abs(x) > Math.Abs(maxX)) transform.position = new Vector3(Math.Sign(x) * maxX, transform.position.y, transform.position.z);
        }
        
        //Machine Entering/Leaving
        if (canMove) CheckForMachines();
        //Enter
        if (touchedMachine != null) {
            if (canMove && Input.GetKeyDown(KeyCode.DownArrow)) {
                transform.position = TouchedMachine.gameObject.transform.position;
                touchedMachine.Pump();
                canMove = false;
                Debug.Log("EvilMachine: Attached to machine");
            }
            //Pump
            else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                touchedMachine.Pump();
            }
        }
        
        //Leave
        if (!canMove && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))) {
            canMove = true;
            Debug.Log("EvilMachine: Detached to machine");
        }
    }
    
    private void CheckForMachines() {
        
        foreach (EvilMachine em in allMachines) {
            if (Vector3.Distance(transform.position, em.transform.position) < machineEnterDistance) {
                TouchedMachine = em;
                return;
            }
        }
        TouchedMachine = null;
    }
}
