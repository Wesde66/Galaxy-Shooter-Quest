using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAstroid : MonoBehaviour
{
    [SerializeField] float _speedDown;
    [SerializeField] GameObject _SmallAstroid;
    private Player _Player;
    // Start is called before the first frame update
    void Start()
    {
        _Player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speedDown * Time.deltaTime);
        if(transform.position.y <= -4)
        {
            float Randonx = Random.Range(-8f, 8f);
            transform.position = new Vector3(Randonx, 11.5f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(other != null)
            {
               _Player.Damage();
               
                Instantiate(_SmallAstroid, transform.position, Quaternion.identity);
                
                Destroy(this.gameObject, 0.2f);
            }
        }

        if (other.tag == "Laser")
        {
            Instantiate(_SmallAstroid, transform.position, Quaternion.identity);

            Destroy(other.gameObject);

            Destroy(this.gameObject, 0.2f);
        }

       
    }
}
