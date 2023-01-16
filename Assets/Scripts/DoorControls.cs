using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControls : MonoBehaviour
{
    private GameManager _gameManager;
    private PlayerControls _player;

    private CapsuleCollider2D _doorCollider;

    [SerializeField]
    private SpriteRenderer _doorClosed;
    [SerializeField]
    private SpriteRenderer _doorOpened;

    //private Vector2 _distanceToDoor;

    private int _testbeersCollected;
    private int _beersTotalInLevel;



    private void Awake()
    {
        //_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //_player = GameObject.Find("Player").GetComponent<PlayerControls>();

        _doorOpened.enabled = false;
        _doorClosed.enabled = true;
        
        _doorCollider= GetComponentInChildren<CapsuleCollider2D>();

        _beersTotalInLevel = GameObject.FindGameObjectsWithTag("Beer").Length;
        Debug.Log($"nb biere lvl {_beersTotalInLevel}");

        

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.collider.CompareTag("Player"))
        {
            //Debug.Log("bim");
            _doorOpened.enabled = true;
            _doorClosed.enabled = false;
            _doorCollider.enabled = false;
        }
    }




}
