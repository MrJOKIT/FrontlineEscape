using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayCam : MonoBehaviour
{
    public Transform fakeCamera;

    private void Update()
    {
        transform.position = fakeCamera.transform.position;
        transform.rotation = fakeCamera.transform.rotation;
    }
}
