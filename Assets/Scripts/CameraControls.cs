using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraControls : MonoBehaviour
{

    [SerializeField] Transform _target;

    [SerializeField] [Range (0,1)] float _lerpTime = 0.01f;

    Vector3 _velocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
        _velocity = Vector3.zero;

        Vector3 targetPosition_zoffset = new Vector3(_target.position.x, _target.position.y, -10f);

        Vector3 newPositon = Vector3.SmoothDamp(transform.position, targetPosition_zoffset, ref _velocity, _lerpTime*Time.fixedDeltaTime);

        transform.position = newPositon;


        /*
        Vector3 targetPosition_zoffset = new Vector3(_target.position.x, _target.position.y, -10f);


        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition_zoffset, _lerpTime*Time.fixedDeltaTime);

        transform.position = newPosition;*/
    }



    private void LateUpdate()
    {


        
    }
}
