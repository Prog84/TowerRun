using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce;

    private float _jumpBoostForce;
    private float _startJumpBoost = 1;
    private bool _isGrouned;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _jumpBoostForce = _startJumpBoost;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _isGrouned)
        {
            _isGrouned = false;
            _rigidbody.AddForce(Vector3.up * _jumpForce * _jumpBoostForce);
        }        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Road road))
        {
            _isGrouned = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out JumpBooster jumpBooster))
        {
            _jumpBoostForce = jumpBooster.JumpBoost;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out JumpBooster jumpBooster))
        {
            _jumpBoostForce = _startJumpBoost;
        }
    }
}
