using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ������Ϸ����
/// </summary>
public class LoadGame : MonoBehaviour
{
    /// <summary>
    /// ���볡��
    /// </summary>
    public void LoadingGame()
    {
        //���ؽ���1�ų���
        SceneManager.LoadSceneAsync(1);
    }
}