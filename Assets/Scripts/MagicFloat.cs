using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFloat : MonoBehaviour
{
    public float hoverSpeed;
    public float hoverOffset;
    public float rotateSpeed;
    private float startHeight;

    private void Awake() {
        startHeight = transform.position.y;
    }
    private void FixedUpdate() {
        float height = startHeight + hoverOffset* Mathf.Cos(Time.time * hoverSpeed);
        transform.position = new Vector3(transform.position.x, height , transform.position.z);

        transform.Rotate(new Vector3(0, rotateSpeed,0), Space.World);

    }


}
