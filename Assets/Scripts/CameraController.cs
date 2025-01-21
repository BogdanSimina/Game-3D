using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    private Vector3 offset;
    public GameObject Player_user;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position;
    }
    private void LateUpdate()
    {
        transform.position = Player_user.transform.position + offset;
    }
}
