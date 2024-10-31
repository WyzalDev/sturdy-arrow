using Cinemachine;
using SturdyArrow.Core.Archer;
using SturdyArrow.Core.Arrow;
using SturdyArrow.Core.Arrow.Container;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using Zenject;

namespace SturdyArrow.Infrastructure.Installers
{
    public class ArcherInstaller : MonoInstaller
    {
        //TODO: 1 create scriptableObject or Serializable Class for 3 params below
        [SerializeField]
        private float destroyArcherTime = 1f;

        [SerializeField]
        private Vector3 containerPosition;

        [SerializeField]
        private Vector3 containerRotation;

        [SerializeField]
        private Vector3 containerCameraFollowOffset;

        [SerializeField]
        private List<GameObject> skins;

        [SerializeField]
        private Arrow arrowPrefab;

        [SerializeField]
        private GameObject archerPrefab;

        [SerializeField]
        private AnimatorController animatorController;

        [SerializeField]
        private CinemachineVirtualCamera archerCameraPrefab;

        public override void InstallBindings()
        {
            BindArrowCreator();
            InstantiateAndBindContainerCamera();
            InstantiateAndBindArrowContainer();
            InstantiateArcher();
        }

        private void InstantiateArcher()
        {
            var archer = Container.InstantiatePrefab(archerPrefab);
            
            var skin = Container.InstantiatePrefab(skins[ArcherMarker.skinIdex]);
            skin.transform.SetParent(archer.transform);
            skin.transform.localPosition = new Vector3(0, 0, 0);
            skin.transform.Rotate(new Vector3(0, 180, 0));
            if(skin.TryGetComponent(out Animator animator))
            {
                animator.runtimeAnimatorController = animatorController;
            }

            CinemachineVirtualCamera archerCamera = Container.InstantiatePrefab(archerCameraPrefab).GetComponent<CinemachineVirtualCamera>();
            archerCamera.Follow = archer.transform;
            archerCamera.transform.parent = archer.transform;
            
            var arrowContainerInstantiator = skin.AddComponent<ArrowContainerInstantiator>();
            arrowContainerInstantiator.DestroyParentTime = destroyArcherTime;
            arrowContainerInstantiator.ContainerPosition = containerPosition + archer.transform.position;
            arrowContainerInstantiator.ContainerRotation = Quaternion.Euler(containerRotation);
            Container.Inject(arrowContainerInstantiator);
        }

        private void BindArrowCreator()
        {
            Container.Bind<IArrowCreator>()
                .To<DefaultArrowCreator>()
                .AsSingle()
                .WithArguments(arrowPrefab)
                .NonLazy();
        }

        private void InstantiateAndBindArrowContainer()
        {
            var arrowContainer = Container.InstantiateComponentOnNewGameObject<ArrowContainer>();

            Container.Bind<IArrowContainer>()
                .FromInstance(arrowContainer)
                .AsSingle();
        }

        private void InstantiateAndBindContainerCamera()
        {
            var containerCamera = Container.InstantiateComponentOnNewGameObject<CinemachineVirtualCamera>();
            containerCamera.Priority = 2;
            containerCamera.AddCinemachineComponent<CinemachineComposer>();
            var containerCameraTransposer = containerCamera.AddCinemachineComponent<CinemachineTransposer>();
            containerCameraTransposer.m_FollowOffset = containerCameraFollowOffset;

            Container.Bind<ContainerCamera>()
                .AsSingle()
                .WithArguments(containerCamera)
                .NonLazy();
        }
    }
}