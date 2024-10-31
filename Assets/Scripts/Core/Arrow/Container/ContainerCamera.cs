using Cinemachine;

namespace SturdyArrow.Core.Arrow.Container
{
    public class ContainerCamera
    {
        ContainerCamera(CinemachineVirtualCamera virtualCamera) => VirtualCamera = virtualCamera;
        public CinemachineVirtualCamera VirtualCamera { get; private set; }
    }
}