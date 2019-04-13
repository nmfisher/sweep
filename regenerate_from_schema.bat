set GENERATOR_PATH=C:\Users\nickh\Documents\Projects\openapi-generator\modules\openapi-generator-cli\target\openapi-generator-cli.jar
set PROJECT_DIR=C:\Users\nickh\Documents\Projects\sweep_server\

pushd %PROJECT_DIR%
java -jar %GENERATOR_PATH% generate -i sweep.yaml -g fsharp-giraffe -o %PROJECT_DIR%\server -DpackageName=Sweep
java -jar %GENERATOR_PATH% generate -i sweep.yaml -g mysql-schema -o %PROJECT_DIR%\sql -DpackageName=Sweep
java -jar %GENERATOR_PATH% generate -i sweep.yaml -g typescript-axios -o %PROJECT_DIR%\dashboard\lib\api -DpackageName=Sweep
call %PROJECT_DIR%\sql\regenerate.bat
pushd %PROJECT_DIR%\dashboard\lib\api
npm run build

