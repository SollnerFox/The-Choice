
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 12f;
    private CharacterController controller;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * x +    transform.forward * z;
        controller.Move(moveDirection * _speed * Time.deltaTime);
    }
}
