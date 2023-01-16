using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{


    #region Variables

    #region Serialized
    [Header ("Character Movement")]
    [SerializeField] float _moveSpeed;
    [SerializeField] float _jumpForce;

    [SerializeField] float _runMultiplicator;
    [SerializeField] float _hurtMultiplicator;

    [SerializeField] int _maxNumberOfJumps;

    [SerializeField] Animator _animator;

    [Header ("GroundChecker")]
    [SerializeField] float _radius;
    [SerializeField] float _offsetYGroundChecker;

    [SerializeField] LayerMask _layer;
    #endregion

    [Header ("Health")]
    [SerializeField] int _playerMaxHP = 10;
    [SerializeField] int _playerHP;

    [Header ("HealthBar")]
    [SerializeField] Image _healthbarGreen;
    [SerializeField] Image _healthbarRed;
    private HealthBar _healthbar;               


    private Rigidbody2D _rigidbody2D;
    private Vector2 _direction;

    #region etat du player
    bool _isHurt;
    bool _isDead;

    public int _deathCounter;

    //evidemment la faudrait plutot utiliser la n variable qu'on a crée
    //mais j'ai po reussi
    //ça m'echappe un peu pour l'instant
    //du coup c'est tout sale
    //bouuuh
    public int _beersCollected;
    #endregion

    #region deplacements
    bool _isRunning;
    int _runButtonPushCount;

    bool _isCrouching;
    bool _isPushing;

    bool _isJumping;
    int _numberOfJumps = 0;

    [SerializeField] GameObject _spawnPoint;
    #endregion

    private BoxCollider2D _playerCollider;
    float _colliderSizeXInit;
    float _colliderSizeYInit;
    float _colliderOffsetXInit;
    float _colliderOffsetYInit;


    #endregion

    private void Awake()
    {
        _playerHP = _playerMaxHP;

        _rigidbody2D = GetComponent<Rigidbody2D>();

        _playerCollider= GetComponent<BoxCollider2D>();

        _colliderSizeXInit = _playerCollider.size.x;
        _colliderSizeYInit = _playerCollider.size.y;
        _colliderOffsetXInit = _playerCollider.offset.x;
        _colliderOffsetYInit= _playerCollider.offset.y;

        _healthbar = GameObject.Find("HealthBar").GetComponent<HealthBar>();

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        GroundChecker();
        

        //recuperation des boutons pour le deplacement
        _direction.x = Input.GetAxisRaw("Horizontal") * _moveSpeed;

        _animator.SetFloat("MoveSpeedX", Mathf.Abs(_direction.x));
        _animator.SetFloat("MoveSpeedY", _direction.y);



        //recuperation des boutons pour le saut
        if (Input.GetButtonDown("Jump"))
        {
            _isJumping = true;

        }


        //on recupere la touche pour courrir
        //je le met dans l'update parce que je dois garder la touche enfoncee
        if (Input.GetButton("Run"))
        {
            //Debug.Log("running");

            _animator.SetBool("IsRunning", true);
            _isRunning = true;

        }
        else
        {
            _animator.SetBool("IsRunning", false);
            _isRunning = false;
        }


        //Crouch
        if (Input.GetButton("Crouch"))
        {
          //Debug.Log("crouching");
            _animator.SetBool("IsCrouching", true);
            _playerCollider.size = new Vector2(3.0f, 1.5f);
            _playerCollider.offset = new Vector2(_colliderOffsetXInit, 0.7f);


        }
        else
        {
            _animator.SetBool("IsCrouching", false);
            _playerCollider.size = new Vector2(_colliderSizeXInit, _colliderSizeYInit);
            _playerCollider.offset = new Vector2(_colliderOffsetXInit, _colliderOffsetYInit);
        }

        //Push
        if (Input.GetButton("Push"))
        {
            _animator.SetBool("IsPushing", true);
        }
        else
        {
            _animator.SetBool("IsPushing", false);
        }

        if (_playerHP <= 0)
        {
            
            Death();
        }

    }


    private void FixedUpdate()
    {
        //appliquer une gravite permanente
        _direction.y = _rigidbody2D.velocity.y;


        //application de la force pour le deplacement
        if(_isRunning)
        {
            Vector2 runVector = new Vector2(_direction.x * _runMultiplicator, _direction.y);
            _rigidbody2D.velocity = runVector;

        }
        else
        {
            _rigidbody2D.velocity = _direction;
        }
        
        //application de la force pour le saut
        if (_isJumping && _numberOfJumps<_maxNumberOfJumps && _rigidbody2D.velocity.y < 0.6f )
        {
            _numberOfJumps++;
            _isJumping= false;
          //Debug.Log("jumping");

            
            Vector2 jumpVector = new Vector2(_direction.x, _jumpForce);
            //ici si on utilise le addforce, on va sauter de plus en plus haut
            //je vais m'en servir pour les impacts sur twinshooter peut etre
            //_rigidbody2D.AddForce(jumpVector);
            

            
            _direction.y = _jumpForce;
            _rigidbody2D.velocity = _direction * Time.fixedDeltaTime;  
            
        }


        //tourner le personnage dans la bonne direction
        if(_direction.x <0f)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        else if(_direction.x > 0f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        //deuxieme methode
        /*
         if (_direction.x < 0f || _direction.x >0f)
        {
            transform.right = _direction;
        }
        */


        if (_isHurt)
        {
            //Vector2 hurtWalkVector = new Vector2(_direction.x * _hurtMultiplicator, _direction.y);
            _animator.SetBool("IsHurt", true);
        }
        if (_isPushing)
        {
            _animator.SetBool("IsPushing", true);
        }

        if (_isDead)
        {
            _animator.SetBool("IsDead", true);
        }


        //gere la vitesse de chute
        if (_rigidbody2D.velocity.y < 0)
        {
            _rigidbody2D.gravityScale = 2.5f;
        }
        else
        {
            _rigidbody2D.gravityScale = 1.5f;
        }

    }


    //on peut remplacer cette methode par le floor collider
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            _animator.SetTrigger("Grounded");
            _numberOfJumps = 0;
        }
    }*/


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Vector3 positionTransformOffset = new Vector3(transform.position.x, transform.position.y - _offsetYGroundChecker, transform.position.z);

        Gizmos.DrawWireSphere(positionTransformOffset, _radius);
    }


    private void TakeDamage(int damage)
    {
        _playerHP = _playerHP - damage;

    }

    private void HealHP(int HP)
    {
        _playerHP = _playerHP + HP;
    }


    void GroundChecker()
    {
        //GroundChecker

        Vector3 positionTransformOffset = new Vector3(transform.position.x, transform.position.y - _offsetYGroundChecker, transform.position.z);

        Collider2D floorCollider = Physics2D.OverlapCircle(positionTransformOffset, _radius, _layer);

        if (floorCollider != null)
        {
         //Debug.Log("isfloor");
            _animator.SetTrigger("Grounded");
            _numberOfJumps = 0;



            if (floorCollider.CompareTag("PlateForme"))
            {

                transform.SetParent(floorCollider.transform); 
            }
            else
            {
                transform.SetParent(null);
            }

        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Pikes"))
        {
            TakeDamage(2);

            _healthbar.SetHealth(_playerHP);

            Vector2 jumpVector = new Vector2(_direction.x, _jumpForce);
            _direction.y = _jumpForce;
            _rigidbody2D.velocity = _direction * Time.fixedDeltaTime;

            Debug.Log("aie");

            //_animator.SetBool("IsTakingPikeDamage", true);
        }

        if (collision.collider.CompareTag("Boss") || collision.collider.CompareTag("Bulle"))
        {
            TakeDamage(1);

            _healthbar.SetHealth(_playerHP);
        }

        if (collision.collider.CompareTag("Beer"))
        {
            Debug.Log("beer!");
            _beersCollected++;
        }

        if (collision.collider.CompareTag("Meat"))
        {
            HealHP(1);

            _healthbar.SetHealth(_playerHP);
        }

        if (collision.collider.CompareTag("FallStop"))
        {
            Death();
        }
    }

    private void Death()
    {
        _deathCounter++;
        transform.position = _spawnPoint.transform.position;
        _playerHP = _playerMaxHP;
        _healthbar.SetHealth(_playerHP);
    }

}
