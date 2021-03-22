using System;
using BepInEx;
using BepInEx.Configuration;
using JotunnLib.Managers;
using JotunnLib.Entities;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;

namespace FishFood
{
    [BepInPlugin("SSyl.FishFood", "FishFood", "1.1.0")]
    [BepInDependency("com.bepinex.plugins.jotunnlib")]
    public class FishFood : BaseUnityPlugin
    {
        public static ConfigEntry<bool> modEnabled;
        public static ConfigEntry<float> repairAmount;
        public static ConfigEntry<float> repairAmountAdvancedKit;
        public static ConfigEntry<bool> customJsonRecipes;
        public static ConfigEntry<int> nexusID;

        private void Awake()
        {
            modEnabled = Config.Bind("General", "Enabled", true, "Settings this to false disables all features of this mod.");
            customJsonRecipes = Config.Bind("General", "CustomJsonRecipes", false, "Setting this to true will make the mod use custom recipes that you've defined in a seperate custom_recipes.json file.\nYou must create this file yourself, otherwise this setting won't do anything.");
            nexusID = Config.Bind("General", "NexusID", 531, "Nexus mod ID for update checker. Do not change this value.");

            if (!modEnabled.Value)
                return;

                // Register our handler
                ObjectManager.Instance.ObjectRegister += registerObjects;
                PrefabManager.Instance.PrefabRegister += registerPrefabs;
        }
        private void registerPrefabs(object sender, EventArgs e)
        {
            PrefabManager.Instance.RegisterPrefab(new FishSausagesPrefab());
            PrefabManager.Instance.RegisterPrefab(new FishSoupPrefab());
        }

        private void registerObjects(object sender, EventArgs e)
        {
            string[] itemsArray = 
            {   "FishSausages",
                "FishSoup",
                "FishingRod",
                "FishingBait"
            };
            // Items
            ObjectManager.Instance.RegisterItem("FishSausages");
            ObjectManager.Instance.RegisterItem("FishSoup");
            // Recipes
            string recipesFileName = "recipes.json";
            if (customJsonRecipes.Value == true && File.Exists(Path.Combine(Paths.PluginPath, "FishFood", "custom_recipes.json")))
            {
                recipesFileName = "custom_recipes.json";
            }

            string jsonString = File.ReadAllText(Path.Combine(Paths.PluginPath, "FishFood", recipesFileName));
            RecipeList recipeList = LitJson.JsonMapper.ToObject<RecipeList>(jsonString);
            
            if (recipeList != null)
            {
                foreach (var recipe in recipeList.recipes)
                {
                    RecipeConfig recipeConfig = new RecipeConfig
                    {
                        Name = recipe.name,
                        Item = recipe.item,
                        Amount = recipe.amount,
                        CraftingStation = recipe.craftingStation,
                        MinStationLevel = recipe.minStationLevel
                    };
                    List<PieceRequirementConfig> list = new List<PieceRequirementConfig>();
                    foreach (var recipeReq in recipe.requirements)
                    {
                        list.Add(
                        new PieceRequirementConfig()
                        {
                            Item = recipeReq.item,
                            Amount = recipeReq.amount
                        });
                    }
                    recipeConfig.Requirements = list.ToArray();

                    if (itemsArray.Contains(recipeConfig.Item))
                    {
                        ObjectManager.Instance.RegisterRecipe(recipeConfig);
                    }
                }
            }
        }
    }
}
