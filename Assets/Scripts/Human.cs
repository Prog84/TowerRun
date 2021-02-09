using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Human : MonoBehaviour
{
    [SerializeField] private Transform _fixationPoint;

    public Transform FixationPoint => _fixationPoint;

    private Animator _animator;
    private string _isRunning = "isRunning";

    private void Awake()
    {
        _animator = GetComponent<Animator>();   
    }

    public void Bounce(float force)
    {
        if (TryGetComponent(out Rigidbody rigidbody))
        {
            transform.parent = null;
            rigidbody.isKinematic = false;
            rigidbody.AddForce(new Vector3(Random.Range(-force, force), Random.Range(-force, force), Random.Range(-force, force)), ForceMode.Force);
        }
    }

    public void Run()
    {
        _animator.SetBool(_isRunning, true);
    }

    public void StopRun()
    {
        _animator.SetBool(_isRunning, false);
    }
}
