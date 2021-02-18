using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private SceneLoader sceneLoader;

    public string lev2;
    public string lev3;
    public string lev4;

    public GameObject[] menuPanels;                     // An array of all menu panels

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            sceneLoader.LoadScene(lev2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            sceneLoader.LoadScene(lev3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            sceneLoader.LoadScene(lev4);
        }
    }

    public void SelectPanel(int panelNumber)
    {
        for (int i = 0; i < menuPanels.Length; i++)     // For each item in menuPanels
        {
            if (panelNumber == i)                       // If the panelNumber passed in matches the item's index...
            {
                menuPanels[i].SetActive(true);          // ...then set it as active
            }
            else
            {
                menuPanels[i].SetActive(false);         // ...if not, set it as inactive
            }
        }
    }
}
