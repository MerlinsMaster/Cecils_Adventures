using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject playerGO;                   // Reference to the Player game object
    private PlayerHealth playerHealth;                              // Reference to the Player script

    public GameObject healthBarGO;                                  // Reference to the Health Bar game object

    public float healthBarSize;                                     // The actual size of the Health Bar in the rect transform
    public float healthPosition;                                    // The position of the Health Bar in the rect transform
    public float healthPositionSmooth;                              // An offset of that position
    public float healthSmoothSpeed;                                 // The speed that the offset moves to the position

    private void Start()
    {
        playerHealth = playerGO.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        healthPosition = ((playerHealth.health / playerHealth.maxHealth) * healthBarSize) - healthBarSize;      // Determines position of the health bar object
        healthPositionSmooth += (healthPosition - healthPositionSmooth) * Time.deltaTime * healthSmoothSpeed;   // Allows health bar to smoothly move to the new position
        healthBarGO.transform.localPosition = new Vector2(healthPositionSmooth, transform.localPosition.y);     // Move to the new position over time
    }

}
