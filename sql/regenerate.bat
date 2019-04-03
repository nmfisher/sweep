mysql -u root --password=MyNewPass -e "DROP DATABASE sweep_development; CREATE DATABASE sweep_development"
mysql -u root --password=MyNewPass -D sweep_development < C:\Users\nickh\Documents\Projects\sweep_server\sql\mysql_schema.sql
mysql -u root --password=MyNewPass -D sweep_development -e ALTER TABLE goods ADD PRIMARY KEY(id)