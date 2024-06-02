using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    //开始游戏方法
    public void LoadingGame(){
        //加载进入1号场景
        SceneManager.LoadSceneAsync(1);
    }
}
