using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public float speed = 2f;
    public Vector3 direction = Vector3.forward;
    public float distance = 2f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Mișcă obstacolul înainte și înapoi
        transform.position = startPosition + direction * Mathf.Sin(Time.time * speed) * distance;
    }
}