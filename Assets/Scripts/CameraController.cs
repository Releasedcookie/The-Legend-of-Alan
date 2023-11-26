using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private bool followPlayer = true;

    // Update is called once per frame
    private void Update()
    {
        if (followPlayer)
        {
            transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        }
    }
    public void followPlayerFunction()
    {
        followPlayer = true;
    }
    public void stopFollowingPlayer()
    {
        followPlayer = false;
    }
}
