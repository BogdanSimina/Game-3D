using UnityEngine;

public class MovingObstacle1 : MonoBehaviour
{
    public float speed = 2f; // Viteza mișcării
    public Vector3 direction1 = Vector3.left;  // Direcția inițială (ex: stânga-dreapta)
    public Vector3 direction2 = Vector3.forward; // A doua direcție (ex: înainte-înapoi)
    public float distance1 = 2f; // Distanța de mișcare pe direcția 1
    public float distance2 = 2f; // Distanța de mișcare pe direcția 2

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; // Reține poziția inițială
    }

    void Update()
    {
        // Mișcă obstacolul în stânga-dreapta și înainte-înapoi
        float offset1 = Mathf.Sin(Time.time * speed) * distance1;
        float offset2 = Mathf.Cos(Time.time * speed) * distance2;

        // Actualizează poziția
        transform.position = startPosition + direction1 * offset1 + direction2 * offset2;
    }
}