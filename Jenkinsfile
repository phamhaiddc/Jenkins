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
                    def subdirectory = 'WebApplication1'

                    // Change the working directory to the project's subdirectory
                    dir(subdirectory) {
                        // Now you're in the 'src/webapplication1' directory
                        script {
                            def dotnetCommand = bat(script: 'dotnet --version', returnStatus: true)
                            if (dotnetCommand == 0) {
                                bat 'dotnet restore'
                                bat 'dotnet build'
                                // Add additional commands as needed (e.g., dotnet test)
                            } else {
                                error 'dotnet CLI is not installed. Install it on your Jenkins agent.'
                            }
                        }
                    }

            }
        }
        }

        stage('Build Image') {
            
            steps {

                 script {
                    def dockerImage = docker.build('webapi:latest')

                    // Run the Docker container from the built image
                    def container = dockerImage.run('-d --name api_container')

                    // Remember the image ID for removal
                    def imageId = dockerImage.imageId

                    // Perform other tasks with the container

                    // Remove the Docker container when done
                    container.remove()

                    // Remove the Docker image using the 'docker rmi' command
                    sh "docker rmi ${imageId}"
                }

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
