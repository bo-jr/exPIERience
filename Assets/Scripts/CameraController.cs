using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    Vector2 mouseLook;
    Vector2 smoothVal;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;
    GameObject character;

    void Start()
    {
        character = this.transform.parent.gameObject;
    }

    void Update()
    {
        var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothVal.x = Mathf.Lerp(smoothVal.x, mouseDelta.x, 1f / smoothing);
        smoothVal.y = Mathf.Lerp(smoothVal.y, mouseDelta.y, 1f / smoothing);
        mouseLook += smoothVal;

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
    }
}
