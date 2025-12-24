using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalPoint : MonoBehaviour
{
    [Header("このステージの番号（1, 2...）")]
    public int stageNumber = 1;

    // プレイヤー（タグがPlayer）が触れたら発動
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameClear();
        }
    }

    void GameClear()
    {
        Debug.Log("ステージ " + stageNumber + " クリア！");

        // 1. セーブデータを更新
        // 「今クリアしたステージ」が「これまでの記録」より大きければ更新する
        int currentRecord = PlayerPrefs.GetInt("ClearedStageIndex", 0);

        if (stageNumber > currentRecord)
        {
            PlayerPrefs.SetInt("ClearedStageIndex", stageNumber);
            PlayerPrefs.Save(); // 確実に保存
        }

        // 2. ステージセレクトに戻る
        SceneManager.LoadScene("StageSelectScene");
    }
}