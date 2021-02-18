using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Vector2 velocity;

    public float smoothTimeX;
    public float smoothTimeY;

    public float xOffset;
    public float yOffset;
    public float xOffset_default;
    public float yOffset_default;

    private GameObject player;

    public bool bounds;
    public bool isFollowing;

    public float cameraSize;
    public float normalSize;
    public float speed;

    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;

    void Awake()
    {
        SM.playerCamera = this;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Camera.main.orthographicSize = cameraSize;
        xOffset = xOffset_default;
        yOffset = yOffset_default;
        isFollowing = true;
    }

    void LateUpdate()
    {
        if (isFollowing)
        {
            float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x + xOffset, ref velocity.x, smoothTimeX);
            float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y + yOffset, ref velocity.y, smoothTimeY);

            transform.position = new Vector3(posX, posY, transform.position.z);

            ChangeCameraSize();

            if (bounds)
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x), Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y), Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
            }
        }


    }

    public void ChangeCameraSize()
    {
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, cameraSize, speed * Time.deltaTime);
        float xOffset_temp = xOffset;
        xOffset = Mathf.Lerp(xOffset_temp, xOffset, speed * Time.deltaTime);
        float yOffset_temp = yOffset;
        yOffset = Mathf.Lerp(yOffset_temp, yOffset, speed * Time.deltaTime);
    }

    public void SetMinCameraPosition()
    {
        minCameraPos = gameObject.transform.position;
    }

    public void SetMaxCameraPosition()
    {
        maxCameraPos = gameObject.transform.position;
    }
}
