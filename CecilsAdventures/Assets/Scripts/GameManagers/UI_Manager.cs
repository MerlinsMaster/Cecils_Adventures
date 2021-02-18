using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public Text score;
    public Text ammo;
    public GameObject gun;
    public GameObject sword;

    private void Start()
    {
        Display();
    }

    private void Update()
    {
        Display();
    }

    public void Display()
    {
        score.text = SM.scoreManager.score.ToString("00000");
        ammo.text = SM.player.ammo.ToString("00");

        if (SM.player.weaponIndex == 1)
        {
            sword.SetActive(true);
            gun.SetActive(false);
        }
        else if (SM.player.weaponIndex == 2)
        {
            sword.SetActive(false);
            gun.SetActive(true);
        }
        else
        {
            sword.SetActive(false);
            gun.SetActive(false);
        }
    }
}
