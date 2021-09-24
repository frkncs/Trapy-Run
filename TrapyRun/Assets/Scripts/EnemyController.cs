using States.Enemy;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region Variables

    // Public Variables
    [HideInInspector] public static bool canRun = false;
    /* Bu şekilde canRun adında bir değişken kullanmamın sebebi;
    sağ veya sol yoldan spawn'lanan düşmanlar harekete başlayamıyor.
    default olarak idle'nin içinde kalıyorlar ve arayüz (tutorialUI) 1 kere kapandığı
    için bir daha TutorialFinish Action'ı çalışmıyor. Bu yüzden de düşman harekete geçemiyor.
    bunu static yaptığımız için arayüz 1 kere kapanınca (oyun başlayınca) sonradan spawnlanan
    düşmanlar direkt koşmaya başlıyor. */

    [HideInInspector] public EnemyBaseState currentState;

    [HideInInspector] public EnemyMovement enemyMovement;
    [HideInInspector] public Transform enemyLookCoordTransform;

    // Private Variables
    [SerializeField] private LayerMask groundLayerMask;

    private Animator animator;

    private float minYPos = -5;
    private bool _canCatchPlayer = true;

    #endregion Variables

    private void Start()
    {
        InitializeVariables();
        Stop();
    }

    private void OnEnable()
    {
        SignInEvents();
    }

    private void OnDisable()
    {
        SignOutEvents();
    }

    private void Update()
    {
        currentState.Update(this);
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdate(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    public void Stop()
    {
        currentState = new EnemyIdleState(this);
        enemyMovement.enabled = false;
    }

    public void Run()
    {
        enemyMovement.enabled = true;
        currentState = new EnemyRunState(this);
        enemyMovement.ActivateNavMesh();
    }

    public void CatchPlayer(Collision collision)
    {
        if (!_canCatchPlayer) { return; }

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Catch();
        }
    }

    public bool CheckIfFell()
    {
        return transform.position.y <= minYPos;
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

    private void InitializeVariables()
    {
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    private void SignInEvents()
    {
        Actions.WinEvent += OnWinEvent;
    }

    private void SignOutEvents()
    {
        Actions.WinEvent -= OnWinEvent;
    }

    private void OnWinEvent()
    {
        _canCatchPlayer = false;
    }
}