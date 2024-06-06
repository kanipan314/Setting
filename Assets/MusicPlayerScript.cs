using UnityEngine;

public class MusicPlayerScript : MonoBehaviour
{
    private AudioSource audioSource;

    // Start is called before the first frame update

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play(); // âπäyÇçƒê∂Ç∑ÇÈ
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
