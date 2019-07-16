pipeline {
    agent any 
    stages {
        stage('Static Code Analysis') {
            steps {
                sh '''/opt/sonar-scanner-3.3.0.1492/bin/sonar-scanner \
                  -Dsonar.projectKey=pjamenaja_onixapi \
                  -Dsonar.organization=pjamenaja \
                  -Dsonar.sources=.,sources \
                  -Dsonar.branch.name=trunk \
                  -Dsonar.projectVersion=SNAPSHOT \
                  -Dsonar.log.level=DEBUG \
                  -Dsonar.host.url=https://sonarcloud.io \
                  -Dsonar.login=d7c6549b1a4aedfe7318858b8e1b816343d5b080'''
            }
        }          
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
