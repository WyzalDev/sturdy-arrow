using UnityEngine;

namespace SturdyArrow.Core.Arrow.Container
{
    public interface IArrowContainer
    {
        public void TryEnableArrows(int count);

        public int GetContainerLength();

        public GameObject GetGameObject();
    }
}