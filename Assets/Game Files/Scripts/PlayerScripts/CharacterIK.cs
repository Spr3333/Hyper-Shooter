using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterIK : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private RigBuilder rigBuilder;


    [Header("IK Constraint")]
    [SerializeField] private TwoBoneIKConstraint[] TwoBoneIKConstraint;
    [SerializeField] private MultiAimConstraint[] MultiAimConstraint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConfigureIK(Transform ikTarget)
    {
        rigBuilder.enabled = true;

        foreach(var twoBoneikConstraint in TwoBoneIKConstraint)
        {
            twoBoneikConstraint.data.target = ikTarget;
        }

        foreach (var multiArmConstraint in MultiAimConstraint)
        {
            WeightedTransformArray weightedTransforms = new WeightedTransformArray();
            weightedTransforms.Add(new WeightedTransform(ikTarget, 1));
            multiArmConstraint.data.sourceObjects = weightedTransforms;
        }

        rigBuilder.Build();
    }

    public void DisableIK()
    {
        rigBuilder.enabled = false;
    }
}
