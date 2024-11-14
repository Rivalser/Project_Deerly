using UnityEngine;
using Pathfinding;

public class EnemyAIScript : MonoBehaviour
{

    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    private Path _path;
    private Seeker _seeker;
    private Rigidbody2D _rb;

    private int _currentWaypoint;

    private Animator _animator;
    private EnemyStateController _stateController;


    // Start is called before the first frame update
    void Start()
    {
        _seeker = GetComponent<Seeker>();
        _stateController = GetComponent<EnemyStateController>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _stateController.SetState(EnemyStateController.EnemyState.Idle, _animator);

        InvokeRepeating(nameof(UpdatePath), 0f, .5f);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (_path is null)
        {
            _stateController.SetState(EnemyStateController.EnemyState.Idle, _animator);
            return;
        }

        if (_path.vectorPath.Count == _currentWaypoint)
        {
            _stateController.SetState(EnemyStateController.EnemyState.Idle, _animator);
            return;
        }

        _stateController.SetState(EnemyStateController.EnemyState.Moving, _animator);

        Vector2 direction = ((Vector2)_path.vectorPath[_currentWaypoint] - _rb.position).normalized;
        Vector2 force = direction * (speed * Time.deltaTime);

        _rb.AddForce(force);

        float distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWaypoint]);

        if (distance < nextWaypointDistance) ++_currentWaypoint;
    }


    public EnemyAIScript SetTarget(Transform newTarget)
    {
        target = newTarget;
        UpdatePath();

        return this;
    }


    private void UpdatePath()
    {
        if (!_seeker.IsDone()) return;

        _seeker.StartPath(_rb.position, target.position, _OnPathCalculationComplete);
    }


    private void _OnPathCalculationComplete(Path p)
    {
        if (p.error) return;

        _path = p;
        _currentWaypoint = 0;
    }
}
