// -----------------------------------------------------------------------
// <copyright file="BetterSinkholes.cs" company="Build">
// Copyright (c) Build. All rights reserved.
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
    public class BetterSinkholes : Plugin<Config>
    {
        private static readonly BetterSinkholes InstanceValue = new BetterSinkholes();
        private Harmony harmony;

        private BetterSinkholes()
        {
        }

        /// <summary>
        /// Gets the only existing instance of the <see cref="BetterSinkholes"/> class.
        /// </summary>
        public static BetterSinkholes Instance { get; } = InstanceValue;

        /// <inheritdoc />
        public override string Author { get; } = "Build, origially written by Blackruby";

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
            harmony = new Harmony($"build.betterSinkholes.{DateTime.UtcNow.Ticks}");
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
