# Game Design Document - Oleadas Roguelike
**Versión**: 0.2  
**Autor**: Marc Flores Blanes  
**Última actualización**: Mayo 2026

---

## 1. Concepto del juego

Roguelike de oleadas en arena fija con vista cenital (top-down). El jugador elige una clase RPG (mago, arquero o espadachín) y sobrevive oleadas de enemigos que escalan en dificultad. Entre oleadas, una tienda permite comprar objetos que mejoran al personaje y crean builds únicas. El juego es endless: no hay condición de victoria, el objetivo es llegar lo más lejos posible.

**Referencias principales**: Brotato, Vampire Survivors, Hades, The Binding of Isaac

---

## 2. Mecánicas Core

### Movimiento
- **Teclas**: WASD (8 direcciones)
- **Vista**: cenital (top-down)
- **Mapa**: arena fija, sin scroll

### Ataque
- **Teclas**: flechas direccionales (4 direcciones)
- **Estilo**: manual, estilo The Binding of Isaac
- **Compatible con mando**: stick izquierdo = movimiento, stick derecho = ataque (twin-stick)

### Habilidades activables
- **Tecla**: Q
- Cada clase tiene 1 habilidad especial activable

| Clase | Habilidad | Descripción |
|---|---|---|
| Mago | Meteorito | Cae un meteorito en área, daño alto |
| Arquero | Lluvia de flechas | Lluvia de flechas en área, múltiples impactos |
| Espadachín | Giro en área | Giro 360° estilo Garen (LoL), daño a todos los enemigos cercanos |

⚠️ *Sistema de cooldown/maná/cargas: pendiente de decidir*
⚠️ *Diseño detallado pendiente (v0.3)*

### Clases

| Clase | Tipo de ataque | Mecánica |
|---|---|---|
| Mago | Proyectil a distancia | Bola de energía, cooldown ~0.5s |
| Arquero | Proyectil a distancia | Flecha rápida, cooldown ~0.25s |
| Espadachín | Melee | Hitbox parented al jugador, duración ~0.3s, 1 hit por swing, cooldown ~0.4s |

**Notas de clase:**
- El espadachín tiene más vida base y daño en área para compensar el riesgo melee
- Múltiples proyectiles permitidos en pantalla simultáneamente
- El sprite gira hacia la dirección de ataque durante el swing

---

## 3. Sistema de Oleadas

### Estructura de una oleada
```
Oleada empieza
→ 30 segundos de combate
→ Spawn continuo de enemigos durante esos 30s
→ Al acabar los 30s: los enemigos restantes desaparecen
→ Pausa: se abre la tienda
→ El jugador compra objetos (sin timer, cierra cuando quiera)
→ Siguiente oleada
```

### Spawn de enemigos
- Aparecen desde posiciones aleatorias en el **perímetro de la arena**
- Velocidad de spawn escala con la oleada

### Escalado de dificultad (fórmulas)
```
Cantidad de enemigos = baseEnemigos + (oleadaActual * multiplicador)
Vida enemigo         = vidaBase * (1.1 ^ oleadaActual)     → +10% por oleada
Velocidad enemigo    = Min(velBase + oleadaActual * 0.1f, velMaxima)
Monedas drop         = monedaBase + (oleadaActual * incremento)
```

### Sistema de Bosses
- Cada **5 oleadas** aparece un boss del tier correspondiente
- Dentro de cada tier, el boss es **aleatorio**
- Esto garantiza runs distintas manteniendo progresión clara de dificultad

| Oleada | Tier | Pool de bosses |
|---|---|---|
| 5 | Tier 1 | 3 bosses (dificultad baja) |
| 10 | Tier 2 | 3 bosses (dificultad media) |
| 15 | Tier 3 | 3 bosses (dificultad alta) |
| 20+ | Tier 4 | 3 bosses (dificultad muy alta) |

⚠️ *Diseño detallado de cada boss pendiente (v0.3)*

### Dificultad progresiva
- Sin selector de dificultad (filosofía souls: el juego es lo que es)
- La dificultad escala automáticamente con cada oleada
- El jugador compensa con objetos de la tienda
- **Sistema de modificadores de dificultad voluntarios** (estilo Heat de Hades): desbloqueable tras completar X oleadas. Permite añadir handicaps voluntarios para jugadores que buscan más reto.
⚠️ *Pendiente de implementación (v1.0)*

---

## 4. Sistema de Drops

### Modelo de drop
- **Siempre dropea algo** al matar un enemigo
- Las probabilidades determinan QUÉ se dropea, no SI se dropea

### Tabla de drops (oleada 1)
| Item | Probabilidad | Valor |
|---|---|---|
| Moneda pequeña | 50% | 1 moneda |
| Moneda grande | 30% | 5 monedas |
| Poción de vida | 20% | Cura 10 PS |

### Escalado de drops
- El valor de las monedas y las pociones aumenta con la oleada
- Las probabilidades pueden ajustarse por tipo de enemigo
- Enemigos más difíciles = drops mejores
- Bosses siempre dropean recompensa especial (más monedas, objeto garantizado...)

### Recogida
- Los drops se recogen al pasar sobre ellos (colisión trigger)
- Las monedas se acumulan en el contador del jugador
- Las pociones se aplican instantáneamente al recogerlas

---

## 5. Sistema de Tienda

### Estructura
- Se abre entre cada oleada
- Muestra **4 objetos aleatorios** del pool de objetos desbloqueados
- Los objetos ofrecidos son **siempre distintos entre sí**
- Los objetos no comprados **desaparecen** al cerrar la tienda
- **El jugador cierra la tienda cuando quiera** (sin timer)

### Reroll
- **Primer reroll**: gratuito
- **Rerolls sucesivos**: coste creciente en monedas
```
Reroll 1: gratis
Reroll 2: 5 monedas
Reroll 3: 10 monedas
Reroll 4: 20 monedas
Reroll 5+: 40 monedas
```

### Sistema de objetos

#### Tipos de objetos
| Tipo | Apilable | Ejemplo |
|---|---|---|
| Stats | ✅ Sí | +10 daño, +20 vida máxima |
| Habilidades | ❌ No | Proyectil adicional, escudo temporal |

#### Desbloqueo de objetos (estilo Isaac)
- No todos los objetos están disponibles desde el inicio
- Se desbloquean progresivamente jugando
- Ejemplos de condiciones de desbloqueo:
  - Llegar a oleada X por primera vez
  - Matar X enemigos en total
  - Completar una run con cada clase
  - Encontrar ciertos objetos combinados
⚠️ *Sistema detallado de desbloqueos pendiente (v0.3)*

#### Disponibilidad por clase
- **Objetos universales**: cualquier clase puede comprarlos
- **Objetos específicos de clase**: solo aparecen al jugar esa clase
- Inspirado en el sistema de Hades/Brotato

#### Sistema de rarezas
| Rareza | Color | Probabilidad | Características |
|---|---|---|---|
| Común | Blanco/Gris | Alta | Solo stats básicos |
| Raro | Azul | Media | Stats + efecto menor |
| Épico | Morado | Baja | Stats + efecto especial |
| Legendario | Dorado | Muy baja | Stats + efecto transformador |

#### Sinergias (estilo Isaac/Hades)
- Ciertos objetos combinados activan efectos especiales
- Ejemplo: "Objeto A" + "Objeto B" = proyectiles en cadena
⚠️ *Pendiente de diseño detallado (v0.3-v0.4)*

---

## 6. Enemigos

### Enemigo básico (implementado)
| Stat | Valor base |
|---|---|
| Vida | 30 |
| Velocidad | 2 |
| Daño contacto | 10 |
| Drop | Modelo B (ver sección 4) |

### Enemigos planificados
| Tipo | Característica | Oleada de aparición |
|---|---|---|
| Básico melee | Persigue al jugador | Oleada 1 |
| Rápido | Alta velocidad, poca vida | Oleada 5 |
| Tanque | Mucha vida, lento | Oleada 5 |
| A distancia | Dispara proyectiles | Oleada 10 |
| Boss Tier 1 | Stats altos, mecánica especial | Oleada 5 |
| Boss Tier 2 | Stats muy altos | Oleada 10 |
| Boss Tier 3 | Stats extremos | Oleada 15 |
| Boss Tier 4 | Jefe final recurrente | Oleada 20+ |

### IA de enemigos
- **Persecución directa**: dirección normalizada hacia el jugador
- **Sin NavMesh**: física de Rigidbody2D para separación entre enemigos
- **Colisión**: Circle Collider 2D físico (no superposición) + hijo Hitbox trigger (recibir daño)

---

## 7. Progresión y Meta-progresión

### Dentro de la run
- Los objetos comprados en tienda persisten durante toda la run
- Al morir, se pierde todo (roguelike puro)
- Score = oleada máxima alcanzada

### Entre runs (meta-progresión)
- Guardar mejor marca por clase
- Guardar estadísticas globales (runs jugadas, enemigos matados, etc.)
- **Desbloqueo progresivo de objetos** (estilo Isaac): jugando se van desbloqueando objetos nuevos que aparecen en futuras runs
- Desbloquear clases jugando con otras
- Desbloquear sistema de modificadores de dificultad
⚠️ *Implementación: JSON local (v0.3)*

---

## 8. Arquitectura Técnica

### Stack
- **Motor**: Unity 6
- **Lenguaje**: C#
- **Control de versiones**: Git + GitHub
- **IDE**: Visual Studio 2026

### Persistencia
- **Catálogo** (objetos, enemigos, clases): ScriptableObjects
- **Progreso del jugador**: JSON local en `Application.persistentDataPath`
- **Run actual**: en memoria (Unity)
- **Rankings online / Wiki**: Supabase + API REST *(v1.0, aparcado)*

### Layers y colisiones
| Layer | Colisiona con |
|---|---|
| Player | Enemy |
| Enemy | Player, Enemy |
| PlayerProjectile | EnemyHitbox |
| EnemyHitbox | PlayerProjectile |

### Managers planificados
- **GameManager**: estado global del juego
- **WaveManager**: gestión de oleadas y escalado
- **EnemySpawner**: spawn de enemigos en el perímetro
- **ShopManager**: gestión de tienda entre oleadas
- **AudioManager**: música y SFX
- **UIManager**: HUD y menús
- **SaveManager**: guardado y carga JSON *(v0.3)*

---

## 9. Contenido planificado

### Objetos (mínimo 10, objetivo 20-25)
⚠️ *Pendiente de diseño detallado (KAN-9, v0.2)*

### Habilidades por clase (mínimo 4 por clase)
⚠️ *Pendiente de diseño detallado (KAN-10, v0.2)*

---

## 10. Pendiente / Por decidir

- [ ] Diseño detallado de los 20+ objetos y sus efectos
- [ ] Diseño de sinergias entre objetos
- [ ] Stats definitivos del espadachín y arquero
- [ ] Diseño detallado de los bosses por tier
- [ ] Sistema de cooldown/maná/cargas para habilidades activables
- [ ] ¿Hay objetos de curación en la tienda además de las pociones de drop?
- [ ] Diseño de los modificadores de dificultad voluntarios (Heat style)
- [ ] Condición de desbloqueo de los modificadores
- [ ] Sistema detallado de desbloqueo de objetos (condiciones específicas)
- [ ] Diseño del mapa/arena (tamaño, bordes, decoración)
- [ ] ¿Hay eventos especiales en oleadas concretas además del boss?
- [ ] Drop especial garantizado al matar un boss

---

## 11. Roadmap

| Versión | Contenido |
|---|---|
| v0.1 | Prototipo: movimiento, combate, enemigo básico, vida y daño ✅ |
| v0.2 | Sistemas core: oleadas, drops, tienda básica, 3 clases, más enemigos |
| v0.3 | Meta y pulido: guardado JSON, rarezas, sinergias básicas, menús, audio |
| v0.4 | Release: sprites finales, balanceo, mando, publicación itch.io |
| v1.0 | Online: rankings, wiki, modificadores de dificultad |

---

*Este documento es un trabajo en progreso. Se actualiza con cada decisión de diseño relevante.*
