# QuickCrossbowReload

A lightweight Valheim mod that reduces the reload time of crossbows and prevents reloads from being interrupted by sprinting or jumping.

---

## Features

- 🔄 **Reduced Reload Time** – Faster reloads for crossbows
- 🏃‍♂️ **Movement-Friendly** – Reloading won't cancel when you sprint or jump
- ⚙️ **Fully Configurable** – Adjust the reload speed multiplier to your liking

---

## Configuration

Once the mod has loaded for the first time, you can edit its configuration at:

BepInEx/config/quickcrossbowreload.cfg


### Settings:

| Key                   | Description                                      | Default |
|-----------------------|--------------------------------------------------|---------|
| `ReloadSpeedMultiplier` | Multiplier for crossbow reload speed (`0.1` = very fast, `1.0` = normal) | `0.5`   |

---

## Installation

### Using Thunderstore Mod Manager:

1. Click **Install** from Thunderstore or the in-app manager.
2. Launch the game.

### Manual Installation:

1. Install [BepInEx](https://valheim.thunderstore.io/package/denikson/BepInExPack_Valheim/).
2. Download `QuickCrossbowReload.dll`.
3. Move the file into:
<Your Game Directory>\BepInEx\plugins\

---

## Compatibility

- ✅ Compatible with the latest Valheim version
- ✅ Safe to use in multiplayer (client-side only behavior)
- ❌ Does not affect bows or non-crossbow weapons

---

## Development

- Written in C# using Harmony and BepInEx
- Built against `assembly_valheim.dll` from the base Valheim install

---

## Source Code

[https://github.com/Clarkewha/QuickCrossbowReload](https://github.com/Clarkewha/QuickCrossbowReload)

---

## License

MIT License – feel free to use, modify, and redistribute with attribution.

