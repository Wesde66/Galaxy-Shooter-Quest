using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _speed = 1;
    [SerializeField] float _Radius;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _ExplosionClip;

    private Player _player;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if(_audioSource == null)
        {
            Debug.Log("No audio source attached to enemy");
        }
        else
        {
            _audioSource.clip = _ExplosionClip;
        }

        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.Log("Animator is null");
        }
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.Log("Player Enemy script is null");
        }

    }

    // Update is called once per frame
    void Update()
    {
        

        
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y  <= -4)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX,11,0);
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       

        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            anim.SetTrigger("onEnemyDeath");
            _speed = 0;
            _audioSource.Play();

            Destroy(this.gameObject, 2.8f);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            
            if(_player != null)
            {
                _player.addScore(20);
            }
            anim.SetTrigger("onEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(gameObject, 2.8f);
        }
       
    }
}
