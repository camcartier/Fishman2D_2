using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BeerCollected : MonoBehaviour
{

    [SerializeField] private IntVariables _beerCollected;
    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _beerCollected.n_value++;
            Destroy(gameObject);
            
        }
    }

    private void Awake()
    {
        _beerCollected.n_value= 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
