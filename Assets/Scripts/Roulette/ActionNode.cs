using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNode : INode
{
    //()=>()
    public delegate void myDelegate();
    myDelegate _action;

    //Constructor
    public ActionNode(myDelegate action)
    {
        _action = action;
    }

    //SubAction (Metodo)
    public void SubAction(myDelegate newAction)
    {
        _action += newAction;
    }
    public void Execute()
    {
        _action();
    }
}
