shopt -s nocasematch
case "production" in
 $1 ) cp sweep.yaml sweep_prod.yaml; spec_file=sweep_prod.yaml; sed -i -e 's/sweep-development\.ngrok\.io/app\.sweephq\.com/g' sweep_prod.yaml;;
esac

java -jar $GENERATOR_PATH generate -i $spec_file -g dart -o ./clients/lib/dart -DpackageName=Sweep