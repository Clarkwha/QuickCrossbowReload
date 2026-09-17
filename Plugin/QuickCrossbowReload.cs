using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;
using System.Reflection;

[BepInPlugin("quickcrossbowreload", "QuickCrossbowReload", "1.0.0")]
public class QuickCrossbowReload : BaseUnityPlugin
{
    private static ConfigEntry<float> reloadSpeedMultiplier;

    private void Awake()
    {
        reloadSpeedMultiplier = Config.Bind(
            "General",
            "ReloadSpeedMultiplier",
            0.5f,
            new ConfigDescription("Multiplier for crossbow reload speed (0.1 = faster, 1.0 = normal)", new AcceptableValueRange<float>(0.1f, 2.0f))
        );

        Harmony harmony = new Harmony("quickcrossbowreload");
        harmony.PatchAll();
        Logger.LogInfo($"QuickCrossbowReload loaded. Reload multiplier set to {reloadSpeedMultiplier.Value}");
    }

    // Patch to reduce crossbow reload time
    [HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetWeaponLoadingTime))]
    public static class Patch_GetWeaponLoadingTime
    {
        public static void Postfix(ItemDrop.ItemData __instance, ref float __result)
        {
            if (__instance.m_shared.m_skillType == Skills.SkillType.Crossbows)
            {
                __result *= reloadSpeedMultiplier.Value;
            }
        }
    }

    // Patch CancelReloadAction using reflection (in case it's private or internal)
    [HarmonyPatch]
    public static class Patch_CancelReloadAction
    {
        static MethodBase TargetMethod()
        {
            return AccessTools.Method("Player:CancelReloadAction");
        }

        public static bool Prefix(Player __instance)
        {
            var weapon = __instance.GetCurrentWeapon();
            if (weapon != null && weapon.m_shared.m_skillType == Skills.SkillType.Crossbows)
            {
                // Prevent reload cancellation for crossbows
                return false;
            }
            return true;
        }
    }

    // Patch ResetLoadedWeapon to prevent action queue from being cleared
    [HarmonyPatch(typeof(Player), nameof(Player.ResetLoadedWeapon))]
    public static class Patch_ResetLoadedWeapon
    {
        public static bool Prefix(Player __instance)
        {
            var weapon = __instance.GetCurrentWeapon();
            if (weapon != null && weapon.m_shared.m_skillType == Skills.SkillType.Crossbows)
            {
                // Prevent reload from being removed
                return false;
            }
            return true;
        }
    }
}
