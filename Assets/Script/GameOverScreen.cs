using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverScreen : MonoBehaviour
{
    public CameraFollow camera;
    public void SetUp(){
        gameObject.SetActive(true);
        camera.TurnOff();
    }

    public void RestartScene(){
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit(){
        UnityEditor.EditorApplication.isPlaying = false;
    }

}
