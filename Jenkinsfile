pipeline {
    agent any

    environment {
        JENKINS_BUILD = "true"
    }

    stages {
        stage('Clone Repository') {
            steps {
                git 'https://your-repo-url.git'
            }
        }

        stage('Restore Dependencies') {
            steps {
                sh 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                sh 'dotnet build --configuration Release'
            }
        }

        stage('Run Automated Tests') {
            steps {
                sh 'dotnet run'
            }
        }
    }
}
