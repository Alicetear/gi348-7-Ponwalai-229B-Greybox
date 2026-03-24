using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform player;

    void LateUpdate()
    {
        if (player == null) return;

        transform.position = new Vector3(
            player.position.x,
            player.position.y,
            -10f
        );
    }
}
