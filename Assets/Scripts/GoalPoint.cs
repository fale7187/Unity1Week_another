using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalPoint : MonoBehaviour
{
    [Header("このステージの番号")]
    public int stageNumber = 1;

    [Header("ClearPanel")]
    public GameObject clearUI;

    private bool isCleared = false; // 2回発動しないように

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // まだクリアしてなくて、プレイヤーが触れたら
        if (!isCleared && collision.CompareTag("Player"))
        {
            isCleared = true;
            ShowClearUI();
            SaveProgress(); // クリアした瞬間にセーブ
        }
    }

    void ShowClearUI()
    {
        Debug.Log("クリア！");
        // プレイヤーの動きを止める
        Time.timeScale = 0f;

        // パネルを表示！
        if (clearUI != null)
        {
            clearUI.SetActive(true);
        }
    }

    void SaveProgress()
    {
        int currentRecord = PlayerPrefs.GetInt("ClearedStageIndex", 0);
        if (stageNumber > currentRecord)
        {
            PlayerPrefs.SetInt("ClearedStageIndex", stageNumber);
            PlayerPrefs.Save();
        }
    }

    // Nextボタンの OnClick に登録する
    public void OnNextButtonPress()
    {
        // 時間を動かす
        Time.timeScale = 1f;

        // 次のステージへ
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0); // タイトルへ
        }
    }
}