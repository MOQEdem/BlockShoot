using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRound : MonoBehaviour
{
    [SerializeField] private List<Transform> _waypoints;
    [SerializeField] private List<Enemy> _enemies;

    public List<Transform> Waypoints => _waypoints;
    public List<Enemy> Enemies => _enemies;
}
