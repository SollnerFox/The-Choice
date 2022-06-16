
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 12f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _jumpHeight = 3f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private LayerMask _groundMask;
    private Vector3 velocity;
    private bool isGrounded;
    private CharacterController characterController;
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }


    private void Update()
    {
        isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);
        

        if (isGrounded && velocity.y < 0) 
        {
            velocity.y = -2f;
        } 

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * x +    transform.forward * z;
        velocity.y += _gravity * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            characterController.Move(moveDirection * _speed / 5 * Time.deltaTime);
        }
        else 
        { 
            characterController.Move(moveDirection * _speed / 10 * Time.deltaTime); 
        }
        

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity); 
        }

        characterController.Move(velocity * Time.deltaTime);
    }
    private void OnDrawGizmos() 
    {
        Gizmos.DrawSphere(_groundCheck.position, _groundDistance);
    }
}
