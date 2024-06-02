using UnityEngine;

public class LightRotationTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject Light;
    //创建光照
    private Quaternion source_quaternion =Quaternion.Euler(91f,-90f,0f), rotate_quaternion = Quaternion.Euler(269f, -90f, 0f);
    //创建光照初始角度与旋转后角度
    private bool x = true;
    //用于判定光照是复原还是旋转
    public float rotate_speed = 0.3f;
    //光照旋转速度
    private int rotate_tag=0;
    //光照标记，用于确认复原还是旋转

    void Start(){
        Light = GameObject.Find("Directional Light");
        //获取光照
    }
    void Update(){
        if (rotate_tag == 1) Light.transform.rotation = Quaternion.Lerp(Light.transform.rotation, rotate_quaternion, Time.deltaTime * rotate_speed);
        else Light.transform.rotation = Quaternion.Lerp(Light.transform.rotation, source_quaternion, Time.deltaTime * rotate_speed);
    }
    private void OnTriggerEnter(Collider other){//确认标记
        if (other.name == "KnightBody"){
            if (x){
                rotate_tag = 1;
                x = !x;
            }
            else{
                rotate_tag = 2;
                x = !x;
            }
        }
        //玩家第一次触发标记为1，后续触发交替标记
    }
}
