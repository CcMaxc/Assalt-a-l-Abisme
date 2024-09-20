using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform playerTransform;
    public float offsetX;
    public float offsetY;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (playerTransform != null)
        {
            float targetX = playerTransform.position.x + offsetX;
            float targetY = playerTransform.position.y + offsetY;

            transform.position = new Vector3(targetX, targetY, -10f);
        }
    }
}
