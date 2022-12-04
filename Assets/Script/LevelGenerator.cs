using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator: MonoBehaviour {
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 100f;
    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private List<Transform> levelPartList;
    [SerializeField] private Transform finalLevel;
    
    private GameObject player;
    public Score playerScore;
    private Vector3 lastEndPosition;
    private int freeLabs = 0;
    private bool finalLevelCreated = false;

    public GameObject lab;
    public GameObject exam;
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
        if(playerScore.getScore() < 180) {
            Transform chosenLevelPart = levelPartList[Random.Range(0, levelPartList.Count)];
            Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition + chosenLevelPart.position - new Vector3(19,0,0));
            Vector3 v = lastEndPosition + chosenLevelPart.position;
            if(Random.Range(0,2) == 0){
                Instantiate(lab, new Vector3(Random.Range(v.x-4, v.x+3),Random.Range(v.y+2,v.y-2), 0), Quaternion.identity);
            } else if(Random.Range(0,2) == 0){
                Instantiate(exam, new Vector3(Random.Range(v.x-4, v.x+3),Random.Range(v.y+2,v.y-2), 0), Quaternion.identity);
            }

            lastEndPosition = lastLevelPartTransform.Find("LevelEnd").position;
        }else if(playerScore.getScore() >= 180 && !finalLevelCreated){
            Transform chosenLevelPart = finalLevel;
            Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition  + new Vector3(-8,0,0));
            finalLevelCreated = true;
        }
        
    }

    private Transform SpawnLevelPart (Transform levelPart , Vector3 spawnPosition) {
        Transform levelPartTransform = Instantiate (levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }

}
