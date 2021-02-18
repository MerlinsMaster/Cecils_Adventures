using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMego : MonoBehaviour
{
    public Sprite[] megoImages;
    public SpriteRenderer image;
    public GameObject spriteGO;
    private int index;

    private void Start()
    {
        image = spriteGO.GetComponent<SpriteRenderer>();
        index = Random.Range(0, megoImages.Length);
        image.sprite = megoImages[index];
    }

}
