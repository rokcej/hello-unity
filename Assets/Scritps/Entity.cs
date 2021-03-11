using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int hp = 100;

 /*   // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Bullet")) {
            hp -= collision.gameObject.GetComponent<Bullet>().damage;
            Destroy(collision.gameObject);

            if (hp <= 0) {
                Destroy(gameObject);
            }

        }
    }*/
}
