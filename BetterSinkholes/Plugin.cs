// -----------------------------------------------------------------------
// <copyright file="Plugin.cs" company="Thomasjosif">
// Copyright (c) Thomasjosif. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace BetterSinkholes
{
    using System;
    using Exiled.API.Features;
    using HarmonyLib;

    /// <summary>
    /// The main plugin class.
    /// </summary>
    public class Plugin : Plugin<Config>
    {
        private Harmony harmony;

        /// <inheritdoc />
        public override string Author { get; } = "Thomasjosif, origially written by Blackruby";

        /// <inheritdoc />
        public override string Name { get; } = "BetterSinkholes";

        /// <inheritdoc />
        public override string Prefix { get; } = "BSH";

        /// <inheritdoc />
        public override Version RequiredExiledVersion { get; } = new Version(3, 0, 0);

        /// <inheritdoc />
        public override Version Version { get; } = new Version(4, 0, 0);

        /// <inheritdoc />
        public override void OnEnabled()
        {
            Config.FinalizeConfigs();
            harmony = new Harmony($"thomasjosif.betterSinkholes.{DateTime.UtcNow.Ticks}");
            harmony.PatchAll();
            base.OnEnabled();
        }

        /// <inheritdoc />
        public override void OnDisabled()
        {
            harmony.UnpatchAll();
            harmony = null;
            base.OnDisabled();
        }
    }
}
