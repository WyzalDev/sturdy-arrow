using UnityEngine;
using Zenject;

namespace SturdyArrow.Core.Arrow.Container
{
    public class ArrowContainerInstantiator : MonoBehaviour
    {
        //TODO: 2 create scriptableObject or Serializable Class for 3 params below
        public float DestroyParentTime { private get; set; } = 1f;

        public Vector3 ContainerPosition { private get; set; }

        public Quaternion ContainerRotation { private get; set; }

        private IArrowContainer _arrowContainer;

        [Inject]
        private void Construct(IArrowContainer arrowContainer)
        {
            _arrowContainer = arrowContainer;
        }

        public void OnShootAnimation()
        {
            var containerGameObject = _arrowContainer.GetGameObject();
            containerGameObject.SetActive(true);
            containerGameObject.transform.position = ContainerPosition;
            containerGameObject.transform.rotation = ContainerRotation;

            _arrowContainer.TryEnableArrows(10);
            Debug.Log("SHOOOT NOW COMPLETE");
            Destroy(transform.parent.gameObject, DestroyParentTime);
        }
    }
}