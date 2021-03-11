using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBackground : MonoBehaviour
{
    public GameObject[] sprites;
    public float[] speeds;

    float[] startPos;
    float[] lengths;

    // Start is called before the first frame update
    void Start()
    {
        startPos = new float[sprites.Length];
        lengths = new float[sprites.Length];

        for (int i = 0; i < sprites.Length; ++i) {
            startPos[i] = sprites[i].transform.position.x;
            lengths[i] = sprites[i].GetComponent<SpriteRenderer>().bounds.size.x;

            GameObject right = Instantiate(sprites[i]);
            right.transform.position += new Vector3(lengths[i], 0f, 0f);

            GameObject left = Instantiate(sprites[i]);
            left.transform.position -= new Vector3(lengths[i], 0f, 0f);

            right.transform.SetParent(sprites[i].transform);
            left.transform.SetParent(sprites[i].transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < sprites.Length; ++i) {
            float dist = (Time.time * speeds[i]) % lengths[i];
            sprites[i].transform.position = new Vector3(startPos[i] - dist, transform.position.y, transform.position.z);
        }
    }
}
