using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target;
    public float speed = 5;
    public Vector3 offset;
    public static SmoothCameraFollow instance;

    private void Awake()
    {
        instance = this;
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }

    void FixedUpdate()
    {
        if (!target || GameManager.instance.state != GameState.Playing) return;

        Vector3 newPosition = target.position + offset;
        transform.position = Vector3.Slerp(transform.position, newPosition, Time.deltaTime * speed);
    }
}