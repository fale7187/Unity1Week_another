using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Update()
    {
        // Rキーが押されたらリトライ
        if (Input.GetKeyDown(KeyCode.R))
        {
            RetryScene();
        }
    }

    public void RetryScene()
    {
        // 現在のシーンの名前を取得して、読み込み直す
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}