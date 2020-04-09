using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>{
    public Observable<Transform> currentTarget = new Observable<Transform>(null);
}
