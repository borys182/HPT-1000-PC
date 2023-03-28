setlocal
set PGPASSWORD=admin_friends

"psql.exe" -h localhost -U postgres -d postgres  -a -b -e  <HE-005_CreateDB.sql 1>Log_Query.txt 2>Log_Error.txt

endlocal