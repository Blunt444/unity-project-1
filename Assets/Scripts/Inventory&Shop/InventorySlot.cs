using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public ItemSO itemSO;
    public int quantity;
    public TMP_Text quantityText;
    public Image itemImage;

    
    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GetComponentInParent<InventoryManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(quantity > 0)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
            {
                inventoryManager.UseItem(this);
            }
        }
    }

    public void UpdateUI()
    {
        if (itemSO == null)
        {
            itemImage.gameObject.SetActive(false);
            quantityText.text = "";
            return;
        }

        itemImage.sprite = itemSO.icon;
        itemImage.gameObject.SetActive(true);
        quantityText.text = quantity.ToString();
    }

}
