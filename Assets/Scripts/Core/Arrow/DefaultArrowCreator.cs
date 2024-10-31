using UnityEngine;

namespace SturdyArrow.Core.Arrow
{
    public class DefaultArrowCreator : IArrowCreator
    {
        private Arrow _arrowPrefab;

        public DefaultArrowCreator(Arrow arrowPrefab) => _arrowPrefab = arrowPrefab;

        public Arrow CreateArrow(Vector3 position, Transform parent, Quaternion rotation)
        {
            return GameObject.Instantiate(_arrowPrefab, position, rotation, parent);
        }
    }
}