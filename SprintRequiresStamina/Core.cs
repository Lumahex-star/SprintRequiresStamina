using MelonLoader;
using HarmonyLib;
using UnityEngine;
using ScheduleOne.PlayerScripts;

[assembly: MelonInfo(typeof(SprintRequiresStamina.Core), "SprintRequiresStamina", "1.0.0", "Lumahex", null)]
[assembly: MelonGame("TVGS", "Schedule I")]

namespace SprintRequiresStamina
{
    public class Core : MelonMod
    {
        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("SprintRequiresStamina Initialized.");

            HarmonyInstance.PatchAll();
        }
    }

    [HarmonyPatch(typeof(PlayerMovement), "Move")]
    public class SprintPatch
    {
        public static void Prefix(PlayerMovement __instance)
        {
            if (__instance.CurrentStaminaReserve <= 0f)
            {
                Traverse.Create(__instance)
                .Field("sprintActive")
                .SetValue(false);
            }
        }


        public static void Postfix(PlayerMovement __instance)
        {
            if (__instance.IsSprinting)
            {
                __instance.ChangeStamina(-12.5f * Time.deltaTime);
            }
        }
    }
}