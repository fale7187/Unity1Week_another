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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ぶつかった相手のタグが "Ground" だったら
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // 地面にいる！
        }
    }

    // ■ 接地判定（離れた時）
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; // 空中にいる！
        }
    }
}