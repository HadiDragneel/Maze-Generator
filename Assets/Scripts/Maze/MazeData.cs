using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty { EASY, HARD, SPEEDRUN }

[CreateAssetMenu(fileName = "MazeSettings")]
public class MazeData : ScriptableObject
{
    
    // This scriptable object is used to store data about the maze settings. It can be accessed by multiple places for ease of use.
    // I wanted to make a DontDestroyOnLoad object for this purpose, but a ScriptableObject that could be accessed seemed like a better choice.


    // These are the prefabs and values used to make a maze.
    [SerializeField] public GameObject verticalWall, horizontalWall, floor, startPlatform, endPlatform;
    [SerializeField] [Range(3, 100)] [Header("Width of the maze in blocks")] public int width;
    [SerializeField] [Range(3, 100)] [Header("Length of the maze in blocks")] public int length;
    [SerializeField] [Range(1, 4)] [Header("Width of individual cells. Recommended: 1-2")] public float size;

    // The difficulty enum would've been implemented more thoroughly into gameplay. But I ran out of time before I got to those ideas. Currently its used to set the size of maze depending on choice in main menu.
    public Difficulty currentDifficulty = Difficulty.EASY;
    
    public void SetEasyMode() {
        width = 20;
        length = 20;
        size = 1;
    }

    public void SetHardMode() {
        width = 40;
        length = 40;
        size = 1;
    }

    // Since the data is kept and accessed through this object it needed to be set here.
    // These functions make sure that only acceptable values are used.
    public void SetWidth(int newWidth) {
        if (newWidth >= 3 && newWidth <= 100) {
            width = newWidth;
        }
        else return;
    }

    public void SetLength(int newLength) {
        if (newLength >= 3 && newLength <= 100) {
            length = newLength;
        }
        else return;
    }

    public void SetSize(float newSize) {
        if (newSize >= 1 && newSize <= 4) {
            size = newSize;
        }
        else return;
    }

    public void SetDifficulty(Difficulty newDifficulty) {
        currentDifficulty = newDifficulty;
    }


}
