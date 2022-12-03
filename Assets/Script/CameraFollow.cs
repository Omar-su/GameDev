using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform followTransform;
    public GameObject player;
    public float offsetSmoothing;
    public float speed = 1;

    // Update is called once per frame
    void FixedUpdate() {
        Vector3 targetPostition = new Vector3(transform.position.x + Time.deltaTime + speed, followTransform.position.y + Time.deltaTime, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPostition, offsetSmoothing * Time.deltaTime);
    }
}