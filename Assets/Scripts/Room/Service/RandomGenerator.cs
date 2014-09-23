using UnityEngine;

namespace Sesto.RoadTo5k
{
    // TODO https://github.com/strangeioc/strangerocks/blob/master/StrangeRocks/Assets/scripts/strangerocks/game/model/GameConfig.cs
    public class RandomGenerator : IRandomGenerator
    {

        [PostConstruct]
        public void PostConstruct()
        {
            TextAsset file = Resources.Load("randomStuff") as TextAsset;

            var n = SimpleJSON.JSON.Parse(file.text);
        }
    }
}

