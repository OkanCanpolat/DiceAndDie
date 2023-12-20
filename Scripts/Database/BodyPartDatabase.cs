using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartDatabase : MonoBehaviour
{
    public static BodyPartDatabase Instance;
    [SerializeField] private List<BodyPartMap> bodyParts;
    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    public Transform GetPartTransform(BodyParts part)
    {
        foreach(BodyPartMap map in bodyParts)
        {
            if(map.bodyPart == part)
            {
                return map.partTransform;
            }
        }
        return null;
    }
}
