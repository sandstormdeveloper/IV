using Navegacion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUI
{
    public void SetState(IState state);

    public IState GetState();
}
