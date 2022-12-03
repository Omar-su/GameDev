using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerTextMovement : MonoBehaviour
{
    public int TTL;
    float _TTL;
    Vector3 start, target;
    Vector3 scaleStart, scaleEnd;
    const float scaleIncrease = 0.03f;
    // Start is called before the first frame update
    void Start()
    {
        _TTL = TTL;
        start = transform.localPosition;
        target = start + Vector3.up;
        scaleStart = transform.localScale * transform.lossyScale.x;
        transform.localScale = scaleStart;
        scaleEnd = scaleStart + new Vector3(scaleIncrease, scaleIncrease, scaleIncrease);
    }

    // Update is called once per frame
    void Update()
    {
        _TTL--;
        if (_TTL == 0) Destroy(gameObject);
        transform.localPosition = Vector3.Lerp(target, start, _TTL / TTL);
        transform.localScale = Vector3.Lerp(scaleStart, scaleEnd, _TTL / TTL);
    }
}
