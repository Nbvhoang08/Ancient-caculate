using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManger : MonoBehaviour
{
  
    [Header("Card Configuration")]
    public GameObject CardPrefab;
    public Sprite PositiveSprite;
    public Sprite NegativeSprite;

    [Header("Card Values")]
    public Vector2 SpawnArea; // Khu vực spawn (X và Y)
    public List<int> CardValues = new List<int>();
    public int TargetSum;
    public bool isComplete;
    public bool HasWon;
    public TextMeshPro targetText;
    public Animator animator;



    private void Awake()
    {
        GenerateCards();
        
    }
    private void Start()
    {
        if(targetText != null)
        {
            targetText.text = TargetSum.ToString();
        }
        isComplete = false;
    }
    private void Update()
    {
        if(animator!=null) animator.SetBool("open", isComplete);

    }

    public void OpenWinUI()
    {
        StartCoroutine(OpenWin());
    }
    IEnumerator OpenWin()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("win");
        isComplete = true;
        SoundManager.Instance.PlayVFXSound(3);
        LevelManager.Instance.SaveGame();
        yield return new WaitForSeconds(1.5f);
        UIManager.Instance.OpenUI<Complete>();
        Time.timeScale = 0;
    }

    public void GenerateCards()
    {
        if (CardValues.Count == 0)
        {
            Debug.LogError("CardValues is empty! Please configure the card values in the Inspector.");
            return;
        }

        // Tính toán khoảng cách giữa các thẻ dựa trên số lượng thẻ và khu vực spawn
        float startX = -SpawnArea.x / 2; // Vị trí bắt đầu (bên trái khu vực spawn)
        float gap = SpawnArea.x / (CardValues.Count - 1); // Khoảng cách giữa các thẻ

        for (int i = 0; i < CardValues.Count; i++)
        {
            // Tính toán vị trí spawn cho từng thẻ
            float x = startX + i * gap;
            Vector3 spawnPosition = new Vector3(x, SpawnArea.y, 0);

            // Tạo thẻ
            GameObject cardObj = Instantiate(CardPrefab, spawnPosition, Quaternion.identity);
            NumberCard card = cardObj.GetComponent<NumberCard>();
            card.PositiveSprite = PositiveSprite;
            card.NegativeSprite = NegativeSprite;
            card.gameManager = GetComponent<GameManger>();
            card.Initialize(CardValues[i]);
        }
    }

    public void MergeCards(NumberCard card1, NumberCard card2)
    {
        if (card1 != null && card2 != null)
        {
            card1.Merge(card2);
        }
    }
}
