sql_dir=$(dirname $0)
mysql -u root --password=MyNewPass -e "DROP DATABASE sweep_db; CREATE DATABASE sweep_db"
mysql -u root --password=MyNewPass -D sweep_db < $sql_dir/mysql_schema.sql
mysql -u root --password=MyNewPass -D sweep_db < $sql_dir/add_primary_keys.sql