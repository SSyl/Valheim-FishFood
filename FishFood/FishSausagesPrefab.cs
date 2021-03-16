using System.IO;
using BepInEx;
using JotunnLib.Entities;
using UnityEngine;

namespace FishFood
{
    public class FishSausagesPrefab : PrefabConfig
    {
        public FishSausagesPrefab() : base("FishSausages", "Sausages")
        {

        }

        public override void Register()
        {
            // Configure item drop
            ItemDrop item = Prefab.GetComponent<ItemDrop>();
            item.m_itemData.m_shared.m_itemType = ItemDrop.ItemData.ItemType.Consumable;
            item.m_itemData.m_shared.m_name = "Fish Sausages";
            item.m_itemData.m_shared.m_description = "Salty links of fish. The after taste is somehow fishier...";
            item.m_itemData.m_dropPrefab = Prefab;
            item.m_itemData.m_shared.m_weight = 0.5f;
            item.m_itemData.m_shared.m_maxStackSize = 20;
            item.m_itemData.m_shared.m_variants = 1;
            item.m_itemData.m_shared.m_food = 50f;
            item.m_itemData.m_shared.m_foodStamina = 50f;
            item.m_itemData.m_shared.m_foodBurnTime = 1600f;
            item.m_itemData.m_shared.m_foodRegen = 3f;
            // Color = R, G, B. You divide by 255 because unity wants the color between 0 to 1. (0 being black, 1 being white)
            item.m_itemData.m_shared.m_foodColor = new Color(255/255f, 237/255f, 158/255f);

            var LoadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Paths.PluginPath, "FishFood", "fishfood"));

            if (LoadedAssetBundle == null)
            {
                Debug.Log("Failed to load AssetBundle!");
                return;
            }

            Texture2D icon = LoadedAssetBundle.LoadAsset<Texture2D>("fishsausages-sprite.png");
            if (icon != null)
            {
                Sprite sprite = Sprite.Create(icon, new Rect(0f, 0f, icon.width, icon.height), Vector2.zero);
                //m_icons[0] is the actual sprite itself.
                item.m_itemData.m_shared.m_icons[0] = sprite;
            }

            MeshRenderer renderer = Prefab.GetComponentInChildren<MeshRenderer>();

            Texture2D texture = LoadedAssetBundle.LoadAsset<Texture2D>("fishsausages-texture.png");
            if (renderer != null && texture != null)
            {
                renderer.material.mainTexture = texture;
            }

            LoadedAssetBundle?.Unload(false);
        }
    }
}