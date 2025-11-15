using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    private Vector3 velocity = Vector3.zero;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void LateUpdate()
    {
        Vector3 targetPos = new Vector3(player.transform.position.x, player.transform.position.y + 6, player.transform.position.z - 7);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 0.125f);
    }
}
