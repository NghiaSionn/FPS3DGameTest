using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitCamera : MonoBehaviour
{
    public GameObject player;


    private void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, 500, player.transform.position.z);
    }
}
