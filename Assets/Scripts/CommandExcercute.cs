using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommandExcercute 
{
    public abstract bool ConditionalCheck(string codition);
    public abstract void Run();
}
