using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCamera : MonoBehaviour
{   
    [SerializeField] private float _sensitiveX;
    [SerializeField] private float _sensitiveY;
    [SerializeField]private Transform orientation;
    private float xRotation;
    private float yRotation;
    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * _sensitiveX;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * _sensitiveY;

        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);

    }
}
