using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;

namespace BrekotkinMod
{
    public class BrekotkinSystem : ModSystem
    {
        private int _displayTimer = 0;
        private bool _isShowing = false;

        public override void PostUpdateEverything()
        {
            var config = ModContent.GetInstance<BrekotkinConfig>();

            if (Main.gamePaused || !Main.hasFocus) return;

            if (_isShowing)
            {
                _displayTimer--;
                if (_displayTimer <= 0)
                {
                    _isShowing = false;
                }
                return;
            }

            // Проверка шанса
            if (Main.rand.NextBool(config.ChanceDenominator))
            {
                TriggerJumpscare(config);
            }
        }

        private void TriggerJumpscare(BrekotkinConfig config)
        {
            _isShowing = true;
            _displayTimer = config.DisplayDuration;

            // Воспроизводим звук сразу, громкость регулируется ползунком
            var mod = ModContent.GetInstance<BrekotkinMod>();
            if (mod.brekotkinSound != null && mod.brekotkinSound.IsLoaded)
            {
                mod.brekotkinSound.Value.Play(config.SoundVolume, 0.0f, 0.0f);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            // Вставляем наш слой САМЫМ ПЕРВЫМ (индекс 0)
            // Это гарантирует, что все остальные элементы UI (здоровье, мана, карта и т.д.)
            // будут рисоваться ПОВЕРХ нашего изображения
            layers.Insert(0, new LegacyGameInterfaceLayer(
                "BrekotkinMod: Jumpscare Layer",
                delegate
                {
                    if (_isShowing)
                    {
                        DrawJumpscare();
                    }
                    return true;
                },
                // InterfaceScaleType.UI означает, что изображение будет масштабироваться вместе с интерфейсом
                // Но поскольку слой в самом низу, всё остальное будет поверх него
                InterfaceScaleType.UI)
            );
        }

        private void DrawJumpscare()
        {
            var mod = ModContent.GetInstance<BrekotkinMod>();
            if (mod.brekotkinTexture == null || !mod.brekotkinTexture.IsLoaded) return;

            SpriteBatch spriteBatch = Main.spriteBatch;
            Texture2D texture = mod.brekotkinTexture.Value;

            // Центрируем изображение на экране
            Vector2 position = new Vector2(Main.screenWidth / 2f, Main.screenHeight / 2f);
            Vector2 origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
            
            // Масштабируем изображение, чтобы оно покрывало весь экран
            float scale = Math.Max(Main.screenWidth / (float)texture.Width, Main.screenHeight / (float)texture.Height);
            scale *= 1.1f; // Небольшой запас, чтобы не было видно краев

            // Рисуем изображение
            spriteBatch.Draw(texture, position, null, Color.White, 0f, origin, scale, SpriteEffects.None, 0f);
        }

        public bool IsShowing => _isShowing;
    }
}