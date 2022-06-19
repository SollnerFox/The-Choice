using System.Collections;
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

    public AudioSource stepAudio;
    public AudioClip[] groundClip;
    public float stepTimer = 0.7f;
    private float stepTimerDown;
    private RaycastHit hit;
    Vector3 lastPoint;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        lastPoint = transform.position;
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
            characterController.Move(moveDirection * _speed / 6.5f * Time.deltaTime);
        }
        else 
        { 
            characterController.Move(moveDirection * _speed / 10f * Time.deltaTime);
            //stepAudio.Play();
        }
        

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity); 
        }

        characterController.Move(velocity * Time.deltaTime);
        
        if (lastPoint != transform.position)
        {
            lastPoint = transform.position;
            Sound();
        }
        else
        {
            stepAudio.Stop();
        }
    }

    
    private void OnDrawGizmos() 
    {
        Gizmos.DrawSphere(_groundCheck.position, _groundDistance);
    }

    

    void Sound()
    {
        if (stepTimerDown > 0)
        { stepTimerDown -= Time.deltaTime; }
        if (stepTimerDown < 0)
        { stepTimerDown = 0; }
        if (stepTimerDown == 0)
        {
            if (Physics.Raycast(transform.position, -Vector3.up, out hit, 10))
            {
                if (hit.transform.tag == "Ground")
                {
                    stepAudio.PlayOneShot(groundClip[Random.Range(0, groundClip.Length)], 0.2f);
                }

            }
            stepTimerDown = stepTimer;
        }
    }
}
