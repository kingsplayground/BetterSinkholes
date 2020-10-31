# BetterSinkholes Redux

BetterSinkholes is a plugin that makes **sinkhole environmental hazards** (found in Light Containment Zone - IX Intersections) more realistic and similar to SCP: Containment Breach. With the use of this plugin, players who walk into sinkholes fall into the pocket dimension *and may never return*. The original plugin was made by [Blackruby](https://github.com/rby-blackruby/BetterSinkholes) but was unfortunately discontinuted by him. This repo will be supported by myself (Thomasjosif).

## Requirements
- This plugin uses [EXILED](https://github.com/galaxy119/EXILED/).
- Make sure the config option in `config_gameplay.txt` called `sinkhole_spawn_chance` is set to higher than 0.

## Releases
You can find the latest release [here](https://github.com/kingsplayground/BetterSinkholes/releases).

## Configs
| Config option | Value type | Default value | Description |
| --- | --- | --- | --- |
| `IsEnabled` | bool | true | Enables the BetterSinkholes plugin. Set it to false to disable it. |
| `SlowDistance` | float | 1.15f | Distance a the sinkhole where it starts slowing. Don't set it higher than 1.15! |
| `TeleportDistance` | float | 0.7f | Distance from a sinkhole where it teleports you to the pocket dimension. Set it to higher than 0!|
| `TeleportMessage` | string | '' | Set it to '' to disable sinkhole teleport message. Can use Unity's RichText. |
| `TeleportMessageDuration` | ushort | 0 | Duration of the sinkhole teleport message, when teleport message is not null. |
