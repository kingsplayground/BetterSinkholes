// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="Thomasjosif">
// Copyright (c) Thomasjosif. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace BetterSinkholes
{
    using System.ComponentModel;
    using Exiled.API.Features;
    using Exiled.API.Interfaces;

    /// <inheritdoc />
    public class Config : IConfig
    {
        private float slowDistance = 5.25f;
        private float teleportDistance = 2f;

        /// <inheritdoc />
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the distance from the center of a sinkhole where a player starts getting slowed.
        /// </summary>
        [Description("The distance from the center of a sinkhole where a player starts getting slowed.")]
        public float SlowDistance
        {
            get => slowDistance;
            set => slowDistance = value * value;
        }

        /// <summary>
        /// Gets or sets the distance from the center of a sinkhole where a player gets teleported.
        /// </summary>
        [Description("The distance from the center of a sinkhole where a player gets teleported.")]
        public float TeleportDistance
        {
            get => teleportDistance;
            set => teleportDistance = value * value;
        }

        /// <summary>
        /// Gets or sets the message to show when someone falls into the pocket dimension.
        /// </summary>
        [Description("The message to show when someone falls into the pocket dimension.")]
        public Broadcast TeleportMessage { get; set; } = new Broadcast
        {
            Content = "You've fallen into the pocket dimension!",
            Duration = 5,
            Show = false,
        };
    }
}
