using UnityEngine;

public class SimplifiedIK : MonoBehaviour{
    public int bonesChain = 2;

    float[] bonesLength; 
    float totalLength;
    Transform[] bones;
    Vector3[] positions;
    Vector3[] startDir;
    Quaternion[] startRot;

    void Awake(){
        initIK();
    }

    void initIK(){
        bones = new Transform[bonesChain + 1];
        positions = new Vector3[bonesChain + 1];
        bonesLength = new float[bonesChain];
        startDir = new Vector3[bonesChain + 1];
        startRot = new Quaternion[bonesChain + 1];

        var curr = transform;
        totalLength = 0;
        for (var i = bones.Length - 1; i >= 0; i--){
            bones[i] = curr;
            startRot[i] = getRotationOf(curr);
            if( i != bones.Length -1){
                startDir[i] = getPositionOf(bones[i + 1]) - getPositionOf(curr);
                bonesLength[i] = startDir[i].magnitude;
                totalLength += bonesLength[i];
            }
            curr = curr.parent;
        }
    }

    Quaternion getRotationOf(Transform t){
        return Quaternion.Inverse(t.rotation) * transform.root.rotation;
    }

    Vector3 getPositionOf(Transform t){
        return Quaternion.Inverse(transform.root.rotation) * (t.position - transform.root.position);
    }

    void setRotation(Transform t, Vector3 startDirection, Vector3 heading, Quaternion startRotation){
        t.rotation = transform.root.rotation * Quaternion.FromToRotation(startDirection, heading) * Quaternion.Inverse(startRotation);
    }

    void setPosition(Transform t, Vector3 p){
        t.position = transform.root.rotation * p + transform.root.position;
    }

    void LateUpdate(){
        solveIK();
    }

    void solveIK(){
        if (bonesLength.Length != bonesChain){
            initIK();
        }    
        
        for (int i = 0; i < bones.Length; i++){
            positions[i] = getPositionOf(bones[i]);
        }

        for (int i = positions.Length - 2; i >= 0; i--){
            var direction = (positions[i] - positions[i + 1]).normalized;
            positions[i] = positions[i + 1] + direction * bonesLength[i];

        }

        for (int i = 0; i < positions.Length; i++){
            if(i != positions.Length - 1){
                var heading = positions[i + 1] - positions[i];
                setRotation(bones[i], startDir[i], heading, startRot[i]);
            }
            setPosition(bones[i], positions[i]);
        }

    }
}
