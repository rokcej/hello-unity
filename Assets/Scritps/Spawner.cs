using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject[] asteroids;
    public GameObject enemy;
    public GameObject shield;
    public GameObject weapon;

    public Text difficultyText;

    public float scalingFactor = 1.1f;
    public float scalingDelay = 10f;

    private float timeStart;
    private int difficultyLevel = 0;
    private float difficultyScale = 1f;

    private Vector2 bounds;

    // Start is called before the first frame update
    void Start()
    {
        timeStart = Time.time;
        bounds = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f));

        transform.position = new Vector3(bounds.x + 1f, 0, 0);

        Invoke("SpawnAsteroid", Random.Range(0f, 1f));
        Invoke("SpawnEnemy", Random.Range(0f, 1f));
        Invoke("SpawnShield", Random.Range(5f, 15f));
        Invoke("SpawnWeapon", Random.Range(15f, 30f));
    }

    // Update is called once per frame
    void Update()
    {
        float elapsed = Time.time - timeStart;
        if ((int)elapsed / 10 > difficultyLevel) {
            ++difficultyLevel;
            difficultyScale *= scalingFactor;
            difficultyText.text = "DIFFICULTY MULTIPLIER: " + difficultyScale.ToString("#.00");
		}
          
    }

    void SpawnAsteroid() {
        Vector3 offset = new Vector3(0f, Random.Range(-bounds.y, bounds.y), 0f);
        Asteroid a = Instantiate(asteroids[Random.Range(0, asteroids.Length)], gameObject.transform.position + offset, Quaternion.identity).GetComponent<Asteroid>();
        a.hp = (int)(a.hp * difficultyScale);
        a.speedRange *= difficultyScale;
        Invoke("SpawnAsteroid", Random.Range(2f, 4f) / difficultyScale);
    }
    void SpawnEnemy() {
        Vector3 offset = new Vector3(0f, Random.Range(-bounds.y, bounds.y), 0f);
        Enemy e = Instantiate(enemy, gameObject.transform.position + offset, Quaternion.identity).GetComponent<Enemy>();
        e.hp = (int)(e.hp * difficultyScale);
        e.speedRange *= difficultyScale;
        e.shotDelay /= difficultyScale;
        Invoke("SpawnEnemy", Random.Range(3f, 5f) / difficultyScale);
    }

    void SpawnShield() {
        Vector3 offset = new Vector3(0f, Random.Range(-bounds.y, bounds.y), 0f);
        Instantiate(shield, gameObject.transform.position + offset, Quaternion.identity);
        Invoke("SpawnShield", Random.Range(10f, 15f));
    }

    void SpawnWeapon() {
        Vector3 offset = new Vector3(0f, Random.Range(-bounds.y, bounds.y), 0f);
        Instantiate(weapon, gameObject.transform.position + offset, Quaternion.identity);
        Invoke("SpawnWeapon", Random.Range(15f, 30f));
    }
}
