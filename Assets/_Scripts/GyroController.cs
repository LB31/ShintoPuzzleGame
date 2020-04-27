using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;

// Attach this controller to the main camera, or an appropriate
// ancestor thereof, such as the "player" game object.
public class GyroController : MonoBehaviour
{
    // Optional, allows user to drag left/right to rotate the world.
    private const float DRAG_RATE = .2f;
    float dragYawDegrees;
    float dragPitchDegrees;

    void Update() {

        // Update `dragYawDegrees` based on user touch.
        CheckDrag();

        transform.localRotation =
          // Allow user to drag left/right to adjust direction they're facing.
          Quaternion.Euler(-dragPitchDegrees, -dragYawDegrees, 0f) *

          // Neutral position is phone held upright, not flat on a table.
          Quaternion.Euler(90f, 0f, 0f) *

          // So image is not upside down.
          Quaternion.Euler(0f, 0f, 180f);
    }

    void CheckDrag() {
        if (Input.touchCount != 1) {
            return;
        }

        Touch touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Moved) {
            return;
        }
        if (IsPointerOverUIObject()) {
            return;
        }
        dragYawDegrees += touch.deltaPosition.x * DRAG_RATE;

        dragPitchDegrees += touch.deltaPosition.y * DRAG_RATE;
    }

    private bool IsPointerOverUIObject() {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
