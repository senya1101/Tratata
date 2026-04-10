using UnityEngine;

public class Crystal : Entity
{
protected override void Die()
{
    base.Die(); 
    GameManager.Instance.EndGame(); 
}
}