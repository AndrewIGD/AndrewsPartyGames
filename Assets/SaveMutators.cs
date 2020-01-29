using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMutators : MonoBehaviour
{
    public Mutator[] mutators;
    public void Save()
    {
        foreach(Mutator mutator in mutators)
        {
            mutator.SaveMutation();
        }
    }
    public void ResetMutators()
    {
        foreach (Mutator mutator in mutators)
        {
            mutator.ResetMutation();
        }
    }
}
