using UnityEngine;

public class StableCamera : MonoBehaviour
{
    /* this script handles the camera stablisation */

    public GameObject player;
    public float playerX;
    public float playerY;
    public float playerZ;

    void Update()
    {
        playerX = player.transform.eulerAngles.x;
        playerY = player.transform.eulerAngles.y;
        playerZ = player.transform.eulerAngles.z;

        transform.eulerAngles = new Vector3(playerX - playerX, playerY, playerZ - playerZ);
    }
}
