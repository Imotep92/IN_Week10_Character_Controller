using UnityEngine;
using UnityEngine.AI;

public class NPCMovementScript : MonoBehaviour
{
    NavMeshAgent _agent;
    WaypointManager _wm;
    Transform player;
    Vector3 currentDestination;
    Animator anim;
    [SerializeField] float followDistance = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _wm = FindFirstObjectByType<WaypointManager>();

        currentDestination = _wm.waypoints[Random.Range(0, _wm.waypoints.Length)].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) < followDistance)
        {
            if (Vector3.Distance(player.position, transform.position) < 2)
            {
                Attack();
            }
            else
            {
                Follow();
            }     
        }
        else
        {
            Search();
        }
    }

    void Follow()
    {
        _agent.destination = player.position;
        anim.SetTrigger("Following");
    }

    void Attack()
    {
        _agent.SetDestination(transform.position);
        anim.SetTrigger("Near_Player");
    }

    void Search()
    {
        anim.SetTrigger("Searching");
        if (Vector3.Distance(currentDestination, transform.position) < 5)
        {
            currentDestination = _wm.waypoints[Random.Range(0, _wm.waypoints.Length)].position;
            //currentDestination = transform.position + (new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)));
        }
        _agent.destination = currentDestination;
    }
}
