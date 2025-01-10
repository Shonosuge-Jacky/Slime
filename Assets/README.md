# SLIME Progress and Markdown

## Current Log
#### 6/10/2024
Successfully made a event component in GameModeSystem from GameManager. Now use codemonkey method deal with event in system.

#### 26/9/2024
Problem: SlimeComponent not initiaize correctly(See isAvailable)  ->  SlimeSystem not work
Try Method:
Initialize SlimeComponent in SpawnerSystem instead of Baker? (Check what baker is for in correct scenerio)

#### 16/9/2024
Try Method:
Change all data in DataCenter to static. <br/>
Reference: https://discussions.unity.com/t/what-should-i-be-aware-of-if-i-use-a-static-class/916207/6 <br/>
Check if all data in DataCenter only have ONE instance (ie. wont have to create a new data)

---
## ECS Structure

### SlimeAuthoring

SlimeComponent : Component

* SlimeState CurrSlimeState;
* float MoveSpeed;
* float TurnSpeed;
* float JumpForce;
* Emoji CurrEmoji;
* float Timer;

---

### SlimeSystem

ToDO:

InitialFloorStateJob -> 

if CurrSlimeState == SlimeState.Idle : IdleLogic

else : FloorStateLogic

(Do Once Only At Initialization)

---

### SlimeSpawnerAuthoring

SlimeSpawner : Component

* Entity Prefab
* int Count
* int MinX
* int MinY
* int MaxX

---

### SlimeSpawnerSystem

ToDO:

Spawn Slime within area randomly.

```
state.EntityManager.Instantiate()
```

Phase2:

```
state.Dependency = xxxJob.Schedule()
state.Dependency.Complete();
```

---

### GridDataAuthoring

GridData : Component

* NativeHashMap<float2, FloorState> Float2ToState

---

### GridDataSystem

ToDO:

Update Float2ToState

(Do Once Eveny Time Receive Event - DayNightEvent)

PlaceItem
(?: Int2ToFloorGameObject)

(Do Once Only At Initialization)

---

### FloorObjectAuthoring

FloorObject : Component

* UnityAction OnDayEvent

* UnityAction OnNightEvent

---

### FloorObjectSystem

Update Object

(Do Once Eveny Time Receive Event - DayNightEvent)
