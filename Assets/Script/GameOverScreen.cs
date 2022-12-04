using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public CameraFollow camera;
    public TextMeshProUGUI endScoreText;  
    public Score score;

    public void SetUp(){
        gameObject.SetActive(true);
        setEndScore(score.getScore());
        camera.TurnOff();
    }

    private void setEndScore(double finalScore){
        endScoreText.text = "FINAL SCORE: " + finalScore.ToString() + " HP";
    }

    public void RestartScene(){
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit(){
        UnityEditor.EditorApplication.isPlaying = false;
    }

}
