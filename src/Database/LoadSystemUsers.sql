INSERT INTO [ProjectCure].[dbo].[Users]
           ([UserEmail]
           ,[UserFirstName]
           ,[UserLastName]
           ,[UserRoleId]
           ,[UserActiveIn]
           ,[UserNotifyFiveDays]
           ,[UserNotifyTenDays]
           ,[UserPassword])
     VALUES
(N'louisfischer@gmail.com', N'Louis', N'Fischer', N'1', N'1', N'1', N'1', N'5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8'),
(N'm.ellement@gmail.com', N'Matthew', N'Ellement', N'1', N'1', N'0', N'0', N'd95c3086066012ccd19bc644765a6692086c227a3c356e58ea4c586caa501d6f'),
(N'annieellement@projectcure.org', N'Annie', N'Ellement', N'1', N'1', N'0', N'0', N'd95c3086066012ccd19bc644765a6692086c227a3c356e58ea4c586caa501d6f'),
(N'lindseymoore@projectcure.org', N'Lindsey', N'Moore', N'1', N'1', N'0', N'0', N'd95c3086066012ccd19bc644765a6692086c227a3c356e58ea4c586caa501d6f')
