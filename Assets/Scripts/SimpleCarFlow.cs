using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarFlow : MonoBehaviour
{
    private GameObject bullet;
    private void FixedUpdate()
    {
            if(!bullet)
                transform.position += new Vector3(0, 0, 0.2f);
    }

}
