using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Zキー切り替え用フラグ（SwitchManagerが操作します）")]
    public bool canMove = true;

    [Header("移動パラメーター")]
    public float moveSpeed = 5f; // 横移動の速さ
    public float jumpForce = 10f; // ジャンプ力

    private Rigidbody2D rb;
    private bool isGrounded = false; // 地面にいるか？

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // ■ 操作権がない場合の処理
        if (!canMove)
        {
            // 横方向の速度だけ 0 にする（重力による落下は止めない）
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return; // ここで処理を終了
        }

        // ■ 横移動 (A/Dキー または 矢印)
        float x = Input.GetAxisRaw("Horizontal");
        // 速度を直接セットする（Y軸は今の速度を維持して重力に任せる）
        rb.linearVelocity = new Vector2(x * moveSpeed, rb.linearVelocity.y);

        // ■ ジャンプ (Spaceキー)
        // 「地面にいる時」かつ「スペースキーを押した瞬間」
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            // 上方向（Y軸正方向）に力を加える
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    // ■ 接地判定（何かにぶつかった時）
    // PlayerMovement.cs の接地判定を「上から踏んだ時だけ」にする
    // 接地判定（ぶつかった時）
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Box"))
        {
            CheckGrounded(collision, true);
        }
    }

    // 接地判定（触れている間も継続チェック）
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Box"))
        {
            CheckGrounded(collision, true);
        }
    }

    // 接地判定（離れた時）
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Box"))
        {
            // 離れた時は一旦 false にするが、Stay側で別の足場に触れていれば true に戻る
            isGrounded = false;
        }
    }

    // 足元でぶつかっているか判定する共通関数
    private void CheckGrounded(Collision2D collision, bool state)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            // 上向きの面（足元）に触れているか判定
            if (contact.normal.y > 0.5f)
            {
                isGrounded = state;
                return;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1. まずトゲに触れたかチェック
        if (collision.CompareTag("Trap"))
        {
            // 2. ★追加：自分のタグが "Player"（本体）の時だけ死ぬ
            // （影のタグを "Shadow" に設定している前提です）
            if (gameObject.CompareTag("Player"))
            {
                Debug.Log("本体がトゲに触れたので死亡！");
                UnityEngine.SceneManagement.SceneManager.LoadScene(
                    UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
                );
            }
        }
    }
}