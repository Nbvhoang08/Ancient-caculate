using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GamePlay : UICanvas
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    public Sprite OnVolume;
    public Sprite OffVolume;
    [SerializeField] private Text LevelName;
    [SerializeField] private Image buttonImage;

    void Start()
    {

        UpdateButtonImage();
    }

    private void Update()
    {
        UpdateLevelText();
    }

    public void RetryBtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SoundManager.Instance.PlayClickSound();
    }

    private void UpdateLevelText()
    {
        if (LevelName != null)
        {
            LevelName.text = "Level: " + SceneManager.GetActiveScene().name;
        }
    }

    public void Levelbtn()
    {
        UIManager.Instance.CloseAll();
        Time.timeScale = 1;
        SceneManager.LoadScene("Home");
        SoundManager.Instance.PlayClickSound();
        UIManager.Instance.OpenUI<Level>();
        UIManager.Instance.CloseUIDirectly<GamePlay>();
    }

    public void SoundBtn()
    {
        SoundManager.Instance.TurnOn = !SoundManager.Instance.TurnOn;
        UpdateButtonImage();
        SoundManager.Instance.PlayClickSound();
    }

    private void UpdateButtonImage()
    {
        if (SoundManager.Instance.TurnOn)
        {
            buttonImage.sprite = OnVolume;
        }
        else
        {
            buttonImage.sprite = OffVolume;
        }
    }
}
