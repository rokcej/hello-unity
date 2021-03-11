using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance = null;

    // Awake before Start
	void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
		}
    }

    // Start is called before the first frame update
    private void Start() {
        Play("Theme");
    }

    // Update is called once per frame
    void Update()
    {

    }

	public void Play(string name) {
        Sound s = System.Array.Find(sounds, Sound => Sound.name == name);
        s.source.Play();
	}
}
