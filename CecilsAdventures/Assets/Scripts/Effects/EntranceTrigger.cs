using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceTrigger : MonoBehaviour
{
    private Doug doug;

    public float zoomValue;
    public float xOffsetValue;
    public float yOffsetValue;

    public float zoomValue2;
    public float xOffsetValue2;
    public float yOffsetValue2;

    public bool triggered;
    public GameObject barrier;

    public GameObject bossBattleMusic;
    public GameObject healthBar;
    public Transform target;
    public float speed;
    public bool healthBarMoved;

    private void Start()
    {
        triggered = false;
        healthBarMoved = true;
        barrier.SetActive(false);
        doug = GameObject.Find("Doug").GetComponent<Doug>();
        //Invoke("ReturnToDefaultView", 14f);
        //Invoke("StartBossBattleMusic", 12f);
        //Destroy(gameObject, 15f);
    }

    private void Update()
    {
        MoveHealthBar();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player" && !triggered)
        {
            barrier.SetActive(true);
            GoToView();
            Invoke("ReturnToDefaultView", 8f);
            Invoke("StartBossBattleMusic", 8f);
            Destroy(gameObject, 9f);
            doug.ReleaseDoug();
            triggered = true;
        }
    }

    public void GoToView()
    {
        SM.playerCamera.cameraSize = zoomValue;
        SM.playerCamera.xOffset = xOffsetValue;
        SM.playerCamera.yOffset = yOffsetValue;
        doug.playSounds = true;
    }

    public void ReturnToDefaultView()
    {
        SM.playerCamera.cameraSize = zoomValue2;
        SM.playerCamera.xOffset = xOffsetValue2;
        SM.playerCamera.yOffset = yOffsetValue2;
        doug.canFire = true;
    }

    public void StartBossBattleMusic()
    {
        Instantiate(bossBattleMusic, transform.position, transform.rotation);
        healthBarMoved = false;
    }

    public void MoveHealthBar()
    {
        if(!healthBarMoved)
        {
            healthBar.transform.position = Vector3.MoveTowards(healthBar.transform.position, target.position, speed * Time.deltaTime);
        }

        if(Vector3.Distance(healthBar.transform.position, target.position) < 0.001f)
        {
            healthBarMoved = true;
        }

        
    }

}
