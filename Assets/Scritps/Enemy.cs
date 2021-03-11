using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public Vector2 speedRange;
    private float speed = 5f;
    public Vector2 amplitudeRange;
    private float amplitude;
    public Vector2 frequencyRange;
    private float frequency;
    public GameObject explosion;
    public GameObject bullet;
    float yStart;
    float xStart;
    float timeStart;

    public float shotDelay = 2f;
    public float shotErrorAngle = 5f;
    private float lastShot = 0f;

    private int pattern;

    Material mat;

    // Start is called before the first frame update
    void Start()
    {
        frequency = Random.Range(frequencyRange.x, frequencyRange.y);
        amplitude = Random.Range(amplitudeRange.x, amplitudeRange.y);
        speed = Random.Range(speedRange.x, speedRange.y);
        mat = GetComponent<SpriteRenderer>().material;
        yStart = transform.position.y;
        xStart = transform.position.x;
        timeStart = Time.time;
        pattern = Random.Range(0, 4);
    }

    // Update is called once per frame
    void Update()
    {
        if (pattern <= 2) {
            Vector3 pos = transform.position;
            pos.x -= speed * Time.deltaTime;
            pos.y = yStart + Mathf.Sin(Time.time * frequency) * amplitude;
            transform.position = pos;
        } else {
            Vector3 pos = transform.position;
            pos.x = xStart - 0.2f * speed * (Time.time - timeStart) + Mathf.Sin(Time.time * frequency) * amplitude;
            pos.y = yStart + Mathf.Cos(Time.time * frequency) * amplitude;
            transform.position = pos;
        }

        if (transform.position.x < -20f) {
            Destroy(gameObject);
        }

        Shoot();
    }

    void Shoot() {
        float t = Time.time;
        if (t - lastShot >= shotDelay) {
            PlayerController player = FindObjectOfType<PlayerController>();
            if (player != null) {
                Vector2 dir = new Vector2(
                    player.transform.position.x - transform.position.x,
                    player.transform.position.y - transform.position.y);
                dir.Normalize();

                float error = Random.Range(-shotErrorAngle, shotErrorAngle);
                float cos = Mathf.Cos(shotErrorAngle * Mathf.Deg2Rad);
                float sin = Mathf.Sin(shotErrorAngle * Mathf.Deg2Rad);
                dir = new Vector2(cos * dir.x - sin * dir.y, sin * dir.x + cos * dir.y);

                GameObject bulletObj = Instantiate(bullet, transform.position, Quaternion.identity);
                bulletObj.GetComponent<Bullet>().dir = dir;

                lastShot = t;
            }
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
        FindObjectOfType<GameManager>().AddScore(2);
        Destroy(gameObject);
        Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
    }
}
