using UnityEngine;

public class OffCandleTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject candle;
    private AudioSource player;
    private GameObject MusicTrigger;

    private void Start(){
        MusicTrigger = GameObject.Find("MusicTrigger");
        player = MusicTrigger.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other){
        if (other.name == "KnightBody"){
            MusicTrigger.GetComponent<BackgroundMusic>().IfBackgroundMusic = false;
            player.Stop();
            candle.GetComponent<Light>().enabled = false;
        }
    }
}
