using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace FirstMod1.Content.Items
{
    public class AAB : ModItem
    {
        // The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.FirstMod1.hjson' file.
        public override void SetDefaults()
        {
            Item.damage = 0; // No damage
            Item.DamageType = DamageClass.Generic; // Generic damage class
            Item.useTime = 15; // Fast use time
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.HoldUp; // Use style
            Item.consumable = true; // So it disappears after use
            Item.shoot = ProjectileID.None; // No projectile
            Item.value = Item.buyPrice(gold: 5); // Set item value
            Item.rare = ItemRarityID.Red; // Set item rarity
        }

        public override bool? UseItem(Player player)
        {
            MyModPlayer modPlayer = player.GetModPlayer<MyModPlayer>();
            if (modPlayer.DropChanceTimer > 0)
            {
                // Reset the drop chance timer
                modPlayer.DropChanceTimer = 0; 
                modPlayer.dropChanceDecreased = false; // Allow reapplication of drop chance decrease
                Main.NewText("Item drop chances reset to normal instantly!", 255, 240, 20);
            }
            else
            {
                Main.NewText("There are no active penalties to reset.", 255, 20, 20);
            }
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wood, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
