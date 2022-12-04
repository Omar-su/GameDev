using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodAccess : MonoBehaviour
{
    public MeshRenderer enterText;
    EvilMachine[] allMachines;
    EvilMachine touchedMachine = null;

    const float HavocTime = 5;
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

    private Animator animator;
    private SpriteRenderer spr;

    const float machineEnterDistance = 5;

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed /= 60;

        animator = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        enterText.enabled = false;
        allMachines = GameObject.FindObjectsOfType<EvilMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        var height = 2 * Camera.main.orthographicSize;
        var width = height * Camera.main.aspect;
        //Movement
        animator.SetBool("Walking", false);
        if (canMove) {
            if (Input.GetKey(KeyCode.LeftArrow)) {
                transform.position += Vector3.left * movementSpeed;
                animator.SetBool("Walking", true);
                spr.flipX = true;
            }
            if (Input.GetKey(KeyCode.RightArrow)) {
                transform.position -= Vector3.left * movementSpeed;
                animator.SetBool("Walking", true);
                spr.flipX = false;
            }
            float x = transform.localPosition.x, maxX = Camera.main.orthographicSize * Screen.width / Screen.height;
            if (Math.Abs(x) > Math.Abs(maxX)) transform.position = new Vector3(Math.Sign(x) * maxX, transform.position.y, transform.position.z);
        }
        
        //Machine Entering/Leaving
        if (canMove) CheckForMachines();
        //Enter
        if (touchedMachine != null) {
            if (canMove && Input.GetKeyDown(KeyCode.DownArrow)) {
                transform.position = TouchedMachine.gameObject.transform.position;
                animator.SetBool("Bike", true);
                spr.flipX = false;

                touchedMachine.Pump();
                canMove = false;
                enterText.enabled = false;
                Debug.Log("EvilMachine: Attached to machine");
            }
            //Pump
            else if (canMove == false && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)) {
                touchedMachine.Pump();
            }
        }
        
        //Leave
        if (!canMove && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))) {
            canMove = true;
            animator.SetBool(touchedMachine.animationName, false);
            CheckForMachines();
            Debug.Log("EvilMachine: Detached to machine");
        }

        //Activate powers
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            foreach (EvilMachine em in allMachines) {
                em.CauseHavoc();
            }
            //TODO stand still animation
            canMove = false;
            StartCoroutine("StopHavocInTime");
        }
    }

    IEnumerator StopHavocInTime() {
        Debug.Log("Stop");
        yield return new WaitForSeconds(HavocTime);
        foreach (EvilMachine em in allMachines) {
            em.CauseHavoc();
        }
        Debug.Log("Hammer time");
        //TODO stand still animation
        canMove = true;
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
