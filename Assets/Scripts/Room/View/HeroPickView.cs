using strange.extensions.mediation.impl;
using UnityEngine;

namespace Sesto.RoadTo5k
{
    // TODO
    public class HeroPickView : View
    {
        public GameObject heroPickPortraitPrefab;

        private GameObject[] heroPortraits;

        void Awake()
        {
            heroPortraits = new GameObject[10];
            for (int i = 0; i < 10; i++)
            {
                GameObject heroPortrait = Instantiate(heroPickPortraitPrefab) as GameObject;
                heroPortrait.name = "Hero " + (i + 1);
                heroPortrait.transform.parent = transform;
                RectTransform portraitTransform = heroPortrait.transform as RectTransform;
                portraitTransform.localScale = Vector3.one;
                portraitTransform.localRotation = Quaternion.identity;
                portraitTransform.localPosition = Vector3.zero;
                portraitTransform.anchoredPosition = Vector2.zero;
                portraitTransform.anchorMin = new Vector2(0.2f * (i % 5), i < 5 ? 0.5f : 0);
                portraitTransform.anchorMax = new Vector2(0.2f + 0.2f * (i % 5), i < 5 ? 1f : 0.5f);
                portraitTransform.sizeDelta = Vector2.zero;
                    
                heroPortraits[i] = heroPortrait;
            }
        }
    }
}
