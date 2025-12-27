using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    [Header("開閉するドア")]
    public GameObject door;

    [Header("スイッチの色設定")]
    public Color activeColor = Color.green;
    private Color defaultColor;
    private SpriteRenderer myRenderer;

    // 何人がスイッチに乗っているかカウント
    private int onSwitchCount = 0;

    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        if (myRenderer != null)
        {
            defaultColor = myRenderer.color; // 元の色を覚えておく
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Player, Shadow, Boxタグが乗ったら
        if (collision.CompareTag("Player") || collision.CompareTag("Shadow") || collision.CompareTag("Box"))
        {
            if (onSwitchCount == 0)
            {
                if (SoundManager.instance != null)
                {
                    SoundManager.instance.PlaySwitch();
                }
            }
            onSwitchCount++;
            UpdateDoorState();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Shadow") || collision.CompareTag("Box"))
        {
            onSwitchCount--;
            UpdateDoorState();
        }
    }

    void UpdateDoorState()
    {
        // ★シーンリセット時のエラー防止
        if (door == null) return;

        // 誰か一人でも乗っていれば「ON」
        bool isPressed = onSwitchCount > 0;

        if (isPressed)
        {
            door.SetActive(false); // ドアを開ける
            if (myRenderer != null) myRenderer.color = activeColor; // スイッチの色を変える
        }
        else
        {
            door.SetActive(true); // ドアを閉める
            if (myRenderer != null) myRenderer.color = defaultColor; // スイッチの色を戻す
        }
    }
}