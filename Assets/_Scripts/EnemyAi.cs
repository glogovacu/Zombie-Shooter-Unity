using UnityEngine;
using UnityEngine.AI;
public class EnemyAi : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private LayerMask _whatIsGround, _whatIsPlayer;

    [SerializeField] private float _walkPointRange;

    [SerializeField] private float _timeBetweenAttacks;
    
    [SerializeField] private float _sightRange, _attackRange;

    private bool _playerInSightRange, _playerInAttackRange;

    private Transform _playerTransform;
    private Vector3 _walkPoint;
    private bool _walkPointSet;
    private bool _alreadyAttacked;

    private void Start() {
        _playerTransform = PlayerSingleton.Instance.PlayerTransform;
    }
    private void Update() {
        _playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, _whatIsPlayer);
        _playerInAttackRange = Physics.CheckSphere(transform.position, _attackRange, _whatIsPlayer);
        if (!_playerInSightRange && !_playerInAttackRange) Patrolling();
        //ako je jedno u sight onda krece da ga juri
        if (_playerInSightRange && !_playerInAttackRange) ChasePlayer();
        //ako je oba onda ga udara
        if (_playerInSightRange && _playerInAttackRange) AttackPlayer();
    }
    private void Patrolling() {
        if (!_walkPointSet) { 
            SearchWalkPoint();
        }
        if (_walkPointSet) {
            agent.SetDestination(_walkPoint);
        }
        Vector3 distanceTowalkPoint = transform.position - _walkPoint;
        if (distanceTowalkPoint.magnitude < 1f) {
            _walkPointSet= false;
        }

    }
    private void SearchWalkPoint() {
        //stavlja random mesto na mapi
        float randomZ = Random.Range(-_walkPointRange, _walkPointRange);
        float randomX = Random.Range(-_walkPointRange, _walkPointRange);
        _walkPoint = new Vector3(transform.position.x + randomX,transform.position.y, transform.position.z + randomZ);
        if(Physics.Raycast(_walkPoint, -transform.up, 2f,_whatIsGround)) {
            _walkPointSet = true;
        }
    }
    private void ChasePlayer() {
        agent.SetDestination(_playerTransform.position);
    }
    private void AttackPlayer() {
        agent.SetDestination(transform.position);
        transform.LookAt(_playerTransform);
        //stavljamo cooldown na napade
        if (!_alreadyAttacked) {
            _alreadyAttacked = true;
            Invoke(nameof(ResetAttack), _timeBetweenAttacks);
        }
    }
    private void ResetAttack() {
        //resetuje attack
        _alreadyAttacked= false;
    }

}
