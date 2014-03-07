declare @adminRoleId int
declare @managerRoleId int

select @adminRoleId = RoleId
  from dbo.Roles
  where RoleName = 'Admin'
select @managerRoleId = RoleId
  from dbo.Roles
  where RoleName = 'Manager'

if not exists (select * from dbo.Users where UserEmail = 'cglance@gmail.com')
insert into dbo.Users
        ( UserEmail ,
          UserFirstName ,
          UserLastName ,
          UserRoleId ,
          UserActiveIn ,
          UserNotifyFiveDays ,
          UserNotifyTenDays ,
          UserPassword
        )
values  ( 'cglance@gmail.com' , -- UserEmail - varchar(256)
          'Chris' , -- UserFirstName - varchar(50)
          'Lance' , -- UserLastName - varchar(50)
          @adminRoleId , -- UserRoleId - int
          1 , -- UserActiveIn - bit
          1 , -- UserNotifyFiveDays - bit
          1 , -- UserNotifyTenDays - bit
          'a56f3dbc3053cc78282ebb4360025187945043453f94099157496b76530a404d'  -- UserPassword - varchar(256)
        )

if not exists (select * from dbo.Users where UserEmail = 'louisfischer@gmail.com')
insert into dbo.Users
        ( UserEmail ,
          UserFirstName ,
          UserLastName ,
          UserRoleId ,
          UserActiveIn ,
          UserNotifyFiveDays ,
          UserNotifyTenDays ,
          UserPassword
        )
values  ( 'louisfischer@gmail.com' , -- UserEmail - varchar(256)
          'Louis' , -- UserFirstName - varchar(50)
          'Fischer' , -- UserLastName - varchar(50)
          @adminRoleId , -- UserRoleId - int
          1 , -- UserActiveIn - bit
          1 , -- UserNotifyFiveDays - bit
          1 , -- UserNotifyTenDays - bit
          null  -- UserPassword - varchar(256)
        )

if not exists (select * from dbo.Users where UserEmail = 'amohan1223@gmail.com')
insert into dbo.Users
        ( UserEmail ,
          UserFirstName ,
          UserLastName ,
          UserRoleId ,
          UserActiveIn ,
          UserNotifyFiveDays ,
          UserNotifyTenDays ,
          UserPassword
        )
values  ( 'amohan1223@gmail.com' , -- UserEmail - varchar(256)
          'Akshaya' , -- UserFirstName - varchar(50)
          'Mohan' , -- UserLastName - varchar(50)
          @adminRoleId , -- UserRoleId - int
          1 , -- UserActiveIn - bit
          1 , -- UserNotifyFiveDays - bit
          1 , -- UserNotifyTenDays - bit
          null  -- UserPassword - varchar(256)
        )

if not exists (select * from dbo.Users where UserEmail = 'anthonylassiter@gmail.com')
insert into dbo.Users
        ( UserEmail ,
          UserFirstName ,
          UserLastName ,
          UserRoleId ,
          UserActiveIn ,
          UserNotifyFiveDays ,
          UserNotifyTenDays ,
          UserPassword
        )
values  ( 'anthonylassiter@gmail.com' , -- UserEmail - varchar(256)
          'Anthony' , -- UserFirstName - varchar(50)
          'Lassiter' , -- UserLastName - varchar(50)
          @adminRoleId , -- UserRoleId - int
          1 , -- UserActiveIn - bit
          1 , -- UserNotifyFiveDays - bit
          1 , -- UserNotifyTenDays - bit
          null  -- UserPassword - varchar(256)
        )
        
if not exists (select * from dbo.Users where UserEmail = 'brian.avent@gmail.com')
insert into dbo.Users
        ( UserEmail ,
          UserFirstName ,
          UserLastName ,
          UserRoleId ,
          UserActiveIn ,
          UserNotifyFiveDays ,
          UserNotifyTenDays ,
          UserPassword
        )
values  ( 'brian.avent@gmail.com' , -- UserEmail - varchar(256)
          'Brian' , -- UserFirstName - varchar(50)
          'Avent' , -- UserLastName - varchar(50)
          @adminRoleId , -- UserRoleId - int
          1 , -- UserActiveIn - bit
          1 , -- UserNotifyFiveDays - bit
          1 , -- UserNotifyTenDays - bit
          null  -- UserPassword - varchar(256)
        )

if not exists (select * from dbo.Users where UserEmail = 'cmooth@gmail.com')
insert into dbo.Users
        ( UserEmail ,
          UserFirstName ,
          UserLastName ,
          UserRoleId ,
          UserActiveIn ,
          UserNotifyFiveDays ,
          UserNotifyTenDays ,
          UserPassword
        )
values  ( 'cmooth@gmail.com' , -- UserEmail - varchar(256)
          'Cale' , -- UserFirstName - varchar(50)
          'Mooth' , -- UserLastName - varchar(50)
          @adminRoleId , -- UserRoleId - int
          1 , -- UserActiveIn - bit
          1 , -- UserNotifyFiveDays - bit
          1 , -- UserNotifyTenDays - bit
          null  -- UserPassword - varchar(256)
        )

if not exists (select * from dbo.Users where UserEmail = 'erlemulligan@gmail.com')
insert into dbo.Users
        ( UserEmail ,
          UserFirstName ,
          UserLastName ,
          UserRoleId ,
          UserActiveIn ,
          UserNotifyFiveDays ,
          UserNotifyTenDays ,
          UserPassword
        )
values  ( 'erlemulligan@gmail.com' , -- UserEmail - varchar(256)
          'Erle' , -- UserFirstName - varchar(50)
          'Mulligan' , -- UserLastName - varchar(50)
          @adminRoleId , -- UserRoleId - int
          1 , -- UserActiveIn - bit
          1 , -- UserNotifyFiveDays - bit
          1 , -- UserNotifyTenDays - bit
          null  -- UserPassword - varchar(256)
        )

if not exists (select * from dbo.Users where UserEmail = 'jpresa_2000@hotmail.com')
insert into dbo.Users
        ( UserEmail ,
          UserFirstName ,
          UserLastName ,
          UserRoleId ,
          UserActiveIn ,
          UserNotifyFiveDays ,
          UserNotifyTenDays ,
          UserPassword
        )
values  ( 'jpresa_2000@hotmail.com' , -- UserEmail - varchar(256)
          'Jose' , -- UserFirstName - varchar(50)
          'Presa' , -- UserLastName - varchar(50)
          @adminRoleId , -- UserRoleId - int
          1 , -- UserActiveIn - bit
          1 , -- UserNotifyFiveDays - bit
          1 , -- UserNotifyTenDays - bit
          null  -- UserPassword - varchar(256)
        )

if not exists (select * from dbo.Users where UserEmail = 'nickolas.wood@hotmail.com')
insert into dbo.Users
        ( UserEmail ,
          UserFirstName ,
          UserLastName ,
          UserRoleId ,
          UserActiveIn ,
          UserNotifyFiveDays ,
          UserNotifyTenDays ,
          UserPassword
        )
values  ( 'nickolas.wood@hotmail.com' , -- UserEmail - varchar(256)
          'Nick' , -- UserFirstName - varchar(50)
          'Wood' , -- UserLastName - varchar(50)
          @adminRoleId , -- UserRoleId - int
          1 , -- UserActiveIn - bit
          1 , -- UserNotifyFiveDays - bit
          1 , -- UserNotifyTenDays - bit
          null  -- UserPassword - varchar(256)
        )
