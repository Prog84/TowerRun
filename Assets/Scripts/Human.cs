using UnityEngine;

public class Human : MonoBehaviour
{
    [SerializeField] private Transform _fixationPoint;

    public Transform FixationPoint => _fixationPoint;
}
