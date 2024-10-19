using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace FirstMod1.Content.Items
{
    public class AAA : ModItem
    {
        private bool elmoishere = false;
        private bool dropChanceDecreased = false;
        
        public override void SetDefaults()
        {
            Item.damage = 0;
            Item.DamageType = DamageClass.Generic;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
            Item.shoot = ProjectileID.BloodRain;
            Item.value = Item.buyPrice(platinum: 10);
            Item.rare = ItemRarityID.Red;
        }

        public override bool? UseItem(Player player)
        {
            if (!elmoishere)
            {
                Main.NewText("Damage Buff Is Active... However.....", 255, 36, 0);
                // player.GetModPlayer<MyModPlayer>().ElmoJumpy = 1;
                player.GetModPlayer<MyModPlayer>().damageBuffActive = true;
            }

            if (!dropChanceDecreased)
            {
                Main.NewText("Item Drop Chances Are Now Set To 20% Due To The Penalty (We Warned You)", 255, 240, 20);
                player.GetModPlayer<MyModPlayer>().DropChanceTimer = 300000000;
                dropChanceDecreased = true;
            }
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DirtBlock, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }

    public class MyModPlayer : ModPlayer
    {
        // public int ElmoJumpy;
        public int DropChanceTimer;
        public bool dropChanceDecreased;

        // public bool elmoMessageSent;
        public bool damageBuffActive;
        // Add a field to store the texture
        private Texture2D elmoTexture;

        /*public override void Load()
        {
            // Load the texture and handle potential errors
            try
            {
                elmoTexture = ModContent.Request<Texture2D>("AAC").Value;
            }
            catch (Exception e)
            {
                if (!elmoMessageSent)
                {
                    Main.NewText("Failed to load Elmo texture: " + e.Message, 255, 0, 0);
                    elmoMessageSent = true;
                }
            }
        }*/

        public override void UpdateDead()
        {
            // ElmoJumpy = 0;
            DropChanceTimer = 0;
            dropChanceDecreased = false;
        }

        public override void PreUpdate()
        {
            // if (ElmoJumpy == 1)
            // {
            //     // Logic to display the Elmo image goes here, handled in the PostUpdate method.
            // }

            if (DropChanceTimer > 0)
            {
                DropChanceTimer--;

                if (DropChanceTimer % 30 == 0)
                {
                    foreach (NPC npc in Main.npc)
                    {
                        if (npc.active && npc.type != NPCID.GreenSlime)
                        {
                            npc.value *= 0.5f;
                        }
                    }
                }

                if (DropChanceTimer == 0)
                {
                    foreach (NPC npc in Main.npc)
                    {
                        if (npc.active && npc.type != NPCID.GreenSlime)
                        {
                            npc.value *= 2;
                        }
                    }
                    Main.NewText("Item drop chances reset to normal.", 255, 20, 20);
                    Main.NewText("Damage Buff Has Been Removed.", 255, 20, 20);
                    dropChanceDecreased = false;
                    damageBuffActive = false; 
                }
            }
        }

        // public override void PostUpdate()
        // {
        //     if (ElmoJumpy == 1)
        //     {
        //         // Call the DrawElmo method to draw the image on the screen
        //         DrawElmo(Main.spriteBatch);
        //     }
        // }

        /*private void DrawElmo(SpriteBatch spriteBatch)
        {
            // Check if the texture is loaded before drawing
            if (elmoTexture != null)
            {
                Vector2 position = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);

                // Draw the Elmo texture at the player's position
                spriteBatch.Draw(elmoTexture, position, null, Color.White, 0f, new Vector2(elmoTexture.Width / 2, elmoTexture.Height / 2), 1f, SpriteEffects.None, 0f);
            }
            else
            {
                if (elmoTexture != null)
                {
                    Main.NewText("Elmo texture is not loaded!", 255, 0, 0);
                    elmoMessageSent = true;
                }
            }
        }*/
        public override void ModifyWeaponDamage(Item item, ref StatModifier damage) {
            if (damageBuffActive)
            {
                damage *= 1.1f;
            }
        }
    }
}
