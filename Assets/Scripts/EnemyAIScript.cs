using UnityEngine;
using Pathfinding;

public class EnemyAIScript : MonoBehaviour
{

    public GameObject target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    Path _path;
    int _currentWaypoint;
    bool _reachedEndOfPath = false;

    Seeker _seeker;
    Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        var targetPosition = target.GetComponent<Transform>().position;
        _seeker = GetComponent<Seeker>();
        _rb = GetComponent<Rigidbody2D>();

        _seeker.StartPath(_rb.position, targetPosition, OnPathComplete);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnPathComplete(Path p)
    {
        if (p.error) return;
        _path = p;
        _currentWaypoint = 0;
    }
}
