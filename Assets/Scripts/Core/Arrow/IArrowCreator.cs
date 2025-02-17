using UnityEngine;

namespace SturdyArrow.Core.Arrow
{
    public interface IArrowCreator
    {
        public Arrow CreateArrow(Vector3 position, Transform parent, Quaternion rotation);
    }
}