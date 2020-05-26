using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float startY;

    void Start()
    {
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x, startY + Mathf.PingPong(Time.time, 12));
        transform.Rotate(0, 0, 30 * Time.deltaTime);
    }
}
