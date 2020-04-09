using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyCircle : MonoBehaviour{

    public Transform pivot;
    public float angle;
    public float sinSpeed = 1;
    public float sinAmplitude = 1;

    float startY = 0;
    float elapsedTime = 0;

    void Start(){
        startY = transform.position.y;
    }
 
    void Update(){
        elapsedTime += Time.deltaTime;
        
        var p = transform.position;
        p.y = startY + sinAmplitude * Mathf.Sin(elapsedTime * sinSpeed);
        transform.position = p;
        
        transform.RotateAround(pivot.position, Vector3.up, angle * Time.deltaTime);
    }
}
