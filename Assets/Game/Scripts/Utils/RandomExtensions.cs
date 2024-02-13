using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Game.Scripts.Utils
{
    public static class RandomExtensions
    {
        public static int Probability(this int[] probabilityArray)
        {
            var probability = Random.Range(1, 101);

            for (int j = 0; j < probabilityArray.Length; j++)
            {
                if (probability <= probabilityArray[j])
                    return j;
            }
            
            return -1;
        }
        
        public static void ShuffleList<T>(this List<T> list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                var randomIndex = Random.Range(i, list.Count);
                var temp = list[randomIndex];
                    
                list[randomIndex] = list[i];
                list[i] = temp;
            }
        }
    }
}