using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator: MonoBehaviour {
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 100f;
    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private List<Transform> levelPartList;
    private GameObject player;
    private Vector3 lastEndPosition;
    private int freeLabs;

    public GameObject lab;
    
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Awake() {
        lastEndPosition = levelPart_Start.Find("LevelEnd").position;
        int startingSpawnLevelParts = 3;
        for (int i = 0; i < startingSpawnLevelParts; i++) {
            SpawnLevelPart();
        }
    }
    private void Update() {
        if (Vector3.Distance(new Vector3(player.transform.position.x, player.transform.position.y), lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART) {
            // Spawn another level part
            SpawnLevelPart();
        }
    }

    private void SpawnLevelPart() {
        Transform chosenLevelPart = levelPartList[Random.Range(0, levelPartList.Count)];
        Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition + chosenLevelPart.position - new Vector3(19,0,0));
        if(Random.Range(0,1) == 0){
            Instantiate(lab, lastEndPosition + chosenLevelPart.position, Quaternion.identity);
        }
        
        lastEndPosition = lastLevelPartTransform.Find("LevelEnd").position;
    }

    private Transform SpawnLevelPart (Transform levelPart , Vector3 spawnPosition) {
        Transform levelPartTransform = Instantiate (levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }

}
