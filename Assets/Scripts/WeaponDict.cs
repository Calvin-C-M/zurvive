using System.Collections.Generic;

public class WeaponDict
{
    public readonly Dictionary<string, Dictionary<string, float>> attribute = new Dictionary<string, Dictionary<string, float>>()
        {
            {"rifle", new Dictionary<string, float>()
            {
                {"damage", 3.0f},
                {"range", 15.0f},
                {"fireRate", 0.1f},
                {"initAmmo", 30f}
            }},

            {"sword", new Dictionary<string, float>()
            {
                {"damage", 5.0f},
                {"range", 5.0f},
                {"fireRate", 0.5f},
                {"initAmmo", 1f}
            }},

            {"pistol", new Dictionary<string, float>()
            {
                {"damage", 2.0f},
                {"range", 10.0f},
                {"fireRate", 0.4f},
                {"initAmmo", 15f}
            }},
        };
}
