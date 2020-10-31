using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;

namespace BetterSinkholes
{
    public class Config : IConfig
    {
        [Description("Enable/disable BetterSinkholes")]
        public bool IsEnabled { get; set; } = true;

        [Description("Distance from the center of a sinkhole where you get teleported")]
        public float TeleportDistance { get; set; } = 0.7f;

        [Description("Distance from the center of a sinkhole where you start getting slowed")]
        public float SlowDistance { get; set; } = 1.15f;

        [Description("Message broadcasted to the player upon falling into a sinkhole. Default: blank")]
        public string TeleportMessage { get; set; } = "";

        [Description("Broadcasted message duration. Default: 0")]
        public ushort TeleportMessageDuration { get; set; } = 0;
    }

}
