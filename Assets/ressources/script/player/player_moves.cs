using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    // Start is called before the first frame update
    Camera cam;
    Vector3 pos;

    [SerializeField]
    float speed;

    [SerializeField]
    float jumpForce;

    Rigidbody rigid;

    void Start()
    {
        cam = gameObject.GetComponentInChildren<Camera>();
        Cursor.visible = false;
        if (Camera.main != null) Camera.main.gameObject.SetActive(false);

        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        UpdateCam();
    }

    void FixedUpdate()
    {
        UpdatePos();
    }

    void UpdatePos()
    {
        float x = Input.GetAxis("Vertical");
        float y = -Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(x, 0, y) * speed;
        Vector3 currentVelocity = rigid.velocity;

        movement = transform.TransformDirection(movement);
        movement.y = currentVelocity.y;

        rigid.velocity = movement;

        if (Physics.Raycast(transform.position, Vector3.down, 1.1f))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }


    void UpdateCam()
    {
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");
        pos = transform.position;
        transform.Rotate(0, h, 0);
        cam.transform.RotateAround(pos, cam.transform.right, -v);
    }
}
