pipeline {
    agent any

    environment {
        JENKINS_BUILD = "true"
    }

    stages {
        stage('Clone Repository') {
            steps {
                git 'https://github.com/Ria-Raj-Nair-21/inventory.git'
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
