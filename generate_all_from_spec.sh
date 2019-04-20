current_dir=$(pwd)
script_dir=$(dirname $0)

if [ $script_dir = '.' ]
then
script_dir="$current_dir"
fi
echo $script_dir

$script_dir/generate_server_from_spec.sh $1
$script_dir/generate_clients_from_spec.sh $1



