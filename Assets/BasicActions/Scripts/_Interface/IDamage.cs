using UnityEngine;
public interface IDamage
{
    Component Component {get;}
    void Damage(float value);
    bool IsDead();
}
