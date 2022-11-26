using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI; // Pour la variable Text

public class Inventory : MonoBehaviour
{
    public int coinsCount;
    public Text coinsCountText;
    public List<Item> content = new List<Item>();
    private int contentCurrentIndex = 0;
    public Image itemImageUI;
    public Text itemNameUI;
    public Sprite emptyItemImage;
    public PlayerEffects playerEffects;
    
    // Créér un unique inventaire accéssible de partout
    public static Inventory instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Inventory dans la scène");
            return;
        }

        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ConsumeItem();
        }
    }

    private void Start()
    {
        UpdateInventoryUI();
    }

    public void ConsumeItem()
    {
        if (content.Count == 0)
        {
            return;
        }

        Item currentItem = content[contentCurrentIndex];
        PlayerHealth.instance.HealPlayer(currentItem.hpGiven);
        playerEffects.AddSpeed(currentItem.speedGiven, currentItem.speedDuration);
        content.Remove(currentItem);
        GetNextItem();
        UpdateInventoryUI();
    } 

    public void GetNextItem()
    {
        if (content.Count == 0)
        {
            return;
        }

        contentCurrentIndex++;
        if (contentCurrentIndex > content.Count - 1)
        {
            contentCurrentIndex = 0;
        }
        UpdateInventoryUI();
    }

    public void GetPreviousItem()
    {
        if (content.Count == 0)
        {
            return;
        }

        contentCurrentIndex--;
        if (contentCurrentIndex < 0)
        {
            contentCurrentIndex = content.Count - 1;
        }
        UpdateInventoryUI();
    }

    public void UpdateInventoryUI()
    {
        if (content.Count > 0)
        {
            itemImageUI.sprite = content[contentCurrentIndex].image;
            itemNameUI.text = content[contentCurrentIndex].itemName;
        }
        else
        {
            itemImageUI.sprite = emptyItemImage;
            itemNameUI.text = "";
        }
    }

    public void AddCoins(int count)
    {
        coinsCount += count;
        CurrentSceneManager.instance.coinsPickedUpInThisSceneCount += count;
        UpdateTextUI();
    }

    public void RemoveCoins(int count)
    {
        coinsCount -= count;
        UpdateTextUI();
    }

    public void UpdateTextUI()
    {
        coinsCountText.text = coinsCount.ToString();
    }
}
