using System;

[Serializable]
[Flags] public enum Labels
{
    None = 0,
    Poop = 1 << 0,
    Corn = 1 << 1,
    Character = 1 << 2,
    Bullet = 1 << 3,
    Hostile = 1 << 4,
    SplashEffect = 1 << 5,
    Ground = 1 << 6,
    Explosion = 1 << 7,
    Player = 1 << 8,
    Life = 1 << 9
}
