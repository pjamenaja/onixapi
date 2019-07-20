pipeline {
    agent any 
    
    parameters {
        string(name: 'SONAR_LOGIN_KEY', defaultValue: '0616e8ac5902cfec9bc6ee009b1b8c375de6a3aa', description: 'Sonarqube logon key')
    }    
    
    environment {
        BUILT_VERSION = '1.1.0-SNAPSHOT'
        SONAR_SCANNER = '/home/tomcat/.dotnet/tools/dotnet-sonarscanner'
        COVERLET = '/home/tomcat/.dotnet/tools/coverlet'
        UNIT_TEST_ASSEMBLY = './tests/bin/Debug/netcoreapp2.2/OnixTest.dll'
        BUILD_MODE = 'Debug'
    }

    stages {
        stage('Start Code Analysis') {            
            steps {                
                sh "${env.SONAR_SCANNER} begin \
                    /key:pjamenaja_onixapi \
                    /o:pjamenaja \
                    /v:${env.BUILT_VERSION} \
                    /d:sonar.host.url=https://sonarcloud.io \
                    /d:sonar.branch.name=${env.BRANCH_NAME} \
                    /d:sonar.cs.opencover.reportsPaths=./coverage.opencover.xml \
                    /d:sonar.login=${params.SONAR_LOGIN_KEY}"

                sh "echo [${env.BUILT_VERSION}]"
            }
        }

        stage('Build') {
            steps {
                sh "dotnet build -c ${env.BUILD_MODE} -p:Version=${env.BUILT_VERSION}"
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
