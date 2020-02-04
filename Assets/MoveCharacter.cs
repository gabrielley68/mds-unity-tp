using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour {
    [Tooltip("Speed in Unit per second")]
    public float speed = 5f;

    void Start() {
        this.transform.position = new Vector3(2, 2, 0);
    }

    // Update is called once per frame
    void Update() {
        Vector3 move = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow)) {
            move += Vector3.up;
        }        
        if (Input.GetKey(KeyCode.DownArrow)) {
            move += Vector3.down;
        }      
        if (Input.GetKey(KeyCode.LeftArrow)) {
            move += Vector3.left;
            GetComponent<SpriteRenderer>().flipX = true;
        }      
        if (Input.GetKey(KeyCode.RightArrow)) {
            move += Vector3.right;
            GetComponent<SpriteRenderer>().flipX = false;
        }

        this.transform.position = this.transform.position +  speed * Time.deltaTime * move.normalized;
        GetComponent<Animator>().SetFloat("speed", move.magnitude);
        if (Input.GetKey(KeyCode.Space)) {
            GetComponent<Animator>().SetTrigger("roll");
        }
    }
}