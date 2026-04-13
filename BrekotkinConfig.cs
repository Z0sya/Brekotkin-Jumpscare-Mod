using Terraria.ModLoader.Config;
using System.ComponentModel;

namespace BrekotkinMod
{
    [Label("Настройки Дмитрия Брекоткина")]
    public class BrekotkinConfig : ModConfig
    {
        // ConfigScope.ClientSide означает, что настройки сохраняются у каждого игрока отдельно
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Label("Шанс появления (1 из X тиков)")]
        [Tooltip("Чем меньше число, тем чаще появляется Дмитрий. \nРекомендуется: 10000 (очень редко). \nДля теста поставьте 100.")]
        [Range(10, 100000)]
        [Increment(10)]
        [DefaultValue(10000)]
        public int ChanceDenominator;

        [Label("Длительность показа (в тиках)")]
        [Tooltip("60 тиков = 1 секунда.")]
        [Range(10, 300)]
        [DefaultValue(120)]
        public int DisplayDuration;
        
        [Label("Громкость звука")]
        [Tooltip("От 0.0 (тихо/выкл) до 1.0 (максимально громко).")]
        [Range(0.0f, 1.0f)]
        [Increment(0.1f)]
        [DefaultValue(1.0f)]
        public float SoundVolume;
    }
}