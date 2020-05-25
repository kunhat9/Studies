for %%G in (*.sql) do sqlcmd -S localhost -d DB_STUDIES -U sa -P 123 -I -i"%%G"
pause