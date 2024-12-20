using UnityEngine;
using TMPro;
public class NumberCard : MonoBehaviour
{
    private Vector3 _startPosition; // Vị trí ban đầu của thẻ
    private bool _isDragging = false; // Trạng thái kéo thả
    private Camera _mainCamera; // Camera chính để lấy vị trí chuột trong World Space
    public GameManger gameManager;
    public int Value { get; private set; }
    public SpriteRenderer CardRenderer; // SpriteRenderer để thay đổi sprite
    public Sprite PositiveSprite;
    public Sprite NegativeSprite;
    public TextMeshPro ValueText; // Hiển thị giá trị trên thẻ

    private void Start()
    {
        _mainCamera = Camera.main; // Gán Camera chính
    }
    private void Update()
    {
        if (Value == gameManager.TargetSum && !gameManager.HasWon)
        {
         
            gameManager.HasWon = true;
            gameManager.OpenWinUI();
        }
    }

    public void Initialize(int value)
    {
        Value = value;
        UpdateCard();
    }

    private void UpdateCard()
    {
        ValueText.text = Value.ToString();
        CardRenderer.sprite = Value >= 0 ? PositiveSprite : NegativeSprite;
    }

    private void OnMouseDown()
    {
        _startPosition = transform.position; // Lưu vị trí ban đầu
        _isDragging = true;
        SoundManager.Instance.PlayVFXSound(0);
    }

    private void OnMouseDrag()
    {
        if (_isDragging)
        {
            // Lấy vị trí chuột trong World Space
            Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Đặt Z = 0 để giữ thẻ trên cùng mặt phẳng
            transform.position = mousePosition;
            CardRenderer.sortingOrder = 80;
            ValueText.sortingOrder = 100;
        }
    }

    private void OnMouseUp()
    {
        _isDragging = false;

        // Tìm các thẻ gần vị trí thả
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1f); // Bán kính 1 đơn vị
        foreach (Collider2D hit in hits)
        {
            NumberCard otherCard = hit.GetComponent<NumberCard>();
            if (otherCard != null && otherCard != this)
            {
                // Nếu thả gần thẻ khác -> merge
                gameManager.MergeCards(this, otherCard);
                return;
            }
        }
        CardRenderer.sortingOrder = 30;
        ValueText.sortingOrder = 70;
        // Nếu không merge -> quay về vị trí ban đầu
        transform.position = _startPosition;
        SoundManager.Instance.PlayVFXSound(0);
    }

    public void Merge(NumberCard otherCard)
    {
        Value += otherCard.Value;
        UpdateCard();
        Destroy(otherCard.gameObject);
        SoundManager.Instance.PlayVFXSound(1);
    }

    private void OnDrawGizmosSelected()
    {
        // Hiển thị vùng merge trong Scene View để dễ chỉnh sửa
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
