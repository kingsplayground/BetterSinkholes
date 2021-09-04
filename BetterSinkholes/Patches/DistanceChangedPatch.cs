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
    using UnityEngine;

    /// <summary>
    /// Patches <see cref="SinkholeEnvironmentalHazard.DistanceChanged"/> to allow players to fall into the pocket dimension.
    /// </summary>
    [HarmonyPatch(typeof(SinkholeEnvironmentalHazard), nameof(SinkholeEnvironmentalHazard.DistanceChanged), typeof(ReferenceHub))]
    internal static class DistanceChangedPatch
    {
        /// <inheritdoc cref="Config.TeleportDistance" />
        public static float TeleportDistance { get; set; }

        /// <inheritdoc cref="Config.SlowDistance" />
        public static float SlowDistance { get; set; }

        /// <inheritdoc cref="Config.TeleportMessage" />
        public static Broadcast TeleportMessage { get; set; }

        private static bool Prefix(SinkholeEnvironmentalHazard __instance, ReferenceHub player)
        {
            Player ply = Player.Get(player);
            if (ply.IsScp && __instance.SCPImmune)
                return false;

            float distance = (ply.Position - __instance.transform.position).sqrMagnitude;
            if (distance <= TeleportDistance)
            {
                ply.Position = Vector3.down * -1998.5f;
                ply.EnableEffect(EffectType.Corroding);
                ply.DisableEffect(EffectType.SinkHole);
                ply.Broadcast(TeleportMessage);
                return false;
            }

            if (distance <= SlowDistance)
            {
                ply.EnableEffect(EffectType.SinkHole);
                return false;
            }

            ply.DisableEffect(EffectType.SinkHole);
            return false;
        }
    }
}
