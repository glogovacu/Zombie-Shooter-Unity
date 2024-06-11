using System;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAi : MonoBehaviour {

    public EventHandler OnAttack;
    public EventHandler OnChase;
    public EventHandler OnPatroll;

    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private LayerMask _whatIsGround, _whatIsPlayer;

    [SerializeField] private float _walkPointRange;

    [SerializeField] private float _timeBetweenAttacks;
    
    [SerializeField] private float _sightRange, _attackRange;

    private bool _playerInSightRange, _playerInAttackRange;

    private Transform _playerTransform;
    private Vector3 _walkPoint;
    private float _rotationSpeed = 5f;
    private bool _walkPointSet;
    private bool _alreadyAttacked;
    

    private void Start() {
        _playerTransform = PlayerSingleton.Instance.PlayerTransform;
    }
    private void Update() {
        _playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, _whatIsPlayer);
        _playerInAttackRange = Physics.CheckSphere(transform.position, _attackRange, _whatIsPlayer);
        if (!_playerInSightRange && !_playerInAttackRange) Patrolling();
        if (_playerInSightRange && !_playerInAttackRange) ChasePlayer();
        if (_playerInSightRange && _playerInAttackRange) AttackPlayer();
    }
    private void Patrolling() {
        if (!_walkPointSet) { 
            SearchWalkPoint();
        }
        if (_walkPointSet) {
            SetDestination(_walkPoint);
        }
        Vector3 distanceTowalkPoint = transform.position - _walkPoint;
        if (distanceTowalkPoint.magnitude < 1f) {
            _walkPointSet= false;
        }

    }
    private void SearchWalkPoint() {
        float randomZ = UnityEngine.Random.Range(-_walkPointRange, _walkPointRange);
        float randomX = UnityEngine.Random.Range(-_walkPointRange, _walkPointRange);
        _walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if(Physics.Raycast(_walkPoint, -transform.up, 2f,_whatIsGround)) {
            _walkPointSet = true;
        }
    }
    private void ChasePlayer() {
        SetDestination(_playerTransform.position);
        OnChase?.Invoke(this, EventArgs.Empty);
    }
    private void AttackPlayer() {
        agent.SetDestination(transform.position);
        RotateTowardsLocation(_playerTransform.position);
        if (!_alreadyAttacked) {
            OnAttack?.Invoke(this, EventArgs.Empty);
            _alreadyAttacked = true;
            Invoke(nameof(ResetAttack), _timeBetweenAttacks);
        }
    }
    private void ResetAttack() {
        _alreadyAttacked= false;
    }

    private void SetDestination(Vector3 position) {
        agent.SetDestination(position);
        RotateTowardsLocation(position);
    }

    private void RotateTowardsLocation(Vector3 position) {
        // Determine the direction to the player
        Vector3 direction = position - transform.position;
        // Calculate the rotation needed to look at the player
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        // Smoothly rotate towards the player
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

}
