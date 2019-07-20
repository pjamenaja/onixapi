pipeline {
    agent any 
    
    parameters {
        string(name: 'SONAR_LOGIN_KEY', defaultValue: '0616e8ac5902cfec9bc6ee009b1b8c375de6a3aa', description: 'Sonarqube logon key')
    }    
    
    environment {
        BUILT_VERSION = 'VERSION'
        SONAR_SCANNER = '/home/tomcat/.dotnet/tools/dotnet-sonarscanner'
        COVERLET = '/home/tomcat/.dotnet/tools/coverlet'
        UNIT_TEST_ASSEMBLY = './tests/bin/Release/netcoreapp2.2/OnixTest.dll'
    }

    stages {
        stage('Initialize') {  
            //conditional for parameter
            when {
                environment name: 'BUILT_VERSION', value: 'VERSION'
            }                      
            steps {
                sh "echo [${env.BUILT_VERSION}]"
                env.MYTOOL_VERSION = '1.33.55'
            }
        } 

        stage('Start Code Analysis') {            
            steps {                
                sh "${env.SONAR_SCANNER} begin \
                    /key:pjamenaja_onixapi \
                    /o:pjamenaja \
                    /v:SNAPSHOT \
                    /d:sonar.host.url=https://sonarcloud.io \
                    /d:sonar.branch.name=${env.BRANCH_NAME} \
                    /d:sonar.cs.opencover.reportsPaths=./coverage.opencover.xml \
                    /d:sonar.login=${params.SONAR_LOGIN_KEY}"

                sh "echo [${env.BUILT_VERSION}]"
            }
        }          

        stage('Build') {
            steps {
                sh "dotnet build -c Release"
            }
        }

        stage('Unit Test') {
            steps {
                sh "${env.COVERLET} ${env.UNIT_TEST_ASSEMBLY} --target 'dotnet' --targetargs 'test . --no-build' --format opencover"
            }
        } 

        stage('End Code Analysis') {
            steps {
                sh "${env.SONAR_SCANNER} end /d:sonar.login=${params.SONAR_LOGIN_KEY}"
            }
        }        
    }
}
