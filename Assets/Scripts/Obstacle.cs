using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int _countHumanRemove;
    [SerializeField] private int _bounceForce;

    public float CountHumanRemove => _countHumanRemove;
    
    public float BounceForce => _bounceForce;
}
