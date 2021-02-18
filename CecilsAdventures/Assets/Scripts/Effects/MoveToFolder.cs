using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToFolder : MonoBehaviour
{

    public GameObject particleFolder;

    private void Start()
    {
        transform.SetParent(particleFolder.transform);
    }
}
