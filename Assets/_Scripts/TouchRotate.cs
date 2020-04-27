using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchRotate : MonoBehaviour
{
    private Touch theTouch;
    private Vector2 touchStartPosition, touchEndPosition;
    private float xRotation;

    void Start() {

    }

    void Update() {
        float mouseX = 0;
        float mouseY = 0;

        if (Input.touchCount > 0) {
            theTouch = Input.GetTouch(0);

            if (theTouch.phase == TouchPhase.Began) {
                touchStartPosition = theTouch.position;
            } else if (theTouch.phase == TouchPhase.Moved || theTouch.phase == TouchPhase.Ended) {
                touchEndPosition = theTouch.position;

                float x = touchEndPosition.x - touchStartPosition.x;
                float y = touchEndPosition.y - touchStartPosition.y;

                if (Mathf.Abs(x) > Mathf.Abs(y)) {
                    mouseX = x;
                } else {
                    mouseY = y;
                }
            }

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            Camera.main.transform.Rotate(Vector3.up * mouseX);

        }


    }
}
