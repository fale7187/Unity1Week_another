using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalPoint : MonoBehaviour
{
    [Header("UI設定")]
    public GameObject clearUI;

    [Header("ステージ設定")]
    public int stageNumber = 1;
    public bool isFinalStage = false;

    private bool isCleared = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isCleared && collision.CompareTag("Player"))
        {
            isCleared = true;
            ShowClearUI();
            SaveProgress();
        }
    }

    void ShowClearUI()
    {
        Time.timeScale = 0f;
        if (clearUI != null) clearUI.SetActive(true);
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

    public void OnNextButtonPress()
    {
        Time.timeScale = 1f;
        if (isFinalStage)
        {
            SceneManager.LoadScene("ClearScene");
        }
        else
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(currentSceneIndex + 1);
            }
            else
            {
                SceneManager.LoadScene("TitleScene");
            }
        }
    }
}