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
    }
}
