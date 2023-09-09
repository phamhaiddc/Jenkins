pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                // Checkout the source code from the Git repository
                checkout scm
            }
        }

        stage('Build') {
            steps {
                // Build the Docker image using the Dockerfile
                script {
                    def dockerImage = docker.build("WebApi:${env.BUILD_ID}", "-f Dockerfile .")
                }
            }
        }

        stage('Test') {
            steps {
                // Run unit tests or any other testing steps as needed
                sh 'docker run WebApiapp:${env.BUILD_ID} dotnet test'
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
