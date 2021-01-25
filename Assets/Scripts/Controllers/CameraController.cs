using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    // This class exists to control the camera's position in game to optimize visibility of mazes of every size.
    // This is mainly here because I did not manage to optimize my camera and screen size automatically, especially for the larger mazes.
    // Originally this was supposed to use Rigidbody to move. However I ran out of time to smooth that out and fix the imperfect movement.
    // So I opted to use transform.position and Time.Deltatime to keep it from triggering too much. As this camera doesn't have physics, it works pretty much the same.
    // The world boundaries for movement are hardcoded and based on the biggest maze possible.
    // The camera will most often be positioned poorly at maze generation, which I do regret not fixing.
    // But the user is still able to hide the UI and move the camera around to wherever the maze looks good on their screen.


    [SerializeField] private float horizontalSpeed = 20, verticalSpeed = 20;
    private Vector3 currentPosition;



    private void Update() {
        currentPosition = transform.position;

        XAxisMovement();
        ZAxisMovement();
        YAxisMovement();

    }


    // Checks for input to apply horizonal movement on the X-Axis if within borders.
    private void XAxisMovement() {
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && currentPosition.x < 330) {
            transform.position = new Vector3(transform.position.x + horizontalSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && currentPosition.x > -40) {
            transform.position = new Vector3(transform.position.x - horizontalSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }
    }

    // Checks for input to apply horizonal movement on the Z-Axis if within borders.
    private void ZAxisMovement() {
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && currentPosition.z < 330) {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + horizontalSpeed * Time.deltaTime);
        }
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && currentPosition.z > -40) {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - horizontalSpeed * Time.deltaTime);
        }
    }

    // Checks for input to apply vertical movement on the Y-Axis if within borders.
    private void YAxisMovement() {
        if (Input.GetKey(KeyCode.O) && currentPosition.y > 30) {
            transform.position = new Vector3(transform.position.x, transform.position.y - verticalSpeed * Time.deltaTime, transform.position.z);
        }

        if (Input.GetKey(KeyCode.P) && currentPosition.y < 350) {
            transform.position = new Vector3(transform.position.x, transform.position.y + verticalSpeed * Time.deltaTime, transform.position.z);
        }
    }

    

}
