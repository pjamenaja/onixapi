pipeline {
    agent any 
    
    parameters {
        string(name: 'SONAR_LOGIN_KEY', defaultValue: '0616e8ac5902cfec9bc6ee009b1b8c375de6a3aa', description: 'Sonarqube logon key')
        string(name: 'NUGET_PUSH_KEY', defaultValue: 'oy2nkpvkrxmkd3g6axdqhxzuzlxzreuebchf22q5ffvn7u', description: 'Nuget push key')        
    }    
    
    environment {
        PRODUCT_NAME = 'OnixApi'
        BUILT_VERSION = '1.1.1-SNAPSHOT'
        SONAR_SCANNER = '/home/tomcat/.dotnet/tools/dotnet-sonarscanner'
        COVERLET = '/home/tomcat/.dotnet/tools/coverlet'
        UNIT_TEST_ASSEMBLY = './tests/bin/Debug/netcoreapp2.2/OnixTest.dll'
        PACKAGE_PATH = './sources/bin/Release'
        BUILD_MODE = 'Release'
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

        stage('Nuget Publish') {
            steps {
                sh "dotnet nuget push ${env.PACKAGE_PATH}/${env.PRODUCT_NAME}.${env.BUILT_VERSION}.nupkg \
                -k ${params.NUGET_PUSH_KEY} \
                -s https://api.nuget.org/v3/index.json"
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
