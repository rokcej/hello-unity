using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        Invoke("_Destroy", 1.01f);
    }

    void _Destroy() {
        Destroy(gameObject);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
