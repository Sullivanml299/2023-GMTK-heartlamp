using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCamera : MonoBehaviour
{
    public float angleSpeed = 0.02f;
    public float ySpeed = 0.03f;
    public float scrollSpeed = 5f;
    public bool lockMouse = true;
    public float radius = 10.0f;
    float angle = Mathf.PI;
    float height = 2.0f;

    Transform target;
    Vector3 offset;
    float defaultScrollSpeed;

    // Start is called before the first frame update
    void Start()
    {
        defaultScrollSpeed = scrollSpeed;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        offset = new Vector3(0, 2, -radius);
        if (lockMouse) Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset;
        transform.rotation = Quaternion.LookRotation(target.position - transform.position);

        MoveOneCylinder();
        Zoom();

    }

    void MoveOneCylinder()
    {
        var mouseXDelta = Input.GetAxis("Mouse X");
        var mouseYDelta = Input.GetAxis("Mouse Y");

        angle += mouseXDelta * angleSpeed;

        var x = Mathf.Sin(angle) * radius;
        var z = Mathf.Cos(angle) * radius;

        var vSpeed = ySpeed * defaultScrollSpeed / scrollSpeed;
        height = Mathf.Max(height - mouseYDelta * vSpeed, 0.1f);

        offset = new Vector3(x, height, z);
    }

    void Zoom()
    {
        var scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        radius = Mathf.Max(radius - scrollDelta * scrollSpeed, 0.1f);
    }
}
