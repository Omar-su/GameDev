using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScript : MonoBehaviour
{
    private GameObject player;
    private GameObject gameOver;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameOver = GameObject.FindGameObjectWithTag("End");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.Find("END").position, player.transform.position) < 3) {
            player.GetComponent<CharacterMovement>().playerEnd();
        }
        
    }
}
