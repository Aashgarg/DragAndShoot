using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DragAndShoot : MonoBehaviour
{

    [SerializeField] private float power;
    [SerializeField] private float maxDragDistance;
    [SerializeField] private int trajectoryResolution;
    [SerializeField] private float beltIgnoreTime = 0.15f; // tune this

    private Rigidbody2D rb;
    private Camera cam;
    private Vector3 startPoint;
    private LineRenderer lineRend;
    private Vector3 currentDragVector;
    private bool isDragging = false;

    private Collider2D myCollider;
    private Collider2D currentBelt;       // belt we're touching
    private bool launchQueued;
    private Vector2 queuedVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        cam = Camera.main;
        lineRend = GetComponent<LineRenderer>();
        lineRend.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = GetMouseWorldPosition();
            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos);
            if (hit != null && hit.gameObject == gameObject)
            {
                isDragging = true;
                startPoint = mouseWorldPos;
            }
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector3 currentPoint = GetMouseWorldPosition();
            currentDragVector = Vector3.ClampMagnitude(startPoint - currentPoint, maxDragDistance);
            ShowTrajectory((Vector2)currentDragVector * power);
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            queuedVelocity = (Vector2)currentDragVector * power;
            launchQueued = true;   // apply in FixedUpdate

            lineRend.enabled = false;
            isDragging = false;
        }
    }

    void FixedUpdate()
    {
        if (!launchQueued) return;
        launchQueued = false;

        if (currentBelt != null)
            StartCoroutine(IgnoreBelt(currentBelt));

        rb.linearVelocity = queuedVelocity; // rb.velocity on older Unity
    }

    IEnumerator IgnoreBelt(Collider2D belt)
    {
        Physics2D.IgnoreCollision(myCollider, belt, true);
        yield return new WaitForSeconds(beltIgnoreTime);
        if (belt != null)
            Physics2D.IgnoreCollision(myCollider, belt, false);
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.collider.GetComponent<SurfaceEffector2D>() != null)
            currentBelt = col.collider;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider == currentBelt)
            currentBelt = null;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.x = Mathf.Clamp(mousePos.x, 0f, Screen.width);
        mousePos.y = Mathf.Clamp(mousePos.y, 0f, Screen.height);
        mousePos.z = Mathf.Abs(cam.transform.position.z);
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
        worldPos.z = 0f;
        return worldPos;
    }

    void ShowTrajectory(Vector2 initialVelocity)
    {
        lineRend.enabled = true;
        lineRend.positionCount = trajectoryResolution;

        Vector3[] points = new Vector3[trajectoryResolution];
        Vector2 startPos = transform.position;
        Vector2 gravity = Physics2D.gravity * rb.gravityScale;

        for (int i = 0; i < trajectoryResolution; i++)
        {
            float t = i * Time.fixedDeltaTime;
            points[i] = startPos + initialVelocity * t + 0.5f * gravity * t * t;
        }
        lineRend.SetPositions(points);
    }

}
