using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform followTransform;
    public GameObject player;
    public float offsetSmoothing;

    // Update is called once per frame
    void FixedUpdate() {
        Vector3 targetPostition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPostition, offsetSmoothing * Time.deltaTime);
    }
}