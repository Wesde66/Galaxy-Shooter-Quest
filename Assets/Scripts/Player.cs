using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject _Laser;
    [SerializeField] GameObject _TrippleShot;
    [SerializeField] GameObject _ShieldVisualizer;
    [SerializeField] GameObject _LeftEngineDamage;
    [SerializeField] GameObject _RightEngineDamage;
    [SerializeField] AudioSource LaserAudio;
    [SerializeField] AudioClip LasersoundAudioclip;

    [SerializeField] float _fireRate;
    [SerializeField] float speed = 3.5f;
    [SerializeField] private bool _isSpeedBoostActive = false;
    [SerializeField] private bool _isTrippleShotActive = false;
    [SerializeField] private bool _isShieldsUp = false;
    [SerializeField] private int _Lives = 3;
    [SerializeField] private int _Score;

    [SerializeField] Animator anim;
    private UIManager _UIManager;
    float _canFire = -1f;
    private SpawnManager _spawnManager;
    private float _SpeedBoost = 2f;
    private GameManager _gameManager;
    



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.Log("Gamemanager to load new level is null");
        }
       
        transform.position = new Vector3(0, 0, 0);
        LaserAudio = GetComponent<AudioSource>();
        if(LaserAudio == null)
        {
            Debug.Log("Audio source player null");
        }
        else
        {
            LaserAudio.clip = LasersoundAudioclip;
        }
        
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.Log("Spawn Managerr is null");
        }
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_UIManager == null)
        {
            Debug.Log("UI manager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        TurningAnimation();
        CalculateMovment();

#if UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("Fire") && Time.time > _canFire)
        {
            Shoot();
        }

#else

        

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            Shoot();
        }
#endif
    }

    void CalculateMovment()
    {
#if UNITY_ANDROID

        float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        float vertical = CrossPlatformInputManager.GetAxis("Vertical");

#else

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

#endif

        if (_isSpeedBoostActive == false)
        {
            transform.Translate(new Vector3(horizontal, vertical, 0) * speed * Time.deltaTime);

        }
        else if (_isSpeedBoostActive == true)
        {
            transform.Translate(new Vector3(horizontal, vertical, 0) *speed* _SpeedBoost * Time.deltaTime);
        }



        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 0, 3.5f));

       

        if (transform.position.x >= 11)
        {
            transform.position = new Vector3(-11, transform.position.y, 0);
        }
        else if (transform.position.x <= -11)
        {
            transform.position = new Vector3(11, transform.position.y, 0);
        }
    }

    void Shoot()
    {
        _canFire = Time.time + _fireRate;

        if (_isTrippleShotActive == true)
        {
            Instantiate(_TrippleShot, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_Laser, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);

        }

        LaserAudio.Play();

    }

    public void Damage()
    {
       if (_isShieldsUp == false)
        {
            _Lives--;
            _UIManager.UpdateLives(_Lives);
        }
       else if(_isShieldsUp == true)
        {
            _ShieldVisualizer.SetActive(false);
            _isShieldsUp = false;
            return;
        }


       if (_Lives == 2)
        {
            _LeftEngineDamage.gameObject.SetActive(true);
        }

       if(_Lives == 1)
        {
            _RightEngineDamage.gameObject.SetActive(true);
        }


        if (_Lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            

            Destroy(this.gameObject);
        }
    }

    public void TripleShotActivate()
    {
        _isTrippleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());

    }

    IEnumerator TripleShotPowerDownRoutine()
    {

        yield return new WaitForSeconds(3f);

        _isTrippleShotActive = false;
    }

    public void SpeedBoost()
    {
        _isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostActiveCooldown());

    }

    IEnumerator SpeedBoostActiveCooldown()
    {
        yield return new WaitForSeconds(3f);

        _isSpeedBoostActive = false;
    }

    public void ShieldActive()
    {
        _ShieldVisualizer.SetActive(true);
        _isShieldsUp = true;
    }

    public void addScore(int points)
    {
        _Score += points;
        _UIManager.UpdateScore(_Score);
        

    }

    void TurningAnimation()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal < 0)
        {
            anim.SetBool("Left", true);
        }
        else if(horizontal == 0)
        {
            anim.SetBool("Left", false);
        }


        if (horizontal > 0)
        {
            anim.SetBool("Right", true);
        }
        else if (horizontal == 0)
        {
            anim.SetBool("Right", false);
        }
    }

    



}
