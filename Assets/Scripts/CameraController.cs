using UnityEngine;

public class CameraController : MonoBehaviour{
    public Transform cameraTransform;
    public float sensitivity = 10f;

    bool move = false;
    float offset = 0;

    void Update(){
        move = Input.GetMouseButton(1);
        offset = Input.GetAxis("Mouse X");
    }

    void LateUpdate (){
        if(move){
            cameraTransform.RotateAround(Vector3.zero, Vector3.up, offset * sensitivity * Time.deltaTime);
        }
    }
}
