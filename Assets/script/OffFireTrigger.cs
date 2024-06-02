using UnityEngine;

public class OffFireTrigger : MonoBehaviour
{
    public GameObject Torch1;
    public GameObject Torch2;
    private AudioSource player;
    public AudioClip OffFire;
    // Start is called before the first frame update
    void Start(){
        player = GameObject.Find("torch_7").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other){
        if (other.name == "KnightBody"){
            if (Torch1.activeInHierarchy && Torch2.activeInHierarchy){
                player.PlayOneShot(OffFire, 0.1f);
                Torch1.SetActive(false);
                Torch2.SetActive(false);
            }
        }
    }
}
