mysql -u root --password=MyNewPass -e "DROP DATABASE sweep_development; CREATE DATABASE sweep_development"
mysql -u root --password=MyNewPass -D sweep_development < mysql_schema.sql
mysql -u root --password=MyNewPass -D sweep_development < add_primary_keys.sql