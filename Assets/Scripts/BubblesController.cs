using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblesController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Vector2 _direction;

    [SerializeField] float _moveSpeed;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this);
    }
}
