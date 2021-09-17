using System;
using Assets.Scripts;
using System.Collections;
using States.Enemy;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    #region Variables

    [SerializeField] private LayerMask groundLayerMask;
    
    // Public Variables
    [HideInInspector] public EnemyBaseState currentState;
    [HideInInspector] public EnemyMovement enemyMovement;
    
    // Private Variables
    private Animator animator;
    private float minYPos = -5;
    private bool _canCatchPlayer = true;

    #endregion Variables

    private void Start()
    {
        InitializeVariables();
        Stop();
        
        SignInEvents();
    }

    private void SignInEvents()
    {
        Actions.TutorialFinishEvent += Run;
        Actions.WinEvent += OnWinEvent;
    }
    
    private void SignOutEvents()
    {
        Actions.TutorialFinishEvent -= Run;
        Actions.WinEvent -= OnWinEvent;
    }

    private void OnWinEvent()
    {
        _canCatchPlayer = false;
    }
    
    public void Stop()
    {
        currentState = new EnemyIdleState(this);
        enemyMovement.enabled = false;
    }

    public void Run()
    {
        currentState = new EnemyRunState(this);
        enemyMovement.enabled = true;
        enemyMovement.ActivateNavMesh();
    }
    
    private void Update()
    {
        currentState.Update(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    public void CatchPlayer(Collision collision)
    {
        if(!_canCatchPlayer) {return;}
        
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Catch();
        }
    }

    private void OnDisable()
    {
        SignOutEvents();
    }

    public bool CheckIfFell()
    {
        return transform.position.y <= minYPos;
    }

    private void InitializeVariables()
    {
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdate(this);
    }
    
    public bool CheckIfFloating()
    {
        return !Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, 2, groundLayerMask);
    }

    public void PlayIdleAnim()
    {
        animator.Play("Idle");
    }
    
    public void PlayRunAnim()
    {
        animator.Play("Running");
    }
    
    public void PlayFallAnim()
    {
        animator.Play("Falling");
    }
}