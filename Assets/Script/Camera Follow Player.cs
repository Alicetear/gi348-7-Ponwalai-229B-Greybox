using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform player;

    [Header("Boundary Settings")]
    public bool useBoundaries = true; 
    public float minX, maxX, minY, maxY; 

    void LateUpdate()
    {
        if (player == null) return;

        float targetX = player.position.x;
        float targetY = player.position.y;

        if (useBoundaries)
        {
            targetX = Mathf.Clamp(targetX, minX, maxX);
            targetY = Mathf.Clamp(targetY, minY, maxY);
        }

        // 3. ???????????????????????????? (Z ???? -10 ????)
        transform.position = new Vector3(targetX, targetY, -10f);
    }
}
