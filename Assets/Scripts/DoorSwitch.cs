using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    [Header("開閉するドア")]
    public GameObject door;

    [Header("ドアが開いているときの色（半透明とか）")]
    public Color activeColor = Color.green;
    private Color defaultColor;
    private SpriteRenderer myRenderer;

    // 何人がスイッチに乗っているかカウント
    private int onSwitchCount = 0;

    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        defaultColor = myRenderer.color; // 元の色を覚えておく
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Playerタグ（本体または影）が乗ったら
        if (collision.CompareTag("Player") || collision.CompareTag("Shadow"))
        {
            onSwitchCount++; // 乗っている人数を増やす
            UpdateDoorState();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Shadow"))
        {
            onSwitchCount--; // 乗っている人数を減らす
            UpdateDoorState();
        }
    }

    void UpdateDoorState()
    {
        if (onSwitchCount > 0)
        {
            // 誰かが乗っている -> ドアを消す（開ける）
            door.SetActive(false);
            myRenderer.color = activeColor; // スイッチの色を変える
        }
        else
        {
            // 誰も乗っていない -> ドアを出す（閉める）
            door.SetActive(true);
            myRenderer.color = defaultColor; // スイッチの色を戻す
        }
    }
}