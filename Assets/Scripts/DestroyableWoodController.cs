using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableWoodController : MonoBehaviour
{

    [SerializeField] GameObject _wood;
    [SerializeField] GameObject _woodState0;
    [SerializeField] GameObject _woodState1;
    [SerializeField] GameObject _woodState2;

    private int _woodHealth = 4;

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Weapon"))
        {
            _woodHealth--;

        }
    }
}
