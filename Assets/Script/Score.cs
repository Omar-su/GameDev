using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{

    public TextMeshProUGUI scoreText;  
    private double score; 


    private void Start() {
        score = 0;
        setScore(0);
    }
    
    public void increaseScore(double inc){
        score += inc;
        setScore(score);
    }

    public double getScore(){
        return score;
    }

    public void setScore(double scoreN)
    {
        scoreText.text = "Score: " + scoreN.ToString() + " HP";
    }
}
