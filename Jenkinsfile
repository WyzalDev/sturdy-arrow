def PROJECT_NAME = "sturdy-arrow"
def BUILD_PATH = "D:\\workspace\\UnityBuilds"
def UNITY_VERSION = "2022.3.49f1"

pipeline {
    environment {
        PROJECT_PATH = "${WORKSPACE}"
        UNITY_PATH = "F:\\Unity\\${UNITY_VERSION}\\Editor"
    }

    agent any

    stages {
        stage('Build WebGL') {
            when{expression{params.BUILD_PLATFORM == 'WebGL'}}
            steps {
                echo 'Building..'
            }
        }
        stage('Build Windows') {
            when{expression{params.BUILD_PLATFORM == 'Windows'}}
            steps {
                script {
                    bat '"%UNITY_PATH%\\Unity.exe" -quit -batchmode -projectPath "%PROJECT_PATH%" -executeMethod AutomatedBuildProcess.StartWinBuild -logfile -'
                }
            }
        }
        stage('Test') {
            steps {
                echo 'Testing..'
            }
        }
        stage('Deploy Windows') {
            when{expression{params.BUILD_PLATFORM == 'Windows'}}
            steps {
                echo "${currentBuild.fullProjectName} Deploying Windows...."
            }
        }
    }
}