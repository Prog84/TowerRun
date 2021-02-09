using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private PathCreator _pathCreator;
    [SerializeField] private Tower _towerTemplate;
    [SerializeField] private JumpBooster _jumpBoosterTemplate;
    [SerializeField] private Obstacle _obstacleTemplate;
    [SerializeField] private int _humanTowerCount;
    [SerializeField] private int _obstacleCount;
    [SerializeField] private float _boosterOffset;

    private float _roadLength;

    private void Start()
    {
        _roadLength = _pathCreator.path.length;
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        float distanceBetweenTower = _roadLength / _humanTowerCount;
        float distanceTravelled = 0;
        Vector3 spawnPoint;

        for (int i = 0; i < _humanTowerCount; i++)
        {
            distanceTravelled += distanceBetweenTower;
            spawnPoint = _pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
            Instantiate(_towerTemplate, spawnPoint, Quaternion.identity);

            spawnPoint = _pathCreator.path.GetPointAtDistance(distanceTravelled -= _boosterOffset, EndOfPathInstruction.Stop);
            Instantiate(_jumpBoosterTemplate, spawnPoint, Quaternion.identity);
        }

        GenerateObstacles();
    }

    private void GenerateObstacles()
    {
        float distanceBetweenObstacle = _roadLength / _obstacleCount;
        float distanceTravelled = 0;
        Vector3 spawnPoint;

        for (int i = 0; i < _obstacleCount; i++)
        {
            distanceTravelled += distanceBetweenObstacle;
            spawnPoint = _pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
            Instantiate(_obstacleTemplate, spawnPoint, Quaternion.identity);
        }
    }
}
