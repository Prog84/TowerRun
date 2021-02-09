using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTower : MonoBehaviour
{
    [SerializeField] private Human _startHuman; 
    [SerializeField] private Transform _distanceChecker;
    [SerializeField] private float _fixationMaxDistance;
    [SerializeField] private BoxCollider _checkCollider;

    private List<Human> _humans;

    public event UnityAction<int> HumanAdded;

    private void Start()
    {
        _humans = new List<Human>();
        Vector3 spawnPoint = transform.position;
        _humans.Add(Instantiate(_startHuman, spawnPoint, Quaternion.identity, transform));
        _humans[0].Run();
        HumanAdded?.Invoke(_humans.Count);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Human human))
        {
            Tower collisionTower = human.GetComponentInParent<Tower>();

            if (collisionTower != null)
            {
                List<Human> collectedHumans = collisionTower.CollectHuman(_distanceChecker, _fixationMaxDistance);

                if (collectedHumans != null)
                {
                    _humans[0].StopRun();

                    for (int i = collectedHumans.Count - 1; i >= 0; i--)
                    {
                        Human insertHuman = collectedHumans[i];
                        InsertHuman(insertHuman);
                        DisplaceCheckers(insertHuman, true);
                    }

                    _humans[0].Run();
                    if (_humans.Count > 1)
                    {
                        _humans[Random.Range(1, _humans.Count)].Texting();
                    }
                    HumanAdded?.Invoke(_humans.Count);
                }

                collisionTower.Break();
            }
        }

        if (other.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            _humans[0].StopRun();

            if (_humans.Count > obstacle.CountHumanRemove)
            {
                for (int i = 0; i < obstacle.CountHumanRemove; i++)
                {
                    _humans[0].gameObject.AddComponent<Rigidbody>();
                    _humans[0].Bounce(obstacle.BounceForce);
                    _humans.Remove(_humans[0]);
                    DisplaceCheckers(_humans[0], false);
                }
            }
            else
            {
                Time.timeScale = 0;
                Debug.Log("Вы проиграли");
            }

            _humans[0].Run();
        }
    }

    private void InsertHuman(Human collectedHuman)
    {
        _humans.Insert(0, collectedHuman);
        SetHumanPosition(collectedHuman);
    }

    private void SetHumanPosition(Human human)
    {
        human.transform.parent = transform;
        human.transform.localPosition = new Vector3(0, human.transform.localPosition.y, 0);
        human.transform.localRotation = Quaternion.identity;
    }

    private void DisplaceCheckers(Human human, bool isAddHuman)
    {
        float displaceScale = 1.5f;
        Vector3 distanceCheckerNewPosition = _distanceChecker.position;
        if (isAddHuman)
        {
            distanceCheckerNewPosition.y -= human.transform.localScale.y * displaceScale;
        }
        else
        {
            distanceCheckerNewPosition.y += human.transform.localScale.y * displaceScale;
        }
        _distanceChecker.position = distanceCheckerNewPosition;
        _checkCollider.center = _distanceChecker.localPosition;
    }
}
