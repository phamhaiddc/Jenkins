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
                        bat 'dotnet restore "jenkins/WebApplication1/WebApplication1.csproj"'
                        bat 'dotnet build "jenkins/WebApplication1/WebApplication1.csproj"'
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
