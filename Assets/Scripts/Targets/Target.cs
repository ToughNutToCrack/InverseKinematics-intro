using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Target : MonoBehaviour{

    void OnMouseDown(){
        LevelManager.instance.currentTarget.val = transform;
    }
 
}
