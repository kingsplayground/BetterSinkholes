using Exiled.API.Features;
using HarmonyLib;

namespace BetterSinkholes
{
    public class BetterSinkholes : Plugin<Config>
    {

        public static int PatchCount = 0;
        public static Harmony harmony { set; get; }
        public static Config config;

        public override void OnEnabled()
        {
            config = Config;
            if (config.IsEnabled)
            {
                Log.Info("BetterSinkholes is currently enabled on this server. For more information about this plugin, please visit: https://github.com/rby-blackruby/BetterSinkholes \nCurrent version: 2.0\nAuthor: blackruby#9851");
                DoPatching();
            } 
            else
            {
                Log.Info("BetterSinkholes is currently disabled on this server. To enable it, set BetterSinkholes' IsEnabled config option to true in the server configs.");
            }
                
        }

        public override void OnDisabled()
        {
            harmony.UnpatchAll();
        }

        // Enables the modifications made to the sinkholes
        public static void DoPatching()
        {
            harmony = new Harmony($"blackruby.exiled.bettersinkholes{++PatchCount}");
            harmony.PatchAll();
        }
    }
}
