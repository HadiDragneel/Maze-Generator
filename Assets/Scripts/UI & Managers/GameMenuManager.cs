using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameMenuManager : MonoBehaviour
{
    // This class takes in UI input and starts the game/changes scene if necessary. 
    // It uses the MazeDrawer class to generate a maze. It updates the MazeData scriptable object based on the UI sliders.
    // This also shows/hides the UI for a better experience for the user.
    
    [SerializeField] private MazeDrawer generator;
    [SerializeField] private TextMeshProUGUI widthText, lengthText, sizeText;
    [SerializeField] private Slider widthSlider, lengthSlider, sizeSlider;
    [SerializeField] private MazeData gameData;
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private Image showHideButton;
    [SerializeField] private Sprite showImage, hideImage;
    private bool isUIHidden = false;



    private void Start() {
        SetInitialText();
        SetSlider();
        SetCameraPosition();
        generator.GenerateMaze();
    }

    // Used to determine position of camera whenever a maze is generated. I couldn't optimize the camera position anymore with my time. So instead I added a hide/show UI and camera controls.
    public void SetCameraPosition() {
        camera.transform.position = new Vector3(0, (gameData.length + gameData.width), 0);
    }

  
    // Upon entering from main menu, this updates the slider knobs to be positioned on the current maze values.
    public void SetSlider() {
        widthSlider.value = gameData.width;
        lengthSlider.value = gameData.length;
        sizeSlider.value = gameData.size; 
    }


    // Regenerates a maze after destroying the old one and repositions the camera.
    public void OnGenerateClick() {
        SetCameraPosition();
        generator.GenerateMaze(); 
    }


    // Goes back to Main Menu.
    public void OnExitClick() {
        SceneManager.LoadScene("MainMenu");
    }


    // Sets the text above the sliders to reflect the current maze values upon entering scene.
    private void SetInitialText() {
        lengthText.text = ("Length: " + gameData.length);
        widthText.text = ("Width: " + gameData.width);
        sizeText.text = ("Size: " + gameData.size);
    }


    // Updates the text and maze settings in the MazeData scriptable object for length as the UI slider is moved.
    public void OnLengthSlider(float newLength) {
        gameData.SetLength((int)newLength);
        lengthText.text = ("Length: " + newLength);
    }


    // Updates the text and maze settings in the MazeData scriptable object for width as the UI slider is moved.
    public void OnWidthSlider(float newWidth) {
        gameData.SetWidth((int)newWidth);
        widthText.text = ("Width: " + newWidth);
    }


    // Updates the text and maze settings in the MazeData scriptable object for size. Converts it to 1 decimal.
    public void OnSizeSlider(float rawSize) {
        var newSize = Mathf.Round(rawSize * 10.0f) * 0.1f;
        gameData.SetSize(newSize);
        sizeText.text = ("Size: " + newSize);
    }

    // Shows or hides the UI (Excluding the show/hide button) by getting all the Panel children and disabling them.
    public void OnHideShowClick() {
        if (!isUIHidden) {
            for (int i = 0; i < uiPanel.transform.childCount; i++) {
                if (uiPanel.transform.GetChild(i).name != "ShowHideUIButton") {
                    uiPanel.transform.GetChild(i).gameObject.SetActive(false);
                }
                if (uiPanel.transform.GetChild(i).name == "ShowHideUIButton") {
                    showHideButton.sprite = showImage;
                    
                }
            }
            isUIHidden = true;

        }

        else if (isUIHidden) {
            for (int i = 0; i < uiPanel.transform.childCount; i++) {
                if (uiPanel.transform.GetChild(i).name != "ShowHideUIButton") {
                    uiPanel.transform.GetChild(i).gameObject.SetActive(true);
                }
                if (uiPanel.transform.GetChild(i).name == "ShowHideUIButton") {
                    showHideButton.sprite = hideImage;
                }
            }

            isUIHidden = false;
        }
    }

}
