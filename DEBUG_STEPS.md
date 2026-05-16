# 🔧 Electric Arc - Не вижу эффект? Решение!

## ✅ ШАГ 1: Проверьте Inspector

**В правой панели (Inspector) найдите:**

```
┌─────────────────────────────┐
│ Visual Effect               │
│                             │
│ Asset Template:             │
│   [ElectricArc]             │
│                             │
│ Initial Event Name:         │
│   [OnPlay]                  │
│                             │
│ Renderer:                   │
│   ✓ Light Probes: Off       │
│   ✓ Reflection Probes: Off  │
│                             │
│ Properties:                 │
│   ...                       │
│                             │
│ [Play] [Stop] [Reset]       │ ← ВОТ ЭТО!
└─────────────────────────────┘
```

**НАЖМИТЕ КНОПКУ [Play]!** ▶️

---

## ✅ ШАГ 2: Запустите Play Mode

Если кнопка Play в Inspector не помогла:

1. В верхней панели Unity найдите большую кнопку **Play ▶️**
2. Нажмите её
3. Эффект должен запуститься!

---

## ✅ ШАГ 3: Проверьте позицию камеры

Эффект может быть, но камера не туда смотрит!

### В Scene View:
1. Найдите объект **ElectricArc** в Hierarchy (слева)
2. **Дважды кликните** на него
3. Камера должна сфокусироваться на эффекте

### Или:
1. Нажмите `F` когда ElectricArc выбран
2. Это "Frame Selected" - центрирует камеру

---

## ✅ ШАГ 4: Проверьте что эффект в сцене

### В Hierarchy (слева) должно быть:
```
▼ VFX_Portfolio_Project1
  ├─ Main Camera
  ├─ Canvas
  ├─ EventSystem
  ├─ DontDestroyOnLoad
  └─ ... другие объекты
```

**Нужно добавить префаб:**

1. Project → `Assets/_Project/Prefabs/Demos/`
2. Найдите `Demo_ElectricArc_Root.prefab`
3. **Drag & Drop** в Hierarchy
4. Появится в списке!

---

## 🎯 Быстрое решение - С НУЛЯ:

### 1. Очистите сцену (если нужно)
В Hierarchy → ПКМ на старом ElectricArc → Delete

### 2. Добавьте префаб заново
1. Project → `Assets/_Project/Prefabs/Demos/`
2. `Demo_ElectricArc_Root.prefab`
3. Drag в **Hierarchy**

### 3. Настройте позицию
В Inspector:
```
Transform:
  Position: X: 0, Y: 0, Z: 0
  Rotation: X: 0, Y: 0, Z: 0
  Scale: X: 1, Y: 1, Z: 1
```

### 4. Настройте камеру
Выделите **Main Camera** в Hierarchy:
```
Transform:
  Position: X: 0, Y: 1, Z: -3
  Rotation: X: 0, Y: 0, Z: 0
```

### 5. Нажмите Play ▶️

---

## 🔴 Распространённые проблемы:

### ❌ "Вижу только оси координат (RGB)"
✅ Эффект не запущен - нажмите Play

### ❌ "Ничего не происходит"
✅ Проверьте Console (внизу) на ошибки

### ❌ "Эффект есть, но очень маленький"
✅ Увеличьте Scale в Transform:
```
Scale: X: 5, Y: 5, Z: 5
```

### ❌ "Эффект белый/неправильный цвет"
✅ Это нормально! Lightning изначально белый
✅ Нужно открыть VFX Graph и изменить цвет

---

## 📸 Что вы ДОЛЖНЫ увидеть:

### В Scene View (при Play):
```
       /|
      / |
    /   |  ← Белые/фиолетовые вспышки
  /     |     извилистой линии
/       |
```

Молния идёт вверх, быстро вспыхивает и гаснет

---

## 🎮 Следующие шаги:

После того как увидите эффект:

1. ✅ Остановите Play Mode
2. ✅ Откройте `ElectricArc.vfx` (двойной клик)
3. ✅ Измените цвет на синий
4. ✅ Сохраните (Ctrl+S)
5. ✅ Запустите Play снова

---

## 💡 Совет:

Если всё равно не видите - отправьте скриншот:
- Hierarchy (слева)
- Inspector (справа)
- Console (внизу)

Я помогу найти проблему!
