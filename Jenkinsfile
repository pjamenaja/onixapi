pipeline {
    agent any 
    
    parameters {
        string(name: 'SONAR_LOGIN_KEY', defaultValue: '0616e8ac5902cfec9bc6ee009b1b8c375de6a3aa', description: 'Sonarqube logon key')
    }    
    
    stages {
        stage('Start Code Analysis') {            
            steps {                
                sh "/home/tomcat/.dotnet/tools/dotnet-sonarscanner begin \
                    /key:pjamenaja_onixapi \
                    /o:pjamenaja \
                    /v:SNAPSHOT \
                    /d:sonar.host.url=https://sonarcloud.io \
                    /d:sonar.login=${params.SONAR_LOGIN_KEY}"
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
                sh "/home/tomcat/.dotnet/tools/dotnet-sonarscanner end /d:sonar.login=${params.SONAR_LOGIN_KEY}"
            }
        }        
    }
}
