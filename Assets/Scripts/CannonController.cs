using UnityEngine;

public class CannonController : MonoBehaviour
{
    [Header("Cannon")]
    [SerializeField] private Transform _cannonTransform;

    [Header("Cannonball Settings")]
    [SerializeField] private Transform _cannonballSpawnPoint;
    [SerializeField] private Explosion _cannonballPrefab;
    [SerializeField] private float _shootForce;
    private float _cannonballDelayCurrent;
    private float _cannonballDelay = 5f;    

    [Header("Mouse Settings")]
    [SerializeField] private float _mouseSensitivity;
    private float _yRotation = 0f;

    [Header("Moving Settings")]
    [SerializeField] private float _movingForce;
    private float _damping = 0.05f;
    private float _rotationSpeed = 1f;

    private Rigidbody _rigidbody;

    private void Awake() => _rigidbody = GetComponent<Rigidbody>();

    private void Start() => Cursor.lockState = CursorLockMode.Locked;
   
    private void Update()
    {
        Shoot();
        UpdateTimer();
    }

    private void FixedUpdate()
    {
        ApplyMovingForce();
        ApplyRotationX();
        ApplyRotationY();
    }

    private void ApplyMovingForce()
    {
        Vector3 zAxisForce = transform.up * Input.GetAxis("Vertical");

        if (zAxisForce.magnitude > 0)
        {
            zAxisForce *= _movingForce;
            _rigidbody.AddForce(zAxisForce);
        }
        else
        {
            Vector3 dampedVelocity = _rigidbody.velocity * _damping;

            dampedVelocity.y = _rigidbody.velocity.y;
            _rigidbody.velocity = dampedVelocity;
        }
    }
    private void ApplyRotationX()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.rotation *= Quaternion.AngleAxis(horizontalInput * _rotationSpeed, Vector3.forward);
    }

    private void ApplyRotationY()
    {
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        _yRotation += mouseY;
        _yRotation = Mathf.Clamp(_yRotation, 0f, 45f);

        _cannonTransform.localRotation = Quaternion.Euler(_yRotation, 0f, 0f);
    }

    private void Shoot()
    {       
        if (Input.GetButtonDown("Fire1"))
        {
            if (_cannonballDelayCurrent <= 0)
            {
                _cannonballDelayCurrent = _cannonballDelay;

                var cannonball = Instantiate(_cannonballPrefab, _cannonballSpawnPoint.position, _cannonballSpawnPoint.rotation);
                cannonball.GetComponent<Rigidbody>().AddForce(_cannonballSpawnPoint.up * _shootForce);                

                Destroy(cannonball, 10);

                DestroyComponentWaste();
            }
        }
    }

    private void DestroyComponentWaste()
    {        
        ComponentRemoving[] _componentRemoving = FindObjectsOfType<ComponentRemoving>();

        foreach(var component in _componentRemoving)
        {
            component.DestroyComponent();
        }       
    }

    private void UpdateTimer()
    {
        if (_cannonballDelayCurrent > 0)
        {
            _cannonballDelayCurrent -= Time.deltaTime;
        }
    }
}
