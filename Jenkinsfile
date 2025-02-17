def PROJECT_NAME = "sturdy_arrow"

def UNITY_VERSION = "2022.3.52f1"

pipeline {
    environment {
        PROJECT_PATH = "${WORKSPACE}"
        UNITY_PATH = "F:\\Unity\\${UNITY_VERSION}\\Editor"
        BUILD_PATH = "D:\\workspace\\UnityBuilds\\${PROJECT_NAME}"
        SERVER_PATH = "D:\\WebGLServer\\${PROJECT_NAME}"
    }

    agent any

    stages {
        stage('Build Clean') {
            steps {
                script {
                    echo 'Start Build Clean...'
                    bat '(for %%f in ("%BUILD_PATH%\\*.*") do (if NOT "%%~xf"==".zip" del /Q "%%f"))'
                    bat '(for /D %%d in ("%BUILD_PATH%\\*.*") do rd /S /Q "%%d")'
                    echo 'End Build Clean...'
                }
            }
        }

        stage('Server Clean') {
            when{expression{params.BUILD_TYPE == 'Deploy'}}
            steps {
                script {
                    echo 'Start Server Clean...'
                    bat 'if exist "%SERVER_PATH%" (rd /S /Q "%SERVER_PATH%")'
                    bat 'echo symbol %SERVER_PATH%'
                    echo 'End Server Clean...'
                }
            }
        }

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
                    def buildFiles = new File("${BUILD_PATH}").listFiles().findAll { it.isDirectory() }
                    def latestModifiedBuild = buildFiles.max { it.lastModified() }
                    env.LATEST_BUILD_PATH = latestModifiedBuild.getAbsolutePath()
                    env.BUILD_NAME = latestModifiedBuild.getName()
                    echo "${BUILD_NAME} Deploying WebGL build onto local http server..."
                    bat 'xcopy "%LATEST_BUILD_PATH%\\*" "%SERVER_PATH%\\" /E /I'
                }
            }
        }
    }
}