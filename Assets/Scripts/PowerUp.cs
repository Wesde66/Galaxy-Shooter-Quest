using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] AudioClip _AudioClip;
    
    
    //ID for power ups
    //0 = Triple shot
    // 1 = Speed
    // 2 = Shields

    [SerializeField] int _PowerUpID;

    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y <= -2f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {

            AudioSource.PlayClipAtPoint(_AudioClip, transform.position, 100);

            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                switch (_PowerUpID)
                {
                    case 0:
                        player.TripleShotActivate();
                        break;
                    case 1:
                        player.SpeedBoost();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                    default:
                        Debug.Log("wrong number power up");
                        break;

                }

            }

            Destroy(this.gameObject);
        }
    }
}
