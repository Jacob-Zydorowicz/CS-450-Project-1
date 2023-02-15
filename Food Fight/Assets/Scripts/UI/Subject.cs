using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Subject
{

    public void Register(Observer ob);
    public void Unregister(Observer ob);

    public void Notify();
}

