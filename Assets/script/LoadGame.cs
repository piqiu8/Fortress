using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    //��ʼ��Ϸ����
    public void LoadingGame(){
        //���ؽ���1�ų���
        SceneManager.LoadSceneAsync(1);
    }
}
