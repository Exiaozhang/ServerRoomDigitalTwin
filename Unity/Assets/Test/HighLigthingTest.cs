using ETModel;
using UnityEngine;

namespace Test
{
    public class HighLigthingTest: MonoBehaviour
    {
        HighlightableObject h;
        
        void Start()
        {
            h = this.GetComponent<HighlightableObject>();

            h.Off();

            h.On(Color.yellow);
            
        }
    }
}