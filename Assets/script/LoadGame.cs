using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 加载游戏场景
/// </summary>
public class LoadGame : MonoBehaviour
{
    /// <summary>
    /// 载入场景
    /// </summary>
    public void LoadingGame()
    {
        //加载进入1号场景
        SceneManager.LoadSceneAsync(1);
    }
}