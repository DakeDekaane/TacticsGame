using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState {
    public abstract void OnEnter(PlayerFMS player);
    public abstract void Update(PlayerFMS player);
    public abstract void OnExit(PlayerFMS player);
}
