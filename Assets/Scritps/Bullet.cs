using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2f;
    public int damage = 50;

    public Vector2 dir = new Vector2(1f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        float angle = 90f; // Sign isn't necessary because of symmetry
        if (dir.x != 0f) {
            angle = Mathf.Atan(dir.y / dir.x) * 90f / (Mathf.PI * 0.5f);
        }

        transform.Rotate(new Vector3(0f, 0f, angle));
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += dir.x * speed * Time.deltaTime;
        pos.y += dir.y * speed * Time.deltaTime;
        transform.position = pos;

        if (!GetComponent<Renderer>().isVisible) {
        //if (pos.x > 25f || pos.x < -25f || pos.y > 6f || pos.y < -6f) {
            Destroy(gameObject);
        }
    }
}
