// -----------------------------------------------------------------------
// <copyright file="DistanceChangedPatch.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace BetterSinkholes.Patches
{
#pragma warning disable SA1118
    using System.Collections.Generic;
    using System.Reflection.Emit;
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using HarmonyLib;
    using NorthwoodLib.Pools;
    using UnityEngine;
    using static HarmonyLib.AccessTools;

    /// <summary>
    /// Patches <see cref="SinkholeEnvironmentalHazard.DistanceChanged"/> to allow players to fall into the pocket dimension.
    /// </summary>
    [HarmonyPatch(typeof(SinkholeEnvironmentalHazard), nameof(SinkholeEnvironmentalHazard.DistanceChanged), typeof(ReferenceHub))]
    internal static class DistanceChangedPatch
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);

            int index = newInstructions.FindIndex(x => x.opcode == OpCodes.Bgt_Un_S) - 2;

            newInstructions.RemoveRange(index, 2);
            newInstructions.InsertRange(index, new[]
            {
                new CodeInstruction(OpCodes.Call, PropertyGetter(typeof(BetterSinkholes), nameof(BetterSinkholes.Instance))),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(BetterSinkholes), nameof(BetterSinkholes.Config))),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Config), nameof(Config.SlowDistance))),
            });

            var ply = generator.DeclareLocal(typeof(Player));

            index = newInstructions.FindLastIndex(x => x.opcode == OpCodes.Stloc_0) + 1;
            Label slowdownCheckLabel = generator.DefineLabel();
            newInstructions[index].WithLabels(slowdownCheckLabel);

            newInstructions.InsertRange(index, new[]
            {
                // Player ply = Player.Get(player);
                new CodeInstruction(OpCodes.Ldarg_1),
                new CodeInstruction(OpCodes.Call, Method(typeof(Player), nameof(Player.Get), new[] { typeof(ReferenceHub) })),
                new CodeInstruction(OpCodes.Stloc_S, ply.LocalIndex),

                // if (Vector3.Distance(ply.Position, this.transform.position) > BetterSinkholes.Instance.Config.TeleportDistance) goto slowdownCheckLabel;
                new CodeInstruction(OpCodes.Ldloc_S, ply.LocalIndex),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Player), nameof(Player.Position))),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Call, PropertyGetter(typeof(Component), nameof(Component.transform))),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Transform), nameof(Transform.position))),
                new CodeInstruction(OpCodes.Call, Method(typeof(Vector3), nameof(Vector3.Distance), new[] { typeof(Vector3), typeof(Vector3) })),
                new CodeInstruction(OpCodes.Call, PropertyGetter(typeof(BetterSinkholes), nameof(BetterSinkholes.Instance))),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(BetterSinkholes), nameof(BetterSinkholes.Config))),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Config), nameof(Config.TeleportDistance))),
                new CodeInstruction(OpCodes.Bgt_Un_S, slowdownCheckLabel),

                // ply.EnableEffect(EffectType.Corroding);
                new CodeInstruction(OpCodes.Ldloc_S, ply.LocalIndex),
                new CodeInstruction(OpCodes.Ldc_I4_6),
                new CodeInstruction(OpCodes.Ldc_R4, 0f),
                new CodeInstruction(OpCodes.Ldc_I4_0),
                new CodeInstruction(OpCodes.Callvirt, Method(typeof(Player), nameof(Player.EnableEffect), new[] { typeof(EffectType), typeof(float), typeof(bool) })),

                // ply.DisableEffect(EffectType.SinkHole);
                new CodeInstruction(OpCodes.Ldloc_S, ply.LocalIndex),
                new CodeInstruction(OpCodes.Ldc_I4_S, 19),
                new CodeInstruction(OpCodes.Callvirt, Method(typeof(Player), nameof(Player.DisableEffect), new[] { typeof(EffectType) })),

                // ply.Broadcast(BetterSinkholes.Instance.Config.TeleportMessage);
                new CodeInstruction(OpCodes.Ldloc_S, ply.LocalIndex),
                new CodeInstruction(OpCodes.Call, PropertyGetter(typeof(BetterSinkholes), nameof(BetterSinkholes.Instance))),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(BetterSinkholes), nameof(BetterSinkholes.Config))),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Config), nameof(Config.TeleportMessage))),
                new CodeInstruction(OpCodes.Ldc_I4_0),
                new CodeInstruction(OpCodes.Callvirt, Method(typeof(Player), nameof(Player.Broadcast), new[] { typeof(Broadcast), typeof(bool) })),

                // return;
                new CodeInstruction(OpCodes.Ret),
            });

            for (int i = 0; i < newInstructions.Count; i++)
                yield return newInstructions[i];

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }
    }
}
