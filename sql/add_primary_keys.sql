delimiter //

drop procedure if exists add_primary_keys //
create procedure add_primary_keys()
begin
    DECLARE done int default false;
    DECLARE tbl_name CHAR(255);

    DECLARE cur1 cursor for SELECT DISTINCT TABLE_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = "sweep_development" and COLUMN_NAME LIKE "id" ;
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
    open cur1;

    myloop: loop
        fetch cur1 into tbl_name;
        if done then
            leave myloop;
        end if;
        set @sql = CONCAT('ALTER TABLE `sweep_development`.', tbl_name, ' ADD PRIMARY KEY(id)');
        select @sql;
        prepare stmt from @sql;
        execute stmt;
        drop prepare stmt;
    end loop;

    close cur1;
end //

delimiter ;

call add_primary_keys();