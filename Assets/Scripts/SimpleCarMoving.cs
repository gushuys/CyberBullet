using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarMoving : MonoBehaviour
{
    void FixedUpdate()
    {
        if (gameObject.transform.parent.rotation == Quaternion.Euler(new Vector3(0, 180, 0)))
            transform.position += new Vector3(-0.15f, 0f, 0);
        else
            transform.position -= new Vector3(0.15f, 0f, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CarBreaker")
            transform.position = new Vector3(gameObject.transform.parent.position.x,transform.position.y,transform.position.z);
    }
}
