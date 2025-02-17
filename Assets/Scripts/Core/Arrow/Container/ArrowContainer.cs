using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SturdyArrow.Core.Arrow.Container
{
    public class ArrowContainer : MonoBehaviour, IArrowContainer
    {
        public const int MAX_ARROWS_IN_CONTAINER = 100;
        
        [SerializeField]
        private float distanceBetweenArrows = 1f;

        private static Vector3[] _Positions = new Vector3[100];

        private IArrowCreator _arrowCreator;

        private List<Arrow> _container;

        private ContainerCamera _camera;

        private int _disabledArrowsCount;

        [Inject]
        private void Construct(IArrowCreator arrowCreator, ContainerCamera camera)
        {
            _arrowCreator = arrowCreator;
            _camera = camera;
        }

        public void Start()
        {
            InitializeCamera();
            InitializePositions();
            CreateContainer(MAX_ARROWS_IN_CONTAINER);
            gameObject.SetActive(false);
        }

        private void InitializeCamera()
        {
            _camera.VirtualCamera.transform.parent = transform;
            _camera.VirtualCamera.Follow = transform;
            _camera.VirtualCamera.LookAt = transform;
        }

        private void InitializePositions()
        {
            var index = 0;
            var lastX = 0;
            var lastY = 0;
            var spirals = 0;
            while(index < 100)
            {
                //->
                for(int i = lastX; i < 1 + spirals; i++)
                {
                    if(index < 80)
                    {
                        _Positions[index] = new Vector3(0, lastY, i);
                        index++;
                    }
                    else
                    {
                        if(index == 100) break;
                        if(i > -3 && i < 3)
                        {
                            _Positions[index] = new Vector3(0, lastY, i);
                            index++;
                        }
                    }
                }
                lastX = 1 + spirals;

                //|
                //v
                for(int j = lastY; j > -1 - spirals; j--)
                {
                    if(index < 80)
                    {
                        _Positions[index] = new Vector3(0, j, lastX);
                        index++;
                    }
                    else
                    {
                        if(index == 100) break;
                        if(j > -3 && j < 3)
                        {
                            _Positions[index] = new Vector3(0, j, lastX);
                            index++;
                        }
                    }
                }
                lastY = -1 - spirals;

                //<-
                for(int i = lastX; i > -1 - spirals; i--)
                {
                    if(index < 80)
                    {
                        _Positions[index] = new Vector3(0, lastY, i);
                        index++;
                    }
                    else
                    {
                        if(index == 100) break;
                        if(i > -3 && i < 3)
                        {
                            _Positions[index] = new Vector3(0, lastY, i);
                            index++;
                        }
                    }
                }
                lastX = -1 - spirals;

                //A
                //|
                for(int j = lastY; j < 1 + spirals; j++)
                {
                    if(index < 80)
                    {
                        _Positions[index] = new Vector3(0, j, lastX);
                        index++;
                    }
                    else
                    {
                        if(index == 100) break;
                        if(j > -3 && j < 3)
                        {
                            _Positions[index] = new Vector3(0, j, lastX);
                            index++;
                        }
                    }
                }
                lastY = 1 + spirals;

                spirals++;
            }
        }

        private void CreateContainer(int count)
        {
            _container = new List<Arrow>();
            CreateArrow(count);
            _disabledArrowsCount = MAX_ARROWS_IN_CONTAINER;
        }

        private void OnEnable()
        {
            _camera?.VirtualCamera.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            _camera?.VirtualCamera.gameObject.SetActive(false);
        }

        public void TryEnableArrows(int count)
        {
            if(count <= 0) return;

            if(_disabledArrowsCount <= count)
            {
                EnableAllArrows();
                return;
            }

            var i = 0;
            while(count > 0 && i < MAX_ARROWS_IN_CONTAINER)
            {
                if(!_container[i].gameObject.activeSelf)
                {
                    _container[i].gameObject.SetActive(true);
                    count--;
                }
                i++;
            }
        }

        public int GetContainerLength()
        {
            return _container.Count;
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        private void CreateArrow(int count, bool isActiveByDefault = false)
        {
            for(int i = 0; i < count; i++)
            {
                var createdObject = _arrowCreator.CreateArrow(_Positions[i] * distanceBetweenArrows,
                    gameObject.transform, Quaternion.identity);
                createdObject.gameObject.SetActive(isActiveByDefault);
                _container.Add(createdObject);
            }
        }

        private void EnableAllArrows()
        {
            _disabledArrowsCount = 0;
            for(int i = 0; i < MAX_ARROWS_IN_CONTAINER; i++)
            {
                if(!_container[i].gameObject.activeSelf)
                {
                    _container[i].gameObject.SetActive(true);
                }
            }
        }
    }
}