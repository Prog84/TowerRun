using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce;

    private bool _isGrouned;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();    
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _isGrouned)
        {
            _isGrouned = false;
            _rigidbody.AddForce(Vector3.up * _jumpForce);
        }        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Road road))
        {
            _isGrouned = true;
        }
    }
}
