using System;
using BepInEx;
using JotunnLib.Managers;
using JotunnLib.Entities;

namespace FishFood
{
    [BepInPlugin("SSyl.FishFood", "FishFood", "1.0.0")]
    [BepInDependency("com.bepinex.plugins.jotunnlib")]
    public class FishFood : BaseUnityPlugin
    {
        private void Awake()
        {
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
            // Items
            ObjectManager.Instance.RegisterItem("FishSausages");
            ObjectManager.Instance.RegisterItem("FishSoup");
            // Recipes
            ObjectManager.Instance.RegisterRecipe(new RecipeConfig()
            {
                // Name of the recipe (defaults to "Recipe_YourItem")
                Name = "Recipe_FishSausages",

                // Name of the prefab for the crafted item
                Item = "FishSausages",

                Amount = 4,

                // Name of the prefab for the crafting station we wish to use
                CraftingStation = "piece_cauldron",

                // List of requirements to craft your item
                Requirements = new PieceRequirementConfig[]
                {
                    new PieceRequirementConfig()
                    {
                        // Prefab name of requirement
                        Item = "FishRaw",

                        // Amount required
                        Amount = 2
                    },
                    new PieceRequirementConfig()
                    {
                        // Prefab name of requirement
                        Item = "Dandelion",

                        // Amount required
                        Amount = 1
                    },
                    new PieceRequirementConfig()
                    {
                        // Prefab name of requirement
                        Item = "Thistle",

                        // Amount required
                        Amount = 3
                    },
                    new PieceRequirementConfig()
                    {
                        // Prefab name of requirement
                        Item = "Entrails",

                        // Amount required
                        Amount = 2
                    }
                }
            });

            ObjectManager.Instance.RegisterRecipe(new RecipeConfig()
            {
                Name = "Recipe_FishSoup",
                Item = "FishSoup",
                CraftingStation = "piece_cauldron",

                Requirements = new PieceRequirementConfig[]
                {
                    new PieceRequirementConfig()
                    {
                        Item = "FishRaw",
                        Amount = 3
                    },
                    new PieceRequirementConfig()
                    {
                        Item = "Carrot",
                        Amount = 2
                    },
                    new PieceRequirementConfig()
                    {
                        Item = "Turnip",
                        Amount = 1
                    },
                    new PieceRequirementConfig()
                    {
                        Item = "Honey",
                        Amount = 2
                    }
                }
            });

            ObjectManager.Instance.RegisterRecipe(new RecipeConfig()
            {
                Name = "Recipe_FishingRod",
                Item = "FishingRod",
                CraftingStation = "piece_workbench",

                Requirements = new PieceRequirementConfig[]
                {
                    new PieceRequirementConfig()
                    {
                        Item = "RoundLog",
                        Amount = 5
                    },
                    new PieceRequirementConfig()
                    {
                        Item = "FineWood",
                        Amount = 10
                    }
                }
            });

            ObjectManager.Instance.RegisterRecipe(new RecipeConfig()
            {
                Name = "Recipe_FishingBait",
                Item = "FishingBait",
                Amount = 20,

                Requirements = new PieceRequirementConfig[]
                {
                    new PieceRequirementConfig()
                    {
                        Item = "Resin",
                        Amount = 4
                    },
                    new PieceRequirementConfig()
                    {
                        Item = "NeckTail",
                        Amount = 1
                    }
                }
            });
        }
    }
}
