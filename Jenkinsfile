pipeline {
    agent any 
    stages {
        stage('Build') {
            steps {
                sh "dotnet build"
            }
        }
    }
    stages {
        stage('Unit Test') {
            steps {
                sh "dotnet test"
            }
        }
    }    
}
