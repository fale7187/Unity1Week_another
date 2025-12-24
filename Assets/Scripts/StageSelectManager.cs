using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{
    [Header("ボタンの親オブジェクト (LevelGrid)")]
    public Transform gridParent;

    void Start()
    {
        // 1. セーブデータから「クリア済みの最高ステージ番号」を取得
        // デフォルトは0（ステージ1だけ遊べる状態）
        int clearedStageIndex = PlayerPrefs.GetInt("ClearedStageIndex", 0);

        // 2. ボタンを順番にチェックして設定
        for (int i = 0; i < gridParent.childCount; i++)
        {
            // ボタンコンポーネントを取得
            Button btn = gridParent.GetChild(i).GetComponent<Button>();

            // ステージ番号（1, 2, 3...）
            int stageNumber = i + 1;

            // ボタンの文字を "1", "2" に変える
            btn.GetComponentInChildren<TMPro.TMP_Text>().text = stageNumber.ToString();

            // クリック時の動作を登録
            int targetStage = stageNumber; // 一時変数に保存
            btn.onClick.AddListener(() => LoadStage(targetStage));

            // 開放ロジック: 「現在のボタン番号 <= クリア済み + 1」なら押せる
            if (i <= clearedStageIndex)
            {
                btn.interactable = true; // 押せる（色は通常）
            }
            else
            {
                btn.interactable = false; // 押せない（色はDisabledColor）
            }
        }
    }

    void LoadStage(int stageNum)
    {
        // シーン名は "Stage1", "Stage2" ... とする
        string sceneName = "Stage" + stageNum;

        // シーン読み込み
        SceneManager.LoadScene(sceneName);
    }

    // デバッグ用：Dキーでデータ消去
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("セーブデータをリセットしました");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}