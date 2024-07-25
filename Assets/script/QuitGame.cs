using UnityEngine;

/// <summary>
/// 退出游戏
/// </summary>
public class QuitGame : MonoBehaviour
{
    public void EndingGame()
    {
        //#if UNITY_EDITOR和#endif是用来让打包的游戏不要运行其中的代码，UnityEditor是在unity里使用的，打包后是用不了的，会导致打包出错
#if UNITY_EDITOR
        //设置运行状态为false，退出unity运行状态
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        //退出游戏
        Application.Quit();
    }
}