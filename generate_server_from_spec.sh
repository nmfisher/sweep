spec_file=sweep.yaml

shopt -s nocasematch
case "production" in
 $1 ) cp sweep.yaml sweep_prod.yaml; spec_file=sweep_prod.yaml; sed -i -e 's/sweep-development\.ngrok\.io/app\.sweephq\.com/g' sweep_prod.yaml;;
esac

java -jar $GENERATOR_PATH generate -i $spec_file -g fsharp-giraffe -o ./server -DpackageName=Sweep
java -jar $GENERATOR_PATH generate -i $spec_file -g mysql-schema -o ./sql -DpackageName=Sweep
java -jar $GENERATOR_PATH generate -i $spec_file -g typescript-axios -o ./clients/lib/typescript-axios -DpackageName=Sweep

./sql/regenerate.sh
pushd ./clients/lib/typescript-axios
npm run build
popd

