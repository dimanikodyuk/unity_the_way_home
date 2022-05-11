**Жанр: ** Платформер

**Управление: ** WASD - передвижение, SPACE - прыжок, ESCAPE - паза/резюме


UI
-----------------------------------------------
ScoreManager
- CurrScore
- void AddScore(score)
- Reset()
- ScorePointChangeEvent

Lives
- CurrentLives
- void AddLives
- Reset()
- LiveChangeEvent
------------------------------------------------

Player
- Speed
- JumpForce
- DoubleJumpForce
- SideAcceleration
- Lives

- Controlling - Motion/Jumping/Siding/TopAttack

- OnTriggerEnter - Coin/Fruits(lives)/Obstacles/Traps

class Collectable - base
- OnItemCollectedEvent

enum CollectableType 
{
	Coin
, 	Fruits
}

CoinCollectable : Collectable

FruitCollectable : Collectable



class Enemy
enum EnemyType
{
	Bee
,	Chameleon
,	Chiken
,	FatBird
,	Rino
,	RockHead
,	Spike
,	Turtle
}

class Obstacles
enum ObstaclesType
{
	Box
,	Brown
,	FallingPlatform
,	FirePlatform
,	Saw
}

BeeEnemy : Enemy
- Health
- Speed
- Health
- TriggerMove
- Attacking

Chameleon : Enemy
- Health
- Speed
- Path of movement
- Attacking

Chiken : Enemy
- Health
- Movement
- Attacking

FatBird : Enemy
- Movement

Rino : Enemy
- Health
- TriggerMovement
- Attacking

RockHead : Enemy
- Speed
- Path of movement

Spike : Enemy
- Speed
- TriggerMovement
- Attacking

Turtle : Enemy
- Attacking

DataController
- void SaveData
- void LoadData
- void ResetData
