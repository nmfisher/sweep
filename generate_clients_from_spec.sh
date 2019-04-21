spec_file=sweep.yaml

shopt -s nocasematch
case "production" in
 $1 ) cp sweep.yaml sweep_prod.yaml; spec_file=sweep_prod.yaml; sed -i -e 's/sweep-development\.ngrok\.io/app\.sweephq\.com/g' sweep_prod.yaml;;
esac

java -jar $GENERATOR_PATH generate -i $spec_file -g dart -o ./clients/lib/dart -t C:\\Users\\nickh\\Documents\\Projects\\openapi-generator\\modules\\openapi-generator\\src\\main\\resources\\dart2 -DpackageName=Sweep -DpubName=sweep_api -DbrowserClient=false -DdebugModels 
java -jar $GENERATOR_PATH generate -i $spec_file -g csharp-netcore -o ./clients/lib/dotnet -DpackageName=Sweep