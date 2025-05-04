using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsScroller : MonoBehaviour
{
    public float scrollSpeed = 50f;
    public RectTransform creditsText;
    public float endPositionY = 1000f;
    public Button exitButton;
    private float startPositionY;
    private bool hasReachedEnd = false;

    void Start()
    {
        startPositionY = creditsText.anchoredPosition.y;
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ReturnToMainMenu);
        }
    }

    void Update()
    {
        if (!hasReachedEnd)
        {
            creditsText.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
            if (creditsText.anchoredPosition.y >= endPositionY)
            {
                hasReachedEnd = true;
                ReturnToMainMenu();
            }
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
