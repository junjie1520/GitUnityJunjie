# Roluna VS Champi

Proyecto 02 – Platformer

Equipo: JunJie, Christian, Nuria

## Cómo jugar

**La escena de inicio del juego es `Assets/Scenes/Inicio.unity`** (menú principal).
Desde ahí: **Iniciar** carga `Main` (la partida) y **Salir** cierra el juego.
Al terminar la partida se carga `End`, que muestra el resultado y permite volver a `Inicio`.

```
Inicio --> Main --> End --> Inicio
```

Hay que sobrevivir **150 segundos** a las oleadas de Champis. Cada Champi eliminado suma un punto.
La partida termina al acabar el tiempo (victoria) o al quedarse Roluna sin vida (derrota).

### Controles (teclado)

| Tecla | Acción |
|---|---|
| A / D (o flechas) | Moverse |
| W (o flecha arriba) | Saltar |
| S (o flecha abajo), en el aire | Caída rápida |
| Clic izquierdo / Enter | Disparar |

## Requisitos mínimos

| Requisito | Dónde |
|---|---|
| Mensajería con Observers | `ChampiBehaviour.OnMuerte` → `ChampiSpawner.OnChampiMuerto` → `GameManager` (puntos) y `SpawnerPowerUps` (drop). `GameManager.OnSumarPuntos` / `OnPasaSegundo` y `RolunaBehaviour.OnCambioVida` → `UIManager` |
| Control por teclado | `RolunaBehaviour` (Input System: `InputActionReference` de movimiento y disparo) |
| UI de puntuación / power-ups sin `Update` | `UIManager`: solo se actualiza al recibir eventos (puntos, tiempo, vida) |
| Corrutinas | `PlayerManager.CorutinaPowerUp` (duración de power-ups), `ChampiBehaviour.Attack` (ataque), `BalaBehaviour` (autodestrucción a los 3 s) |
| Layers, triggers, colliders | Layers `Player`, `Enemy`, `Bullet`, `Dead`, `PowerUp`. Trigger de detección de Champi (radio 5), trigger de los power-ups, `Physics2D.IgnoreCollision` con los Champis muertos (`ChampiBehaviour`) |
| Lógica y estructura | Managers separados (`GameManager`, `PlayerManager`, `UIManager`, `MenuManager`, `EndGameManager`), spawners y ScriptableObjects para los datos de los power-ups |

## Extras implementados

### Tilemap
- El nivel de `Main` está hecho con **Tilemap** (2 capas), con `TilemapCollider2D` + `CompositeCollider2D` para las colisiones del terreno.

### Animaciones
- **Roluna** — `RolunaBehaviour.ActualizarAnimacion()`: elige cada frame entre Idle / Run / Jump y sus variantes de disparo (`*Shoot`) según el estado real (en el aire, corriendo, disparando). Al pasar de Run a RunShoot mantiene el frame del ciclo para que no salte.
- **Champi** — `ChampiBehaviour`: Idle, Run, Attack y Die.

### IA de los enemigos
- `ChampiBehaviour`:
  - Detecta a Roluna con un trigger de radio 5 y la persigue.
  - Si se queda atascado contra un obstáculo durante 2 s, salta para superarlo.
  - Salta si Roluna está encima y cerca.
  - Al tocarla, ataca con una corrutina.

### Físicas
- Gravedad variable: más gravedad al caer que al subir (`RolunaBehaviour` y `ChampiBehaviour`, `FixedUpdate`), para un salto con más peso.
- Caída rápida con S (`RolunaBehaviour.Bajar`).
- Detección de suelo por puntos de contacto y normales (`RolunaBehaviour.ComprobarSuelo`, `ChampiBehaviour.OnCollisionStay2D`).
- Material físico sin fricción para que los Champis no se queden pegados a las paredes.

### Power-ups con ScriptableObjects
- `SOPowerUp`: cada power-up es un asset con sus estadísticas (vida, velocidad, salto, cadencia de disparo y duración).
- `PlayerManager.AplicarPowerUp()`: una sola función que suma las estadísticas y, si el power-up es temporal, las resta al acabar con una corrutina.
- `SpawnerPowerUps`: cuando muere un Champi, hay un 25 % de probabilidad de que suelte un power-up en su posición.

### Dificultad progresiva
- `GameManager.UnSegundo()`: oleadas de 2 Champis cada 5 s; a partir de 50 s restantes, 3 cada 3 s; en los últimos 20 s, 5 cada 2 s.

### Menús y pantalla final
- `MenuManager`: menú de inicio (iniciar / salir; en el editor, "Salir" detiene el Play).
- `EndGameManager`: pantalla final con victoria o derrota, rango según la puntuación (C, B, A, A+, S, S+) y comentario.
- `GameManager` como **singleton persistente** (`DontDestroyOnLoad`) para pasar la puntuación y el tiempo restante a la escena `End`; se destruye al volver a `Inicio` y se reemplaza al empezar una partida nueva.

### Cámara
- Cámara hija de Roluna que se contra-gira al darse la vuelta el personaje, para que el giro no se note (`RolunaBehaviour.FixedUpdate`).

### Otros
- Cadencia de disparo con cooldown (`RolunaBehaviour.Disparar`).
- HUD con tiempo en formato `m:ss` (`UIManager.ActualizarTiempo`).

## Créditos

- Desarrollo: JunJie, Christian y Nuria.
- Con ayuda de **Claude** para la super imagen del juego y script de Roluna.
