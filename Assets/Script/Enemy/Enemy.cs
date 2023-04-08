using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float combatStop;
    private StateMachine _stateMachine;
    private NavMeshAgent _agent;
    private Animator _animator;
    private SoundManager _soundManager;

    public NavMeshAgent Agent { get => _agent; }

    [SerializeField] private string currentState;
    public Path path;

    public bool foundPlayer;
    [Header("Combat")] 
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private GameObject bulletObject;
    [SerializeField] private float bulletSpeed;
    private float time;
    private float timeDelay = 0.5f;
    private PlayerHealth playerHealth;

    private void Start()
    {
        _stateMachine = GetComponent<StateMachine>();
        _agent = GetComponent<NavMeshAgent>();
        _stateMachine.Initialise();
        _animator = GetComponent<Animator>();
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        playerHealth = target.gameObject.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (foundPlayer)
        {
            var position = target.transform.position;
            _agent.SetDestination(position);
            transform.LookAt(position);
            bulletSpawn.LookAt(position);
            float dirToPlayer = Vector3.Distance(position, transform.position);
            if (dirToPlayer <= combatStop)
            {
                _agent.stoppingDistance = combatStop;
                InCombat();
                Debug.Log("Am stop and shoot you");
            }
            else if (dirToPlayer > combatStop)
            {
                
                _agent.stoppingDistance = 1f;
                _animator.SetBool("Idle",false);
                _animator.SetBool("Walk",true);
                _animator.SetBool("Shooting",false);
                Debug.Log("Here we go agian");
            }
            
            
        }
        
    }

    public void InCombat()
    {
        _stateMachine.enabled = false;
        _animator.SetBool("Idle",false);
        _animator.SetBool("Walk",false);
        _animator.SetBool("Shooting",true);

        time += Time.deltaTime;
        if (time >= timeDelay)
        {
            SoundManager.instace.Play(SoundManager.SoundName.EnemyShoot);
            var bullet = Instantiate(bulletObject, bulletSpawn.transform.position,bulletSpawn.transform.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawn.forward * bulletSpeed;
            time = 0f;
        }
        

    }

    
    

    
}
