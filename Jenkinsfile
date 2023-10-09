pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                // Checkout the source code from the Git repository
                checkout([$class: 'GitSCM', branches: [[name: '*/main']], doGenerateSubmoduleConfigurations: false, extensions: [], submoduleCfg: [], userRemoteConfigs: [[credentialsId: 'jenkins_1', url: 'https://username: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@github.com/phamhaiddc/jenkins.git']]])            }
        }

        stage('Build') {
            steps {
                // Build the Docker image using the Dockerfile
                script {
                    def dotnetCommand = bat(script: 'dotnet --version', returnStatus: true)
                    if (dotnetCommand == 0) {
                        def currentDir = pwd()
                        echo "Current Directory: ${currentDir}"


                        // Use a shell command to list files in the current directory
                        def fileList = sh(script: "ls ${currentDir}", returnStdout: true).trim()

                        // Print the list of files
                        echo "List of files in ${currentDir}:"
                        echo fileList


                        bat 'dotnet restore'
                        bat 'dotnet build'
                        // Add additional commands as needed (e.g., dotnet test)
                    } else {
                        error 'dotnet CLI is not installed. Install it on your Jenkins agent.'
                    }
                }

            }
        }

        stage('Test') {
            steps {
                // Run unit tests or any other testing steps as needed
                sh 'docker run WebApi.csproj:${env.BUILD_ID} dotnet test'
            }
        }
    }

    post {
        always {
            // Clean up Docker images and containers
            cleanWs()
            script {
                docker.image("WebApi:${env.BUILD_ID}").remove()
            }
        }
    }
}
