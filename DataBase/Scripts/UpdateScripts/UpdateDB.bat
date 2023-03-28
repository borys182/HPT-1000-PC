setlocal
set PGPASSWORD=admin_friends

"psql.exe" -h localhost -U postgres -d postgres  -a -b -e  <Ver_1.0.12/Update_To_Ver_1.0.12.sql 1>Log_Query.txt 2>Log_Error.txt

endlocal