using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//Cela enemy ai logika
public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    //ovo je maska bitna sta je igrac sta je ground da bi ai funkcionisao
    public LayerMask whatIsGround, whatIsPlayer;
    public AudioClip[] patrolSources;
    public AudioClip[] attackSources;
    public AudioClip[] chaseSources;
    public AudioSource audioSource;

    //Patrola
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Napad
    public float timeBetweenAttacks;
    public float timeBetweenSound = 1f;
    bool alreadyAttacked;
    bool alreadySound;

    //Status
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public HealthBar healthBar;
    private Animator mAnimator;
    private void Awake()
    {
        // inicjalizacija svega player je dinamicki jer nije povezan sa zombijem direkto i healthbar
        mAnimator = GetComponent<Animator>();
        player = GameObject.Find("PlayerCapsule").transform;
        GameObject gBplayer = GameObject.Find("PlayerCapsule").gameObject;
        healthBar = gBplayer.GetComponent<HealthBar>();
        agent = GetComponent<NavMeshAgent>();
    
    }
    private void Update()
    {
        //Zombiji su vremenom sve brzi i brzi
        agent.speed = agent.speed + (Time.deltaTime*Time.deltaTime);
        //pita da li je player u blizini
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        //pita da li je player u napadnoj blizini
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        // ako nije ni jedno oda patrolira
        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        //ako je jedno u sight onda krece da ga juri
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        //ako je oba onda ga udara
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }
    //logika za patrolu 
    private void Patrolling()
    {
        //ako nema walkpoint
        if (!walkPointSet)
        { 
            //trazi je
            SearchWalkPoint();
        }
        //ako ima
        if (walkPointSet)
        {
            //ide na random mesto

            agent.SetDestination(walkPoint);
        }
        //ovo da bi resetovo patrolu tj kad stigne do mesta krece opet da trazi novo mesto
        Vector3 distanceTowalkPoint = transform.position - walkPoint;
        //ovo je animacija patrole
        mAnimator.SetTrigger("Patrol");
        //logika za sound da ne bi se spamovo
        if (!alreadySound)
        {
            //vise mogucih soundova i bira random
            int randomIndex = Random.Range(0, patrolSources.Length);
            AudioClip randomClip = patrolSources[randomIndex];
            audioSource.clip = randomClip;
            audioSource.Play();
            alreadySound = true;
            Invoke(nameof(ResetSound), timeBetweenSound);
        }
        // stavja walkpoint novi
        if (distanceTowalkPoint.magnitude < 1f)
        {
            walkPointSet= false;
        }

    }
    private void SearchWalkPoint()
    {
        //stavlja random mesto na mapi
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX,transform.position.y, transform.position.z + randomZ);
        if(Physics.Raycast(walkPoint, -transform.up, 2f,whatIsGround))
        {
            walkPointSet = true;
        }
    }
    private void ChasePlayer()
    {
        if (!alreadySound)
        {
            int randomIndex = Random.Range(0, chaseSources.Length);
            AudioClip randomClip = chaseSources[randomIndex];
            audioSource.clip = randomClip;
            audioSource.Play();
            alreadySound = true;
            Invoke(nameof(ResetSound), timeBetweenSound);
        }
        // animacija i juri playera
        mAnimator.SetTrigger("Chase");
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        //stavljamo cooldown na napade
        if (!alreadyAttacked){
            //ovde pisemo kod sta se desi kad nas napadne
            mAnimator.SetTrigger("Attack");
            //smanjuje se hp
            healthBar.DecreaseHealth(20f);
            int randomIndex = Random.Range(0, attackSources.Length);
            AudioClip randomClip = attackSources[randomIndex];
            audioSource.clip = randomClip;
            audioSource.Play();
            //
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        //resetuje attack
        alreadyAttacked= false;
    }
    private void ResetSound()
    {
        //resetuje sound
        alreadySound = false;
    }

}
