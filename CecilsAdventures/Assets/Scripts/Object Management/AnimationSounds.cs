using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSounds : MonoBehaviour
{
    public int index;
    public GameObject[] sounds;

    public void StartSound(int index)
    {
        Instantiate(sounds[index], transform.position, Quaternion.identity);       // instantiate the element in the array specified by that index
    }
}
