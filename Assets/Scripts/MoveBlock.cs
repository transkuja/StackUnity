using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour {

    float speed = 10.0f;
    bool reverseMovement = false;
    bool moveAlongX = false;
    bool isInit = false;
    bool firstTriggerIgnored = false;

    public void Init(bool _moveAlongX)
    {
        moveAlongX = _moveAlongX;
        isInit = true;
    }

    private void Update()
    {
        if (isInit)
        {
            if (reverseMovement)
                transform.position += Time.deltaTime * ((moveAlongX) ? Vector3.right : Vector3.forward) * speed;
            else
                transform.position -= Time.deltaTime * ((moveAlongX) ? Vector3.right : Vector3.forward) * speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EndCourse")
        {
            if (!firstTriggerIgnored)
                firstTriggerIgnored = true;
            else
                reverseMovement = !reverseMovement;
        }
    }

}
