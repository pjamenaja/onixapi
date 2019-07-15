pipeline {
    agent any 
    stages {
        stage('Build') {
            steps {
                sh "dotnet build"
            }
        }
        stage('Unit Test') {
            steps {
                sh "dotnet test"
            }
        }   
        stage('Static Code Analysis') {
            steps {
                sh '''sonar-scanner \
                  -Dsonar.projectKey=pjamenaja_onixapi \
                  -Dsonar.organization=pjamenaja \
                  -Dsonar.sources=. \
                  -Dsonar.host.url=https://sonarcloud.io \
                  -Dsonar.login=d7c6549b1a4aedfe7318858b8e1b816343d5b080'''
            }
        }          
    }
}
