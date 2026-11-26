using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
   public CameraController cam;
    private bool movedUpFromThisTrigger = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.GetComponent<DragAndShoot>()) return;

        // Player climbed past the trigger
        if (!movedUpFromThisTrigger && other.transform.position.y >= transform.position.y)
        {
            cam.MoveUp();
            movedUpFromThisTrigger = true;
        }
        // Player fell back below the trigger
        else if (movedUpFromThisTrigger && other.transform.position.y < transform.position.y)
        {
            cam.MoveDown();
            movedUpFromThisTrigger = false;
        }
    }
}
