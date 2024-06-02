using UnityEngine;

public class OnCandleTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject candle;
    private AudioSource player;
    public AudioClip OpenLight;

    private void Start(){
        player = GameObject.Find("MusicTrigger").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other){
        if (other.name == "KnightBody"){
            if (!candle.GetComponent<Light>().enabled){
                player.PlayOneShot(OpenLight);
                candle.GetComponent<Light>().enabled = true;
            }
        }
    }
}
