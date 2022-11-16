using System;
using System.Collections.Generic;
using System.Text;

class Soldier
{   
    public virtual string Attack()
    {
        return "Shoot from gun";
    }
}

class CoolerSoldier : Soldier
{
    public override string Attack()
    {
        return "Shoot from gun and throw a grenade";
    }
}

class ManInBlack : Soldier
{
    new public virtual string Attack()
    {
        return "Shoot from blaster";
    }
}

class ManInBlackBoss : ManInBlack
{
    public override string Attack()
    {
        return "Shoot from blaster and call an army of aliens";
    }
}