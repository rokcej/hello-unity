using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Entity
{

    public float speed = 4f;
    public float shotDelay = 0.5f;

    public Transform bulletOriginCenter;
    public Transform bulletOriginLeft;
    public Transform bulletOriginRight;

    public GameObject bullet;
    public GameObject shield;
    public GameObject explosion;
	public HealthBar healthBar;

    public Text weaponTierText;

    private float lastShot = 0f;
    private bool hasShield = false;
    private int weaponTier = 1;

    private Vector2 bounds;

    Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<SpriteRenderer>().material;
        bounds = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f));
        shield.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if (Input.GetAxisRaw("Vertical") > 0f) {
            pos.y += speed * Time.deltaTime;
            if (pos.y > bounds.y) pos.y = bounds.y;
		} else if (Input.GetAxisRaw("Vertical") < 0f) {
            pos.y -= speed * Time.deltaTime;
            if (pos.y < -bounds.y) pos.y = -bounds.y;
        }

        if (Input.GetAxisRaw("Horizontal") > 0f) {
            pos.x += speed * Time.deltaTime;
            if (pos.x > bounds.x) pos.x = bounds.x;
        } else if (Input.GetAxisRaw("Horizontal") < 0f) {
            pos.x -= speed * Time.deltaTime;
            if (pos.x < -bounds.x) pos.x = -bounds.x;
        }

        transform.position = pos;

        if (Input.GetKey(KeyCode.Space))
            Shoot();
    }

    void Shoot() {
        float t = Time.time;
        if (t - lastShot >= shotDelay) {
            if (weaponTier <= 1) {
                Instantiate(bullet, bulletOriginCenter.position, Quaternion.identity);
            } else if (weaponTier == 2) {
                Instantiate(bullet, bulletOriginLeft.position, Quaternion.identity);
                Instantiate(bullet, bulletOriginRight.position, Quaternion.identity);
            } else if (weaponTier >= 3) {
                Instantiate(bullet, bulletOriginCenter.position, Quaternion.identity);
                Instantiate(bullet, bulletOriginLeft.position, Quaternion.identity);
                Instantiate(bullet, bulletOriginRight.position, Quaternion.identity);
            }
            lastShot = t;
        }
	}
	private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Asteroid")) {
            AudioManager.instance.Play("PlayerHit");
            Asteroid asteroid = collision.gameObject.GetComponent<Asteroid>();
            if (hasShield) {
                asteroid.Kill();
                shield.SetActive(false);
                hasShield = false;
            } else {
                int hpTemp = hp;
                TakeDamage(asteroid.hp);
                asteroid.TakeDamage(hpTemp);
            }
        } else if (collision.gameObject.CompareTag("Enemy")) {
            AudioManager.instance.Play("PlayerHit");
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (hasShield) {
                enemy.Kill();
                shield.SetActive(false);
                hasShield = false;
            } else {
                int hpTemp = hp;
                TakeDamage(enemy.hp);
                enemy.TakeDamage(hpTemp);
            }
        } else if (collision.gameObject.CompareTag("EnemyBullet")) {
            AudioManager.instance.Play("PlayerHit");
            if (hasShield) {
                shield.SetActive(false);
                hasShield = false;
            } else {
                TakeDamage(collision.gameObject.GetComponent<Bullet>().damage);
            }
            Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("Shield")) {
            AudioManager.instance.Play("Shield");
            hasShield = true;
            shield.SetActive(true);
            Destroy(collision.gameObject);
		} else if (collision.gameObject.CompareTag("Weapon")) {
            AudioManager.instance.Play("Weapon");
            ++weaponTier;
            shotDelay *= 0.9f;
            weaponTierText.text = "WEAPON TIER: " + weaponTier.ToString();
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(int damage) {
        hp -= damage;
        if (hp < 0) hp = 0;
        healthBar.SetValue(hp);

        if (hp <= 0) {
            Destroy(gameObject);
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            FindObjectOfType<GameManager>().EndGame();
		} else {
            CancelInvoke("Flash");
            mat.SetColor("_Intensity", new Color(1f, 0.25f, 0.25f));
            Invoke("Flash", 0.1f);
        }
	}
    private void Flash() {
        mat.SetColor("_Intensity", new Color(0f, 0f, 0f));
    }
}
 