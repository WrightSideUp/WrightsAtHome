CREATE TABLE Log(
id int primary key not null identity(1,1),
TimeStamp datetime,
Message nvarchar(1024),
Exception nvarchar(512),
level nvarchar(10),
logger nvarchar(128))

