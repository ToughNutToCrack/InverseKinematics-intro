using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidanceController : MonoBehaviour{

    public Transform currentTarget;
    public Transform skyTarget;

    public float speed;
    public float viewDistance;
    public int viewAngle;
    public int width;

    public float deltaDistance;

    public bool allowSkyTarget;
    
    [Space]
    public GameObject particles;

    bool straightforward = false;
    Vector3 baseDirection = Vector3.right;
    Vector3 movementDirection = Vector3.right;
    List<Vector3> possibleDirections;
    Vector3 heading;

    void Start(){
        possibleDirections = new List<Vector3>();
        if(allowSkyTarget)
            currentTarget = skyTarget;
        LevelManager.instance.currentTarget.propertyUpdated += (t) => onTargetChange(t);
    }

    void onTargetChange(Transform t){
        straightforward = false;

        if(t == null){
            if(allowSkyTarget)
            currentTarget = skyTarget;
        }else{
            currentTarget = t;
        }
    }

    void Update() {
        if(currentTarget != null){
            heading = currentTarget.position - transform.position;
            if (heading.sqrMagnitude < deltaDistance * deltaDistance){
                straightforward = false;
                if(currentTarget != skyTarget){
                    Destroy(currentTarget.gameObject);
                    if(particles != null){
                        var p = Instantiate(particles, currentTarget.position, Quaternion.identity);
                        Destroy(p, 2);
                    }
                    LevelManager.instance.currentTarget.val = null;
                }
            }
            
            transform.position = Vector3.MoveTowards(transform.position, movementDirection * 100, Time.deltaTime * speed);
            transform.LookAt(movementDirection);
        }
    }

    void LateUpdate() {
        if(currentTarget != null){
            
            if(!straightforward){
                RaycastHit hit;
                Vector3 direction;
                var heading = (currentTarget.position - transform.position);
                var distance = heading.magnitude;
                baseDirection = heading / distance;

                possibleDirections.Clear();
                for(int i=-viewAngle/2; i<viewAngle/2; i++){
                    if(i % width == 0){
                        direction = Quaternion.Euler(0, i, 0) * baseDirection;
                        if (Physics.Raycast(transform.position, direction, out hit, viewDistance)){
                            if(hit.transform != currentTarget.transform){
                                Debug.DrawRay(transform.position, direction * viewDistance, Color.red);
                            }else{
                                Debug.DrawRay(transform.position, direction * viewDistance, Color.green);
                                if(currentTarget != skyTarget){
                                    straightforward = true;
                                }
                                possibleDirections.Clear();
                                movementDirection = baseDirection;
                            }
                        }else{
                            Debug.DrawRay(transform.position, direction * viewDistance, Color.green);
                            possibleDirections.Add(direction);
                        }
                    }
                }
                if(!straightforward){
                    if(possibleDirections.Count > 0){
                        movementDirection = possibleDirections[possibleDirections.Count/2];
                    }
                }
            }
        }
    }

    // void OnDrawGizmos(){
    //     Vector3 direction;
    //     var heading = (targets[activeTarget].position - transform.position);
    //     var distance = heading.magnitude;
    //     var dir = heading / distance;
    //     baseDirection = dir;

    //     for(int i=-viewAngle/2; i<viewAngle/2; i++){
    //         if(i % width == 0){
    //             direction = Quaternion.Euler(0, i, 0) * baseDirection;
    //             Debug.DrawRay(transform.position, direction * viewDistance, Color.green);
    //         }
    //     }
       
    // }


}
