using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    public GameObject explosion;
    public TextMeshProUGUI scoreText;  
    private double score; 


    private void Start() {
        score = 0;
        setScore(0);
    }
    bool reached60 = false, reached120 = false;
    public void increaseScore(double inc){
        score += inc;
        setScore(score);
        StartCoroutine("IncreaseSpeed");
    }

    IEnumerator IncreaseSpeed() {
        const float speedGain = 1.3f;
        const float exploInterval = 0.5f;
        const float exploPerHP = 0.1f;
        Camera.main.gameObject.GetComponent<CameraFollow>().speed *= speedGain;
        GameObject.Find("Player").GetComponent<CharacterMovement>().movementSpeed *= speedGain;
        //GameObject.Find("Player").GetComponent<Rigidbody2D>().gravityScale *= speedGain;
        bool speedUP = false;
        if (reached60 == false && score >= 60) {
            speedUP = true;
            reached60 = true;
        }
        else if (reached120 == false && score >= 120) {
            speedUP = true;
            reached120 = true;
        }
        else if (score >= 180) {
            //TODO END GAME
        }
        if (speedUP) {
            Debug.Log("DOUBLE TIME");
            for (int i = 0; i < exploPerHP * score; i++) {
                Vector3 camPos = Camera.main.transform.position;
                float x = camPos.x + Random.Range(-5, 5), y = camPos.y + Random.Range(-5, 5);
                Instantiate(explosion, new Vector3(x, y, 0), new Quaternion());
                yield return new WaitForSeconds(exploInterval);
            }
        }
    }

    public double getScore(){
        return score;
    }

    public void setScore(double scoreN)
    {
        scoreText.text = "Score: " + scoreN.ToString() + " HP";
    }
}
