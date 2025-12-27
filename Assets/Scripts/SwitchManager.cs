using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    [Header("プレイヤーと影を登録")]
    public PlayerMovement player;
    public PlayerMovement shadow;

    // 今、プレイヤーを操作しているか？
    private bool isPlayerTurn = true;

    void Start()
    {
        // 最初の状態を適用
        Time.timeScale = 1f;
        UpdateControl();
    }

    void Update()
    {
        // Zキーが押されたら切り替え
        if (Input.GetKeyDown(KeyCode.Z))
        {
            isPlayerTurn = !isPlayerTurn; // trueとfalseを反転
            UpdateControl();
        }
    }

    void UpdateControl()
    {
        if (isPlayerTurn)
        {
            // プレイヤーのターン
            SetActive(player, true);
            SetActive(shadow, false);
        }
        else
        {
            // 影のターン
            SetActive(player, false);
            SetActive(shadow, true);
        }
    }

    // 動けるフラグと、見た目の色を変える関数
    void SetActive(PlayerMovement character, bool isActive)
    {
        // 1. 動けるかどうか設定
        character.canMove = isActive;

        // 2. 色を変えて分かりやすくする
        SpriteRenderer sr = character.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            // 動けるなら「不透明（ハッキリ）」、休みなら「半透明（薄く）」
            Color c = sr.color;
            c.a = isActive ? 1.0f : 0.5f;
            sr.color = c;
        }
    }
}