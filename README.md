# ⚡ VFX Portfolio Project

A Unity-based Visual Effects portfolio showcasing interactive particle systems and post-processing effects.

## 🎨 Features

### Electric Arc Effect
- **Three-layer lightning system** with different colors and thicknesses:
  - 💙 **Blue Core** - Primary electric arc with realistic turbulence
  - 💛 **Yellow Layer** - Thick, energy-filled secondary discharge
  - 🤍 **White Branches** - Thin, branching lightning bolts
  
- **Dynamic animation**:
  - Perlin noise-based trajectory modulation
  - Non-uniform turbulence (stable at source, chaotic at endpoints)
  - Particle strip system for smooth lightning paths
  
- **Visual polish**:
  - HDR Bloom post-processing
  - Pulsating emission on magical staffs
  - Global Volume effects

### Interactive Demo System
- Filterable effect categories (Fire & Smoke, Magic & Energy, Sci-Fi, Simulation, Stylized)
- Real-time effect preview
- Fullscreen demo mode
- Dynamic prefab instantiation

## 🛠️ Technical Stack

- **Unity Version**: 2022.3+ (URP/HDRP)
- **VFX Graph**: GPU-accelerated particle systems
- **Post-Processing**: Volume-based effects (Bloom, Color Adjustments)
- **Scripting**: C# MonoBehaviour architecture

## 📁 Project Structure

```
Assets/
├── _Project/
│   ├── Scripts/           # Core logic (DemoListController, FilterController, etc.)
│   ├── VFX/              # Visual Effect Graph assets
│   ├── Prefabs/Demos/    # Effect prefabs
│   ├── Materials/        # Custom materials with Emission
│   └── Scenes/           # Main portfolio scene
└── Scenes/Materials/     # Scene files
```

## 🎮 Controls

- **Select Effect**: Click on demo name in the list
- **Play Demo**: Press "Play Demo" button
- **Filter**: Use category filters to browse effects
- **Back**: Return to portfolio menu

## 🔧 Key Scripts

- `DemoListController.cs` - Manages demo selection and playback
- `FilterController.cs` - Handles category filtering
- `DemoPreviewPanel.cs` - Displays effect information
- `EmissionPulse.cs` - Animated material emission

## ⚡ Electric Arc Technical Details

### VFX Graph Setup
- **Spawn**: Constant rate with particle capacity
- **Initialize**: Random lifetime, velocity, age
- **Update**: Bezier curve trajectory with Perlin Noise modulation
- **Output**: Particle Strip with HDR color gradients

### Turbulence Formula
```
Noise × (0.3 + stripProgress × 0.7)
```
Creates progressive turbulence from source to target.

### Color Palette
- **Blue Arc**: `(0.3, 0.8, 2.5)` HDR
- **Yellow Layer**: `(3.0, 2.5, 0.3)` HDR
- **White Branches**: `(5.0, 5.0, 5.0)` HDR

## 🎯 Future Enhancements

- [ ] Magic Impact effect
- [ ] Smoke & Dust simulation
- [ ] Portal effect
- [ ] 2D Splash effects
- [ ] Sound design integration
- [ ] Additional UI polish

## 📝 License

This is a portfolio project for demonstration purposes.

## 👤 Author

**Virginia Socotra**  
GitHub: [@VirginiaSocotra](https://github.com/VirginiaSocotra)

---

*Built with Unity & VFX Graph* ⚡✨
