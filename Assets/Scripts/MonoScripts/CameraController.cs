using UnityEngine;

public class CameraController : MonoBehaviour
{
   public float screenHeight = 10f; // Match your camera's orthographic size * 2
    public float moveSpeed = 15f;

    private float targetY;

    void Start()
    {
        targetY = transform.position.y;
    }

    void LateUpdate()
    {
        Vector3 currentPos = transform.position;
        float newY = Mathf.MoveTowards(currentPos.y, targetY, moveSpeed * Time.deltaTime);
        transform.position = new Vector3(currentPos.x, newY, currentPos.z);
    }

    public void MoveUp()
    {
        targetY += screenHeight;
    }

    public void MoveDown()
    {
        targetY -= screenHeight;
    }
}