using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private List<GameRound> _rounds;
    [SerializeField] private PlayerMover _player;

    [SerializeField] private int _currentRound;
    [SerializeField] private int _currentEnemy;

    private void OnEnable()
    {
        _player.MovementComplete += OnPlayerMovementComplete;
    }

    private void OnDisable()
    {
        _player.MovementComplete -= OnPlayerMovementComplete;
    }

    private void Start()
    {
        MovePlayer();
    }

    private void OnPlayerMovementComplete()
    {

        foreach (var enemy in _rounds[_currentRound].Enemies)
        {
            enemy.Died += OnEnemyDied;
        }

        MakeEnemyMove(_rounds[_currentRound].Enemies);

        RotatePlayerView();
    }

    private void OnEnemyDied()
    {
        _rounds[_currentRound].Enemies[_currentEnemy].Died -= OnEnemyDied;

        if (_currentEnemy < _rounds[_currentRound].Enemies.Count - 1)
        {
            _currentEnemy++;
            RotatePlayerView();
        }
        else
        {
            if (_currentRound < _rounds.Count - 1)
            {
                _currentEnemy = 0;
                _currentRound++;
                MovePlayer();
            }
            else
            {
                Debug.Log("Pobeda?");
            }
        }
    }

    private void MakeEnemyMove(List<Enemy> enemies)
    {
        foreach (var enemy in enemies)
        {
            enemy.Mover.ActivateMovement();
        }
    }

    private void RotatePlayerView()
    {
        _player.ChangeTrackingEnemy(_rounds[_currentRound].Enemies[_currentEnemy]);
    }

    private void MovePlayer()
    {
        _player.StartMoving(_rounds[_currentRound].Waypoints);
    }
}
