using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Entity
{
    public Vector2 speedRange;
    private float speed = 5f;

    private float angularVelocity;

    Material mat;
	private Object explosionRes;
    


	// Start is called before the first frame update
	void Start()
    {
        speed = Random.Range(speedRange.x, speedRange.y);

        mat = GetComponent<SpriteRenderer>().material;
        explosionRes = Resources.Load("AsteroidExplosion");

        angularVelocity = Random.Range(-90f, 90f);
    }

    // Update is called once per frame
    void Update() {
        Vector3 pos = transform.position;
        pos.x -= speed * Time.deltaTime;
        transform.position = pos;

        transform.Rotate(Vector3.forward * angularVelocity * Time.deltaTime);

        //if (!GetComponent<Renderer>().isVisible) {
        if (transform.position.x < -20f) {
			Destroy(gameObject);
		}
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Bullet")) {
            TakeDamage(collision.gameObject.GetComponent<Bullet>().damage);
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(int damage) {
        hp -= damage;

        if (hp <= 0) {
            Kill();
        } else {
            CancelInvoke("Flash");
            mat.SetColor("_Intensity", new Color(0.75f, 0.75f, 0.75f));
            Invoke("Flash", 0.1f);
        }
    }

    private void Flash() {
        mat.SetColor("_Intensity", new Color(0f, 0f, 0f));
    }
    public void Kill() {
        FindObjectOfType<GameManager>().AddScore(1);
        Destroy(gameObject);
        GameObject explosion = (GameObject)Instantiate(explosionRes);
        explosion.transform.position = gameObject.transform.position;
    }
}
