pushd %~dp0
mysql -u root --password=MyNewPass -e "DROP DATABASE sweep_db; CREATE DATABASE sweep_db"
mysql -u root --password=MyNewPass -D sweep_db < mysql_schema.sql
mysql -u root --password=MyNewPass -D sweep_db < add_primary_keys.sql
load_dev.bat
popd