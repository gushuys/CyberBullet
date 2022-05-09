using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionSettings : MonoBehaviour
{
    public GameObject joystick;
    private void Start()
    {
        switch (Screen.height, Screen.width)
        {
            case (2400, 1080):

                break;
        }
    }
}
