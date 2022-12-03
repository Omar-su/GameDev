using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{   
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 300f;
    private GameObject player;
    private Vector3 lastEndPosition;

       
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lastEndPosition = transform.Find("LevelEnd").position;
    }

    private void Update() {
        
        if(Vector3.Distance(new Vector3(player.transform.position.x, player.transform.position.y), lastEndPosition) > PLAYER_DISTANCE_SPAWN_LEVEL_PART){
            Destroy(gameObject);
        }
        
    }
}
