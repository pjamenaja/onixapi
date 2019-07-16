pipeline {
    agent any 
    
    parameters {
        string(name: 'SONAR_LOGIN_KEY', description: 'Sonarqube logon key')
    }    
    
    stages {
        stage('Start Code Analysis') {            
            steps {                
                sh '''/opt/dotnet_tools/dotnet-sonarscanner begin \
                    /key:"pjamenaja_onixapi" \
                    /o:pjamenaja \
                    /v:SNAPSHOT \
                    /d:sonar.host.url=https://sonarcloud.io \
                    /d:sonar.login=${params.SONAR_LOGIN_KEY}'''
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
        stage('End Code Analysis') {
            steps {
                sh "/opt/dotnet_tools/dotnet-sonarscanner end /d:sonar.login=${params.SONAR_LOGIN_KEY}"
            }
        }        
    }
}
