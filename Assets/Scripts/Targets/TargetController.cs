using UnityEngine;

public class TargetController : MonoBehaviour{
    public float speed = 1;

    Vector3 movement;

    void Update(){
        movement = new Vector3 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Input.GetAxis("Depth"));
        transform.position += movement * speed * Time.deltaTime;
    }
}
