using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SturdyArrow.Util
{
    public class CameraFollower : MonoBehaviour
    {
        public Transform targetCamera;

        #region CAMERA_FOLLOW_METHODS

        private void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            targetCamera = CinemachineCore.Instance.GetActiveBrain(0).transform;
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            targetCamera = CinemachineCore.Instance.GetActiveBrain(0).transform;
        }

        ~CameraFollower()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }


        private void Update()
        {
            if(targetCamera != null)
            {
                transform.position = targetCamera.position;
                transform.rotation = targetCamera.rotation;
            }
        }
        #endregion
    }
}
