using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Complete : UICanvas
{
    // Start is called before the first frame update
  
    // Start is called before the first frame update
    private void OnEnable()
    {
        RestartDOTweenAnimations();
    }

    private void RestartDOTweenAnimations()
    {
        // Restart tất cả animation trong đối tượng hiện tại và con của nó
        foreach (var tween in GetComponentsInChildren<DOTweenAnimation>())
        {
            tween.DORestart();
        }
    }
    public void Next()
    {
        Time.timeScale = 1;
        UIManager.Instance.CloseUI<Complete>(0.2f);
        LoadNextScene();

        SoundManager.Instance.PlayClickSound();
    }
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        // Kiểm tra xem scene tiếp theo có tồn tại không
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene("Home");
            UIManager.Instance.OpenUI<Level>();
            UIManager.Instance.CloseUIDirectly<Complete>();
            UIManager.Instance.CloseUIDirectly<GamePlay>();

        }
    }
}
