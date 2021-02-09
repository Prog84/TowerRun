using UnityEngine;

public class JumpBooster : MonoBehaviour
{
    [SerializeField] private float _jumpBoost;

    public float JumpBoost => _jumpBoost;
}
