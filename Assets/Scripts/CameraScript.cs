using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScriptFollow : MonoBehaviour
{
    [SerializeField]
    GameObject toFollow;
    Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        Vector3 targetPos = new Vector3(toFollow.transform.position.x, toFollow.transform.position.y + 7.5f, toFollow.transform.position.z - 11);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 0.125f);
    }
}
