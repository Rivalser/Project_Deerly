using System.Collections;
using UnityEngine;


namespace EntityControlInterface
{
    [RequireComponent (typeof (Animator))]
    [RequireComponent (typeof (SpriteRenderer))]
    public class EnemyEntityImpl : EntityBase<EnemyEntityStates>
    {
        private static readonly int IsIdleAnimParam = Animator.StringToHash ("isIdle");
        private static readonly int IsAttackingAnimParam = Animator.StringToHash ("isAttacking");
        private static readonly int IsChasingAnimParam = Animator.StringToHash ("isChasing");

        public float speed = 2f;

        public float fovRange = 10f;
        public float attackRange = 1.5f;
        public LayerMask targetLayerMask;
        public Transform currentTarget;
        public float attackCooldownTimer;

        public Transform[] waypoints;
        private readonly float _waitTimeAtWaypoint = 1f;

        private Animator _animator;


        private EnemyEntityStates _currentState = EnemyEntityStates.Idle;
        private int _currentWaypointIndex;
        private bool _isOnCooldown;
        private bool _isWaitingAtWaypoint;
        private SpriteRenderer _spriteRenderer;
        private EnemyStateMachineImpl<EnemyEntityStates> _stateMachine;
        private float _waitCounter;


        private void Awake ()
        {
            _animator = GetComponent<Animator> ();
            _spriteRenderer = GetComponent<SpriteRenderer> ();
            _stateMachine ??= new EnemyStateMachineImpl<EnemyEntityStates> ();
        }

        private void Start ()
        {
            if (waypoints is { Length: > 0 })
                TransitionTo (EnemyEntityStates.Patrolling);
            else
                TransitionTo (EnemyEntityStates.Idle);
        }

        public void Update ()
        {
            DetectTarget ();
            UpdateEntity ();
            ExecuteCurrentStateLogic ();
        }

        // ---- gizmos
        private void OnDrawGizmosSelected ()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere (transform.position, fovRange);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere (transform.position, attackRange);

            if (currentTarget)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine (transform.position, currentTarget.position);
            }

            if (waypoints is not { Length: > 0 }) return;
            Gizmos.color = Color.cyan;

            for (var i = 0; i < waypoints.Length; i++)
            {
                if (!waypoints[i]) continue;
                var waypointPos = waypoints[i].position;
                Gizmos.DrawSphere (new Vector3 (waypointPos.x, waypointPos.y, transform.position.z), 0.3f);

                if (i < waypoints.Length - 1)
                {
                    if (waypoints[i + 1])
                        Gizmos.DrawLine (new Vector3 (waypointPos.x, waypointPos.y, transform.position.z)
                            , new Vector3 (waypoints[i + 1].position.x, waypoints[i + 1].position.y
                                , transform.position.z));
                }
                else
                {
                    if (waypoints[0])
                        Gizmos.DrawLine (new Vector3 (waypointPos.x, waypointPos.y, transform.position.z)
                            , new Vector3 (waypoints[0].position.x, waypoints[0].position.y, transform.position.z));
                }
            }
        }

        public override void UpdateEntity ()
        {
            _stateMachine?.UpdateEntity (this);
        }

        public override void TransitionTo (EnemyEntityStates newState)
        {
            if (_currentState == newState && Application.isPlaying) return;

            // TODO: DEBUG
            Debug.Log ($"{gameObject.name}: Transitioning from {_currentState} to {newState}");
            _currentState = newState;
            _waitCounter = 0f;
            _isWaitingAtWaypoint = false;

            _animator.SetBool (IsIdleAnimParam, newState == EnemyEntityStates.Idle);
            _animator.SetBool (IsChasingAnimParam, newState == EnemyEntityStates.Patrolling);
            _animator.SetBool (IsChasingAnimParam, newState == EnemyEntityStates.Chasing);
            _animator.SetBool (IsAttackingAnimParam, newState == EnemyEntityStates.Attacking);

            switch (newState)
            {
                case EnemyEntityStates.Idle:
                    break;
                case EnemyEntityStates.Patrolling:
                    if (waypoints == null || waypoints.Length == 0)
                        Debug.LogWarning ($"{gameObject.name} entered Patrolling state with no waypoints.");
                    break;
                case EnemyEntityStates.Chasing:
                    break;
                case EnemyEntityStates.Attacking:
                    if (currentTarget)
                        FlipSpriteTowards (currentTarget.position);
                    break;
            }
        }

        public override float GetScoreForState (EnemyEntityStates potentialState)
        {
            var targetInFOV =
                currentTarget && Vector2.Distance (transform.position, currentTarget.position) <= fovRange;

            var targetInAttackRange = currentTarget &&
                                      Vector2.Distance (transform.position, currentTarget.position) <= attackRange;

            switch (potentialState)
            {
                case EnemyEntityStates.Idle:
                    if (!targetInFOV && (waypoints == null || waypoints.Length == 0)) return 10f;
                    return 0f;

                case EnemyEntityStates.Patrolling:
                    if (!targetInFOV && waypoints is { Length: > 0 }) return 50f;
                    return 5f;

                case EnemyEntityStates.Chasing:
                    if (targetInFOV && !targetInAttackRange) return 80f;
                    return 0f;

                case EnemyEntityStates.Attacking:
                    return targetInAttackRange ? 100f : 0f;

                default:
                    return 0f;
            }
        }

        public override EnemyEntityStates GetCurrentState ()
        {
            return _currentState;
        }

        internal override void ExecuteCurrentStateLogic ()
        {
            switch (_currentState)
            {
                case EnemyEntityStates.Idle:
                    HandleIdleState ();
                    break;
                case EnemyEntityStates.Patrolling:
                    HandlePatrollingState2D ();
                    break;
                case EnemyEntityStates.Chasing:
                    HandleChasingState2D ();
                    break;
                case EnemyEntityStates.Attacking:
                    HandleAttackingState2D ();
                    break;
            }
        }

        private void DetectTarget ()
        {
            var colliders = Physics2D.OverlapCircleAll (transform.position, fovRange, targetLayerMask);

            Transform closestTarget = null;
            var minDistance = float.MaxValue;

            foreach (var localCollider in colliders)
            {
                var distanceToTarget = Vector2.Distance (transform.position, localCollider.transform.position);

                if (!(distanceToTarget < minDistance)) continue;
                minDistance = distanceToTarget;
                closestTarget = localCollider.transform;
            }

            currentTarget = closestTarget;
        }

        private void HandleIdleState ()
        {
            // TODO: DEBUG
            Debug.Log ($"{gameObject.name} is Idle.");
        }

        private void HandlePatrollingState2D ()
        {
            if (waypoints == null || waypoints.Length == 0)
            {
                if (_currentState == EnemyEntityStates.Patrolling) _stateMachine.UpdateEntity (this);
                return;
            }

            if (_isWaitingAtWaypoint)
            {
                _waitCounter += Time.deltaTime;
                if (_waitCounter >= _waitTimeAtWaypoint) _isWaitingAtWaypoint = false;
            }
            else
            {
                var currentWaypointTransform = waypoints[_currentWaypointIndex];

                if (currentWaypointTransform == null)
                {
                    _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
                    return;
                }

                var targetPosition = (Vector2)currentWaypointTransform.position;

                if (Vector2.Distance (transform.position, targetPosition) < 0.2f)
                {
                    transform.position = new Vector3 (targetPosition.x, targetPosition.y, transform.position.z);
                    _waitCounter = 0f;
                    _isWaitingAtWaypoint = true;
                    _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
                }
                else
                {
                    MoveTowardsAndFlipSprite (targetPosition);
                }
            }
        }

        private void HandleChasingState2D ()
        {
            if (!currentTarget)
            {
                if (_currentState == EnemyEntityStates.Chasing) _stateMachine.UpdateEntity (this);
                return;
            }

            var targetPosition = (Vector2)currentTarget.position;

            if (Vector2.Distance (transform.position, targetPosition) > attackRange)
                MoveTowardsAndFlipSprite (targetPosition);
        }

        private IEnumerator CooldownCoroutine ()
        {
            _isOnCooldown = true;
            yield return new WaitForSeconds (attackCooldownTimer);
            _isOnCooldown = false;
        }


        private void HandleAttackingState2D ()
        {
            if (!currentTarget)
            {
                if (_currentState == EnemyEntityStates.Attacking) _stateMachine.UpdateEntity (this);
                return;
            }

            if (_isOnCooldown)
            {
                Debug.Log ($"{gameObject.name} is on cooldown.");
                return;
            }

            FlipSpriteTowards (currentTarget.position);

            // TODO: Attack logic
            Debug.Log ($"{gameObject.name} is Attacking {currentTarget.name}.");

            var combatScript = GetComponent<Enemy_Combat> ();
            combatScript.Attack ();
            StartCoroutine (CooldownCoroutine ());
        }

        private void MoveTowardsAndFlipSprite (Vector2 targetPosition)
        {
            var currentPosition = (Vector2)transform.position;
            var direction = (targetPosition - currentPosition).normalized;

            var newPosition = Vector2.MoveTowards (currentPosition, targetPosition, speed * Time.deltaTime);
            transform.position = new Vector3 (newPosition.x, newPosition.y, transform.position.z);

            if (direction.x > 0.01f)
                _spriteRenderer.flipX = true;
            else if (direction.x < -0.01f) _spriteRenderer.flipX = false;
        }

        private void FlipSpriteTowards (Vector2 targetWorldPosition)
        {
            if (targetWorldPosition.x > transform.position.x + 0.01f)
                _spriteRenderer.flipX = true;
            else if (targetWorldPosition.x < transform.position.x - 0.01f) _spriteRenderer.flipX = false;
        }
    }
}