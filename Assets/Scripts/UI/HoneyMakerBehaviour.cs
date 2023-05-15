using System.Collections;
using UnityEngine;

public class HoneyMakerBehaviour : MonoBehaviour 
{
    [SerializeField] private IntValue _honey;

    public IEnumerator MakeHoney(Bee bee)
    {
        int lifespan = ((Lifespan)bee.activeTraits[0]).duration;
        int productionRate = ((ProductionRate)bee.activeTraits[1]).rate;
        int cycles = lifespan / productionRate;

        for (int i = 0; i < cycles; i++)
        {
            yield return new WaitForSeconds(productionRate);
            _honey.value++;
        }
    }
}