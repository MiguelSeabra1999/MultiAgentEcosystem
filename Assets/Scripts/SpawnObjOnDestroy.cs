using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjOnDestroy : MonoBehaviour
{
    public List<GameObject> objects;
    private void OnDestroy() {
        foreach(GameObject obj in objects)
            Instantiate(obj,transform.position,transform.rotation);
    }
}
