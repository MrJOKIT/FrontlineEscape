using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSound : MonoBehaviour
{
    public void StartDestroy()
    {
        StartCoroutine(DestoryTime());
    }
    IEnumerator DestoryTime()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        yield return null;
    }
}
