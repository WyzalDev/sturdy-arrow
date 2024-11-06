def PROJECT_NAME = "sturdy_arrow"
def BUILD_PATH = "D:\\workspace\\UnityBuilds\\${PROJECT_NAME}"
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
                script {
                    bat '"%UNITY_PATH%\\Unity.exe" -quit -batchmode -projectPath "%PROJECT_PATH%" -executeMethod AutomatedBuildProcess.StartWebGLBuild -logfile -'
                }
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
        stage('Deploy WebGL') {
            when{expression{params.BUILD_PLATFORM == 'WebGL' && params.BUILD_TYPE == 'Deploy'}}
            steps {
                script {
                    def dirrectories = new File("${BUILD_PATH}").listFiles().findAll { it.isDirectory() }
                    def latestModifiedDir = dirrectories.max { it.lastModified() }
                    env.LATEST_DIR = latestModifiedDir.getAbsolutePath()
                    echo "${LATEST_DIR} Deploying WebGL on 9090 port...."
                    bat 'python -m http.server 9090 -d %LATEST_DIR%'
                    bat 'timeout /t 5'
                    bat 'curl -S http://localhost:9090/index.html'
                }
            }
        }
    }
}