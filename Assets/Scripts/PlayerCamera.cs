using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCamera : MonoBehaviour
{   
    [SerializeField] private float _mouseSensitive = 300f;
    [SerializeField]private Transform _playerBody;
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
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * _mouseSensitive;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * _mouseSensitive;

        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        _playerBody.rotation = Quaternion.Euler(0f, yRotation, 0f);

    }

    public void Sensitive(float newSensitive)
    {
        _mouseSensitive = newSensitive;
    }

}
