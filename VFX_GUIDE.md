# 🎨 VFX Graph - Полное руководство для Electric Arc

## 📖 Оглавление
1. [Основы VFX Graph](#основы-vfx-graph)
2. [Структура эффекта](#структура-эффекта)
3. [Типы частиц](#типы-частиц)
4. [Создание Electric Arc](#создание-electric-arc)
5. [Оптимизация](#оптимизация)

---

## 🎯 Основы VFX Graph

### Что это?
**VFX Graph** - визуальный редактор частиц, работающий на **GPU** (очень быстро).

### Преимущества:
- ✅ Миллионы частиц без лагов
- ✅ Визуальное программирование (без кода)
- ✅ Real-time preview
- ✅ Гибкость и контроль

### Основные компоненты:
```
[Spawn] → [Initialize] → [Update] → [Output]
   ↓           ↓            ↓          ↓
Создание   Стартовые   Обновление  Рендеринг
частиц     значения    каждый кадр  на экран
```

---

## 🏗️ Структура эффекта

### 1️⃣ **SPAWN (Создание)**
**Задача:** Сколько частиц создавать и когда?

**Примеры:**
- `Constant Spawn Rate` - постоянно (10 частиц/сек)
- `Burst` - взрыв (100 частиц за раз)
- `Single Burst` - один раз при старте

**Для Electric Arc:**
```
Burst: 1 частица каждые 0.1 секунды
(создаём новую молнию 10 раз в секунду)
```

---

### 2️⃣ **INITIALIZE (Инициализация)**
**Задача:** Какие свойства у новой частицы?

**Основные параметры:**
- `Lifetime` - сколько живёт (секунды)
- `Position` - стартовая позиция
- `Velocity` - начальная скорость
- `Color` - цвет
- `Size` - размер

**Для Electric Arc:**
```
Lifetime: 0.1-0.2 сек (быстрая вспышка)
Position: (0,0,0) → (0,0,3) (линия 3 метра)
Color: Cyan/Blue (голубой)
```

---

### 3️⃣ **UPDATE (Обновление)**
**Задача:** Как частица меняется со временем?

**Популярные эффекты:**
- `Turbulence` - хаотичное движение
- `Gravity` - притяжение вниз
- `Color over Lifetime` - изменение цвета
- `Size over Lifetime` - изменение размера

**Для Electric Arc:**
```
Turbulence: Высокая (молния дёргается)
Color: Яркий → Темнеет к концу
```

---

### 4️⃣ **OUTPUT (Рендеринг)**
**Задача:** Как отрисовать частицу?

**Типы Output:**

#### 🔸 **Particle Quad** (квадраты)
- Обычные частицы (огонь, дым, искры)
- Billboard (всегда смотрит в камеру)
- Используют текстуры

#### 🔹 **Particle Strip** (линии) ⚡ *Для молний!*
- Соединяет точки в линию
- Идеально для молний, лучей, следов
- Может быть извилистой

#### 🔶 **Mesh** (3D модели)
- Рендерит 3D объект
- Для обломков, монет и т.д.

**Для Electric Arc используем:** `Particle Strip`

---

## ⚡ Создание Electric Arc - Пошагово

### 📋 **Шаг 1: Создать новый VFX Graph**
1. ПКМ в Project → `Create → Visual Effects → Visual Effect Graph`
2. Назовите: `ElectricArc`
3. Дважды кликните - откроется редактор

---

### 📋 **Шаг 2: Настроить SPAWN**
1. Найдите блок `Spawn`
2. Выберите режим: `Periodic Burst`
3. Настройки:
   ```
   Rate: 10 (10 молний в секунду)
   Count: 1 (по одной молнии)
   ```

---

### 📋 **Шаг 3: Настроить INITIALIZE**
Добавьте блоки:

#### 🔹 **Set Lifetime**
```
Mode: Random
Min: 0.08 сек
Max: 0.15 сек
(Короткая вспышка)
```

#### 🔹 **Set Position (Line)**
```
Start: (0, 0, 0)
End: (0, 0, 3)
(Молния идёт вперёд на 3 метра)
```

#### 🔹 **Set Color**
```
Color: Cyan (0, 1, 1) или (0.3, 0.7, 1)
(Электрический голубой)
```

#### 🔹 **Set Size**
```
Size: 0.05 - 0.1
(Толщина линии)
```

---

### 📋 **Шаг 4: Настроить UPDATE**

#### 🔹 **Turbulence**
```
Intensity: 2.0 - 5.0
Frequency: 3.0
(Молния извивается)
```

#### 🔹 **Color over Lifetime**
```
Gradient:
  0%: Bright Cyan (яркий)
  100%: Dark Blue (темнеет)
```

---

### 📋 **Шаг 5: Настроить OUTPUT**

#### 🔹 **Output Particle Strip**
```
Blend Mode: Additive (светится!)
Strip Count: 32 (точек в линии)
Material: Default Particle
```

#### 🔹 **Set UV Mode**
```
Mode: Stretched
(Текстура растянута вдоль линии)
```

---

## 🎨 Улучшения для Electric Arc

### ✨ **Добавить искры (Sparks)**

Создайте **второй System** в том же VFX:

#### Spawn
```
Burst: 20-30 частиц при старте
```

#### Initialize
```
Position: Random Sphere (радиус 0.5)
Velocity: Outward (от центра)
Lifetime: 0.2-0.4 сек
Size: 0.02-0.05
```

#### Output
```
Particle Quad (обычные квадраты)
Blend: Additive
Color: Yellow/White
```

---

### 💫 **Добавить свечение (Glow Core)**

Третий System:

#### Spawn
```
Single Burst: 1 частица
```

#### Initialize
```
Position: Center
Lifetime: 0.1 сек
Size: 0.3 (большая)
Color: Bright Cyan
```

#### Output
```
Particle Quad
Blend: Additive
Texture: Soft Glow (размытый круг)
```

---

## 🎯 Визуальная схема

```
ELECTRIC ARC SYSTEM
│
├─ System 1: Main Arc (Strip) ⚡
│  ├─ Spawn: 10/sec
│  ├─ Initialize: Line + Color + Turbulence
│  └─ Output: Strip (Additive)
│
├─ System 2: Sparks (Quads) ✨
│  ├─ Spawn: Burst 30
│  ├─ Initialize: Sphere + Velocity
│  └─ Output: Quad (Additive)
│
└─ System 3: Glow Core (Quad) 💫
   ├─ Spawn: Single
   ├─ Initialize: Center + Large
   └─ Output: Quad (Additive, Soft)
```

---

## 🔧 Ключевые параметры для настройки

### ⚡ **Для "агрессивной" молнии:**
```
Turbulence Intensity: 5.0+
Strip Count: 64
Lifetime: 0.05 сек
Color: Bright White/Blue
```

### 🌊 **Для "плавной" электрической дуги:**
```
Turbulence Intensity: 1.0-2.0
Strip Count: 16
Lifetime: 0.3 сек
Color: Purple/Blue gradient
```

---

## 📊 Оптимизация

### ❌ НЕ делайте:
- Слишком много частиц (>100k одновременно)
- Огромные текстуры (512x512 достаточно)
- Complex collisions (дорого)

### ✅ ДЕЛАЙТЕ:
- Используйте LOD (меньше частиц вдали)
- Additive blend (дешевле Alpha)
- Reuse textures (общие для эффектов)
- Cull когда не видно

---

## 🎓 Полезные ресурсы

### Официальные:
- Unity VFX Graph Documentation
- Unity Learn: VFX Graph tutorials

### Текстуры:
- Используйте из `Assets/Samples/.../Textures/`
- Generic/PerlinNoise01.png - отлично для молний

---

## 🚀 Следующие шаги

1. ✅ Откройте `ElectricArc.vfx` в Unity
2. ✅ Изучите структуру Lightning
3. ✅ Измените цвета на голубые
4. ✅ Добавьте искры
5. ✅ Экспериментируйте с Turbulence
6. ✅ Создайте префаб для демо

---

## 💡 Советы

### Workflow:
1. Начните с простого (один Strip)
2. Настройте основу (цвет, форма)
3. Добавляйте слои (искры, свечение)
4. Полируйте (timing, интенсивность)

### Дебаггинг:
- Если не видно → проверьте Output Blend Mode (Additive!)
- Если лагает → уменьшите Spawn Rate
- Если некрасиво → добавьте разнообразия (Random)

---

**Готовы начать? Откройте Unity и посмотрим на Lightning.vfx вместе!** ⚡
