using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseEnemy : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float turnSpeed = 30f;
    [HideInInspector] public float originalMoveSpeed;
    [HideInInspector] public float originalTurnSpeed;

    [Header("Detect Player")]
    public Transform detectPlayerArea;
    public float detectPlayerRadius;
    public LayerMask whatIsAPlayer;
    public bool hasDetectedPlayer;
    bool isChasing;

    [Header("Detected Player")]
    public Transform detectedPlayer;
    float chaseTime;
    public float setChaseTime;

    [Header("Return to Original Position")]
    public Transform originalPosition;

    [Header("Patrol")]
    public PatrolPath patrolPath;
    [Range(0.1f, 1f)]
    public float arriveDistance = 1f;
    public float waitTime = 0.5f;
    [SerializeField] private bool isWaiting = false;
    [SerializeField] Vector2 currentPatrolTarget = Vector2.zero;
    bool isInitialised = false;
    public EnemyMover enemyMover;

    [Header("Combat Scene")]
    public string combatScene;
    public Canvas explorationCanvas;
    public bool hasLoaded = true;
    public static BaseEnemy instance;

    int currentIndex = -1;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            //the SceneManager loads new Scene as an extra Scene (overlapping the other). This is Additive mode
            SceneManager.LoadSceneAsync(combatScene, LoadSceneMode.Additive);
            LevelManager.Instance.StopMusic();            
            explorationCanvas.enabled = false;
            moveSpeed = 0;
            turnSpeed = 0;
            LevelManager.Instance.DefeatedEnemy(this);
            hasLoaded = true;
            LevelManager.Instance.hasPlayed = false;
            LevelManager.Instance.hasLoaded = true;
        }
    }
    //detects player
    protected void DetectPlayer()
    {
        if (chaseTime > 0)
        {
            isChasing = true;
        }
        //when chase time is zero, if player aint within guy detection area,
        //player is no longer detected and chaseTime is reset
        if (chaseTime <= 0)
        {
            detectedPlayer = null;
            chaseTime = setChaseTime;
            isChasing = false;
            return;
        }
        //if the detection area does detect player, sets the transform variable of the detect player
        if (hasDetectedPlayer)
        {
            PlayerMovement player = FindObjectOfType<PlayerMovement>();
            detectedPlayer = player.transform;
        }
        //if detection area does detect player OR (chaseTime>0 AND detectedPlayer is NOT null),
        //chaseTime will decrease
        if (hasDetectedPlayer || isChasing || (chaseTime > 0 && detectedPlayer != null))
        {
            FollowPlayer();
            chaseTime -= Time.deltaTime;
        }
    }
    protected void FollowPlayer()
    {
        //sets the direction the enemy should face towards i.e. towards the player
        Vector2 direction = (detectedPlayer.transform.position - transform.position).normalized;
        //sets the angle of the rotation for the player to face to
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //makes the guy move towards player
        transform.position = Vector2.MoveTowards(this.transform.position, detectedPlayer.transform.position,
            turnSpeed * Time.deltaTime);
        //makes guy rotate to face player
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }
    void Awake()
    {
        instance = this;
        originalMoveSpeed = moveSpeed;
        originalTurnSpeed = turnSpeed;
        if (enemyMover == null) enemyMover = GetComponentInChildren<EnemyMover>();
        if (patrolPath == null) patrolPath = GetComponentInChildren<PatrolPath>();
    }
    protected void Patrol()
    {
        if (!isWaiting)
        {
            if (patrolPath.Length < 2) return;
            if (!isInitialised)
            {
                //gets the current path point via the enemy's position
                var currentPathPoint = patrolPath.GetClosestPathPoint(transform.position);
                //gets the current Index and Position from the above
                this.currentIndex = currentPathPoint.Index;
                this.currentPatrolTarget = currentPathPoint.Position;
                isInitialised = true;
            }
            //checks if the distance between the enemy and the current point is less than the arrival dist
            //i.e. checks to see if the enemy has arrived at the point
            if (Vector2.Distance(transform.position, currentPatrolTarget) < arriveDistance)
            {
                isWaiting = true;
                StartCoroutine(WaitCoroutine());
                return;
            }
            //direction that the enemy moves towards
            Vector2 directionToGo = currentPatrolTarget - (Vector2)enemyMover.transform.position;
            //gets the dot product of these two vectors
            //dot product is a float value equal to magnitudes of 2 vectors x together then x by cos(angle betw them)
            //for normalised vectors, dot returnbs 1 if both point in same direction\
            //-1 if point in completely diff directions
            // and zero if they r perpendicular
            var dotProduct = Vector2.Dot(enemyMover.transform.up, directionToGo.normalized);
            if (dotProduct < 0.98f)
            {
                //helps decide if the enemy rotates left or right
                var crossProduct = Vector3.Cross(enemyMover.transform.up, directionToGo.normalized);
                int rotationResult = crossProduct.z >= 0 ? -1 : 1;
                HandlingPatrollingMovement(new Vector2(rotationResult, 1));
            }
            else
            {
                HandlingPatrollingMovement(Vector2.up);
            }
        }
        //waits a while before setting the struct to the next patrol point
        IEnumerator WaitCoroutine()
        {
            yield return new WaitForSeconds(waitTime);
            var nextPathPoint = patrolPath.GetNextPathPoint(currentIndex);
            currentPatrolTarget = nextPathPoint.Position;
            currentIndex = nextPathPoint.Index;
            isWaiting = false;
        }
    }
    public void HandlingPatrollingMovement(Vector2 movementVector)
    {
        enemyMover.Move(movementVector);
    }
    // Start is called before the first frame update
    void Start()
    {
        //sets the original position of this dude to where it was
        originalPosition = gameObject.transform;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(detectPlayerArea.position, detectPlayerRadius);
    }
    // Update is called once per frame
    void Update()
    {
        //sets the detection area
        hasDetectedPlayer = Physics2D.OverlapCircle(detectPlayerArea.position, detectPlayerRadius, whatIsAPlayer);
        if (hasDetectedPlayer || isChasing)
        {
            DetectPlayer();
        }
        if (!hasDetectedPlayer)
        {
            Patrol();
        }
    }
}
