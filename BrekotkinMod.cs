using ReLogic.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Terraria.ModLoader;

namespace BrekotkinMod
{
    public class BrekotkinMod : Mod
    {
        internal Asset<Texture2D> brekotkinTexture;
        internal Asset<SoundEffect> brekotkinSound;

        public override void Load()
        {
            brekotkinTexture = ModContent.Request<Texture2D>("BrekotkinMod/Assets/Images/BrekotkinFace");
            brekotkinSound = ModContent.Request<SoundEffect>("BrekotkinMod/Assets/Sounds/BrekotkinJumpscare");
        }

        public override void Unload()
        {
            brekotkinTexture = null;
            brekotkinSound = null;
        }
    }
}