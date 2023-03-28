
"pg_dump.exe"  -F p -a --column-inserts -t errors_txt 	 	--dbname=postgres://postgres:admin_friends@127.0.0.1/HE-005 > d:\Projekty\HPT-1000\HPT-1000_PC\DataBase\FillTables\ErrorsTxt.sql
"pg_dump.exe"  -F p -a --column-inserts -t events_txt 	 	--dbname=postgres://postgres:admin_friends@127.0.0.1/HE-005 > d:\Projekty\HPT-1000\HPT-1000_PC\DataBase\FillTables\EventsText.sql
"pg_dump.exe"  -F p -a --column-inserts -t languages	 	--dbname=postgres://postgres:admin_friends@127.0.0.1/HE-005 > d:\Projekty\HPT-1000\HPT-1000_PC\DataBase\FillTables\Language.sql
"pg_dump.exe"  -F p -a --column-inserts -t types_privilige 	--dbname=postgres://postgres:admin_friends@127.0.0.1/HE-005 > d:\Projekty\HPT-1000\HPT-1000_PC\DataBase\FillTables\Types_Privilige.sql
"pg_dump.exe"  -F p -a --column-inserts -t types_block_account 	--dbname=postgres://postgres:admin_friends@127.0.0.1/HE-005 > d:\Projekty\HPT-1000\HPT-1000_PC\DataBase\FillTables\TypesBlockAccount.sql
"pg_dump.exe"  -F p -a --column-inserts -t users 		--dbname=postgres://postgres:admin_friends@127.0.0.1/HE-005 > d:\Projekty\HPT-1000\HPT-1000_PC\DataBase\FillTables\Users.sql

