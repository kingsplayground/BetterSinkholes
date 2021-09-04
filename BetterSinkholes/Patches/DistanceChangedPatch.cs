// -----------------------------------------------------------------------
// <copyright file="DistanceChangedPatch.cs" company="Thomasjosif">
// Copyright (c) Thomasjosif. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace BetterSinkholes.Patches
{
#pragma warning disable SA1313
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using HarmonyLib;

    /// <summary>
    /// Patches <see cref="SinkholeEnvironmentalHazard.DistanceChanged"/> to allow players to fall into the pocket dimension.
    /// </summary>
    [HarmonyPatch(typeof(SinkholeEnvironmentalHazard), nameof(SinkholeEnvironmentalHazard.DistanceChanged), typeof(ReferenceHub))]
    internal static class DistanceChangedPatch
    {
        private static bool Prefix(SinkholeEnvironmentalHazard __instance, ReferenceHub player)
        {
            Player ply = Player.Get(player);
            if (ply.IsScp && __instance.SCPImmune)
                return false;

            Config config = BetterSinkholes.Instance.Config;
            float distance = (ply.Position - __instance.transform.position).sqrMagnitude;
            if (distance <= config.TeleportDistance)
            {
                ply.EnableEffect(EffectType.Corroding);
                ply.DisableEffect(EffectType.SinkHole);
                ply.Broadcast(config.TeleportMessage);
                return false;
            }

            if (distance <= config.SlowDistance)
            {
                ply.EnableEffect(EffectType.SinkHole);
                return false;
            }

            ply.DisableEffect(EffectType.SinkHole);
            return false;
        }
    }
}
