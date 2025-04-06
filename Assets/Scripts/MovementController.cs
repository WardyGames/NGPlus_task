using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class MovementController : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    [SerializeField] private float _speedSmoothTime;
    CharacterController _controller;
    Animator _animator;
    Transform _mainCameraTransform;

    private float _velocityY;
    private float _speedSmoothVelocity;
    private float _currentSpeed;
    
    
    static readonly int SpeedHash = Animator.StringToHash("Speed");
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _mainCameraTransform = Camera.main.transform;
    }
    
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        
        Vector3 forward = _mainCameraTransform.forward;
        Vector3 right = _mainCameraTransform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();
        
        Vector3 direction = (forward * input.y + right * input.x).normalized;

        if (direction != Vector3.zero)
        {
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * _speedSmoothTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
        }
        
        float targetSpeed = _speed * input.magnitude;
        _currentSpeed = Mathf.SmoothDamp(_currentSpeed, targetSpeed, ref _speedSmoothVelocity, _speedSmoothTime);
        
        _controller.Move(direction * _currentSpeed * Time.deltaTime);
        _animator.SetFloat(SpeedHash, 0.5f * input.magnitude, _speedSmoothTime, Time.deltaTime);
    }
}
