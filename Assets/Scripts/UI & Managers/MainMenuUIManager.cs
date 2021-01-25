using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{

    // Accesses the MazeData scriptable object to set settings for game.
    [SerializeField] private MazeData data;

    
    // Sets the current difficulty to hard. This class was planned for more, however I ran out of time before I got to my ideas for this scene.
    public void OnHardDifficultyClick() {
        data.SetDifficulty(Difficulty.HARD);
        data.SetHardMode();
        SceneManager.LoadScene("MazeScene");
    }

    // See OnHardDifficultyClick();
    public void OnEasyDifficultyClick() {
        data.SetDifficulty(Difficulty.EASY);
        data.SetEasyMode();
        SceneManager.LoadScene("MazeScene");
    }

    // Exits the application.
    public void OnExitClick() {
        Application.Quit();
    }
}
