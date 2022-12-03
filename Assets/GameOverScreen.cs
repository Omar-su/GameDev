using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverScreen : MonoBehaviour
{
    public void SetUp(){
        gameObject.SetActive(true);
    }

    public void RestartScene(){
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit(){
        UnityEditor.EditorApplication.isPlaying = false;
    }

}
