//#define DEBUG_MODE

using UnityEngine;

/// <summary>
/// 实现昼夜转换
/// </summary>
public class LightRotationTrigger : MonoBehaviour
{
    /// <summary>
    /// 全局游戏光照
    /// </summary>
    private GameObject Light;

    /// <summary>
    /// 初始光照角度
    /// </summary>
    private Quaternion source_quaternion = Quaternion.Euler(91f, -90f, 0f);

    /// <summary>
    /// 旋转后光照角度
    /// </summary>
    private Quaternion rotate_quaternion = Quaternion.Euler(269f, -90f, 0f);

    /// <summary>
    /// 光照状态，用于判定光照时复原还是旋转
    /// </summary>
    private bool x = true;

    /// <summary>
    /// 光照旋转速度，默认为0.3
    /// </summary>
    public float rotate_speed = 0.3f;

    /// <summary>
    /// 光照标记，用于确认复原还是旋转
    /// </summary>
    private int rotate_tag = 0;

    private void Start()
    {
        Light = GameObject.Find("Directional Light");
#if DEBUG_MODE
        if (!Light)
        {
            Debug.LogError("未获取到光照");
            return;
        }
#endif
    }

    private void Update()
    {
        if (rotate_tag == 1) Light.transform.rotation = Quaternion.Lerp(Light.transform.rotation, rotate_quaternion, Time.deltaTime * rotate_speed);
        else Light.transform.rotation = Quaternion.Lerp(Light.transform.rotation, source_quaternion, Time.deltaTime * rotate_speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        //玩家第一次触发标记为1，后续触发交替标记
        if (other.name == "KnightBody")
        {
            if (x)
            {
                rotate_tag = 1;
                x = !x;
            }
            else
            {
                rotate_tag = 2;
                x = !x;
            }
        }
    }
}