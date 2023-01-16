using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPplateformController : MonoBehaviour
{

    [SerializeField] private Transform[] _waypoint;

    [SerializeField] private float _speed;

    [SerializeField] private float _distTolerance;

    private int _targetWayPoint;


    // Start is called before the first frame update
    void Start()
    {
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
            
            _targetWayPoint++;
            if (_targetWayPoint >= _waypoint.Length)
            {
                _targetWayPoint = 0;
            }
        }
    }
}
