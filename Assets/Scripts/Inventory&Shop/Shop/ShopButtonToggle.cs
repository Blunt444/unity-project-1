using UnityEngine;

public class ShopButtonToggle : MonoBehaviour
{
    public void OpenItemShop()
    {
        if (ShopKeeper.currentShopKeeper == null) return;
        ShopKeeper.currentShopKeeper.OpenItemShop();
    }

    public void OpenWeaponShop()
    {
        if (ShopKeeper.currentShopKeeper == null) return;
        ShopKeeper.currentShopKeeper.OpenWeaponShop();
    }

    public void OpenArmourShop()
    {
        if (ShopKeeper.currentShopKeeper == null) return;
        ShopKeeper.currentShopKeeper.OpenArmourShop();
    }
}
