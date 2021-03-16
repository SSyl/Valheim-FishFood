using System.IO;
using BepInEx;
using JotunnLib.Entities;
using UnityEngine;

namespace FishFood
{
    public class FishSoupPrefab : PrefabConfig
    {
        public FishSoupPrefab() : base("FishSoup", "CarrotSoup")
        {

        }

        public override void Register()
        {
            // Configure item drop
            ItemDrop item = Prefab.GetComponent<ItemDrop>();
            item.m_itemData.m_shared.m_itemType = ItemDrop.ItemData.ItemType.Consumable;
            item.m_itemData.m_shared.m_name = "Fish Soup";
            item.m_itemData.m_shared.m_description = "A fisherman's delight.";
            item.m_itemData.m_dropPrefab = Prefab;
            item.m_itemData.m_shared.m_weight = 1f;
            item.m_itemData.m_shared.m_maxStackSize = 10;
            item.m_itemData.m_shared.m_variants = 1;
            item.m_itemData.m_shared.m_food = 60f;
            item.m_itemData.m_shared.m_foodStamina = 60f;
            item.m_itemData.m_shared.m_foodBurnTime = 2000f;
            item.m_itemData.m_shared.m_foodRegen = 3f;
            // Color = R, G, B. You divide by 255 because unity wants the color between 0 to 1. (0 being black, 1 being white)
            item.m_itemData.m_shared.m_foodColor = new Color(222/255f, 155/255f, 111/255f);

            var LoadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Paths.PluginPath, "FishFood", "fishfood"));

            if (LoadedAssetBundle == null)
            {
                Debug.Log("Failed to load AssetBundle!");
                return;
            }

            Texture2D icon = LoadedAssetBundle.LoadAsset<Texture2D>("fishsoup-sprite.png");
            if (icon != null)
            {
                Sprite sprite = Sprite.Create(icon, new Rect(0f, 0f, icon.width, icon.height), Vector2.zero);
                //m_icons[0] is the actual sprite itself.
                item.m_itemData.m_shared.m_icons[0] = sprite;
            }

            MeshRenderer[] renderers = Prefab.GetComponentsInChildren<MeshRenderer>(true);

            foreach (var rd in renderers)
            {
                if (rd.material.ToString().Contains("carrotsoup"))
                {
                    Texture2D texture = LoadedAssetBundle.LoadAsset<Texture2D>("fishsoup-texture.png");

                    if (rd.material != null && texture != null)
                    {
                        rd.material.mainTexture = texture;
                    }
                }
                else if(rd.material.ToString().Contains("carrot"))
                {
                    rd.material.color = new Color(186 / 255f, 167 / 255f, 162 / 255f);
                }
            }

            /*MeshRenderer renderer = Prefab.GetComponentInChildren<MeshRenderer>(true);
            Texture2D texture = AssetUtils.LoadTexture(Paths.PluginPath + "/FishFood/fishsausages-texture.png");
            if (texture != null)
            {
                //try to get the texture immediately
                Debug.Log(string.Format("Renderer: {0}", rd));
                Debug.Log(string.Format("Material: {0}", rd.material));
                Debug.Log(string.Format("Texture: {0}", rd.material.mainTexture));
                renderer.material.mainTexture = texture;
            }*/

            LoadedAssetBundle?.Unload(false);
        }
    }
}