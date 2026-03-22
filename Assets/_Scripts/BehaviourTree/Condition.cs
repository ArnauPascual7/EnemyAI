using UnityEngine;

public class Condition
{
    public string cname;
    public bool check;

    public Condition(ConditionName name)
    {
        this.cname = name.ToString();
        check = false;
    }
}

public enum ConditionName
{
    Patrol,
    Chase,
    Attack,
    Dead
}