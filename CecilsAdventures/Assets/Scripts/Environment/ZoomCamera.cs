using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    public float zoomValue;
    public float xOffsetValue;
    public float yOffsetValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            SM.playerCamera.cameraSize = zoomValue;
            SM.playerCamera.xOffset = xOffsetValue;
            SM.playerCamera.yOffset = yOffsetValue;
        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.transform.tag == "Player")
    //    {
    //        SM.playerCamera.cameraSize = zoomValue;
    //        SM.playerCamera.xOffset = xOffsetValue;
    //        SM.playerCamera.yOffset = yOffsetValue;
    //    }
    //}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            SM.playerCamera.cameraSize = SM.playerCamera.normalSize;
            SM.playerCamera.xOffset = SM.playerCamera.xOffset_default;
            SM.playerCamera.yOffset = SM.playerCamera.yOffset_default;
        }
    }
}
