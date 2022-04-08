using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    [SerializeField] float _speed = 3f;
    private SpawnManager _SpawnManager;
    
    [SerializeField] GameObject ExplosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        _SpawnManager =GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        AstroidRotation();
    }

    void AstroidRotation()
    {
        transform.Rotate(Vector3.forward * _speed * Time.deltaTime);
    }

    void Explosion()
    {
        
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Laser")
        {
            Explosion();
            Destroy(collision.gameObject);
            _SpawnManager.SpawnRoutine();

            Destroy(this.gameObject, 0.5f);

        }
        
    }
}
