using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;


enum WaypointMode
{
    LOOP,
    PINGPONG
}

public class WPpingpongController : MonoBehaviour
{

    [SerializeField] private Transform[] _waypoint;

    [SerializeField] private float _speed;

    [SerializeField] private float _distTolerance;

    private int _targetWayPoint;
    private bool _isForward = true;

    [SerializeField] private WaypointMode _mode = WaypointMode.LOOP;


    // Start is called before the first frame update
    void Start()
    {
        /* ce block sert a partir d'un position aleatoire
        int startWayPoint = Random.Range(0, _waypoint.Length - 1);
        transform.position = _waypoint[startWayPoint].position;
        _targetWayPoint = startWayPoint + 1;
        */

        transform.position = _waypoint[0].position;

        _targetWayPoint = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = Vector3.MoveTowards(transform.position, _waypoint[_targetWayPoint].position, _speed * Time.deltaTime);

        transform.position = newPosition;


        if (Vector3.Distance(transform.position, _waypoint[_targetWayPoint].position) <= _distTolerance)
        {
            switch (_mode)
            {
                case WaypointMode.LOOP:
                    Loop();
                break;
                case WaypointMode.PINGPONG:
                    PingPong();
                 break;

            }
        }
    }


    void Loop()
    {
        _targetWayPoint++;

        if (_targetWayPoint >= _waypoint.Length )
        {
            _targetWayPoint = 0;
        }
    }


    void PingPong()
    {
        if (_isForward)
        {
            _targetWayPoint++;
            if (_targetWayPoint >= _waypoint.Length - 1)
            {
                _isForward = false;
            }
        }
        else
        {
            _targetWayPoint--;
            if (_targetWayPoint <= 0)
            {
                _isForward = true;
            }

        }
    }
}
