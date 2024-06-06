using UnityEngine;

public class MusicPlayerScript : MonoBehaviour
{
    private AudioSource audioSource;

    // Start is called before the first frame update

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play(); // 音楽を再生する
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
