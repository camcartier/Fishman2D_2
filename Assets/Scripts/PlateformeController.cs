using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformeController : MonoBehaviour
{

    [SerializeField] Transform _startPoint;
    [SerializeField] Transform _endPoint;

    [SerializeField] float _timeToReach;

    private float _timerMovement;

    private bool _isForward = true;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = _startPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isForward) 
        {
            _timerMovement += Time.deltaTime;
            if (_timerMovement >= _timeToReach ) 
            {
                _isForward = false;
            }
        }
        else
        {
            _timerMovement -= Time.deltaTime;
            if (_timerMovement <= 0f)
            {
                _isForward = true;
            }
        }

        Vector3 newPosition = Vector3.Lerp(_startPoint.position, _endPoint.position,_timerMovement/ _timeToReach);

        transform.position = newPosition;
    }
}
