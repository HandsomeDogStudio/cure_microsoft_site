USE [ProjectCure]
GO
/****** Object:  Table [dbo].[Templates]    Script Date: 03/07/2014 15:42:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Templates](
	[TemplateId] [int] IDENTITY(1,1) NOT NULL,
	[TemplateName] [varchar](50) NOT NULL,
	[TemplateText] [varchar](max) NOT NULL,
	[TemplateSubject] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Templates] PRIMARY KEY CLUSTERED 
(
	[TemplateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 03/07/2014 15:42:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](256) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Users]    Script Date: 03/07/2014 15:42:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserEmail] [varchar](256) NOT NULL,
	[UserFirstName] [varchar](50) NULL,
	[UserLastName] [varchar](50) NULL,
	[UserRoleId] [int] NOT NULL,
	[UserActiveIn] [bit] NOT NULL,
	[UserNotifyFiveDays] [bit] NOT NULL,
	[UserNotifyTenDays] [bit] NOT NULL,
	[UserPassword] [varchar](256) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Events]    Script Date: 03/07/2014 15:42:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Events](
	[EventId] [int] IDENTITY(1,1) NOT NULL,
	[EventTitle] [varchar](256) NOT NULL,
	[EventStartDateTime] [datetime2] NOT NULL,
	[EventEndDateTime] [datetime2] NOT NULL,
	[EventStatus] [bit] NOT NULL,
	[EventManagerId] [int] NULL,
	[EventDescription] [varchar](max) NULL,
 CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  ForeignKey [FK_Events_Users]    Script Date: 03/07/2014 15:42:58 ******/
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_Users] FOREIGN KEY([EventManagerId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK_Events_Users]
GO
/****** Object:  ForeignKey [FK_Users_Roles]    Script Date: 03/07/2014 15:42:58 ******/
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([UserRoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO

if not exists (select * from dbo.Roles where RoleName = 'Admin')
insert into dbo.Roles
        ( RoleName )
values  ( 'Admin'  -- RoleName - varchar(256)
          )

if not exists (select * from dbo.Roles where RoleName = 'Manager')
insert into dbo.Roles
        ( RoleName )
values  ( 'Manager'  -- RoleName - varchar(256)
          )


if not exists (SELECT * FROM dbo.Templates where TemplateName = 'Password Reset Email')
insert INTO dbo.Templates
		(TemplateName, TemplateText, TemplateSubject)
values ('Password Reset Email', 'Dear {name},

Your account password at Project C.U.R.E has been reset and you have been issued with a new temporary password {temp password}.
Please go to this page and change your password:
http://projectcure.azurewebsites.net/

Thanks,
The Project C.U.R.E. Team',
'Project C.U.R.E - Password reset confirmation')


if not exists (SELECT * from dbo.Templates where  TemplateName = 'Registration Confirmation Email')
INSERT INTO dbo.Templates
		(TemplateName, TemplateText, TemplateSubject)
VALUES ('Confirmation Email', 'Dear {name},

Thank you for signing up to lead {title} on {date} from {start time} to {end time}.  We appreciate your service to our organization.

Thanks,
The Project C.U.R.E. Team',
'Project C.U.R.E - Registration Confirmation')

if not exists (SELECT * from dbo.Templates where  TemplateName = 'Cancellation Email')
INSERT INTO dbo.Templates
		(TemplateName, TemplateText, TemplateSubject)
VALUES ('Cancellation Email', 'Dear {name},

You have successfully canceled your reservation to lead the event for {title} on {date} from {start time} to {end time}.  Please sign up again soon!  
Thanks,

The Project C.U.R.E. Team',
'Project C.U.R.E - Cancellation')

if not exists (SELECT * from dbo.Templates where  TemplateName = 'Unfilled Group Lead Email')
INSERT INTO dbo.Templates
		(TemplateName, TemplateText, TemplateSubject)
VALUES ('Unfilled Group Lead Email', 'Dear Sort Team Leaders,

The groups below are the current groups who are in need of a Sort Team Leader. 
If you are able to lead a group, please go to the calendar using the link and then click on the "I will lead" button.  We appreciate your service to our organization.

{events}

http://projectcure.azurewebsites.net/Home/Calendar

Thanks,
The Project C.U.R.E. Team',
'Project C.U.R.E - Unfilled Group Lead')

if not exists (SELECT * from dbo.Templates where  TemplateName = 'Reminder Email')
INSERT INTO dbo.Templates
		(TemplateName, TemplateText, TemplateSubject)
VALUES ('Reminder Email', 'Dear {name},

This is a reminder that you are scheduled to lead {title} on {date} from {start time} to {end time}.  Thank you for your service to our organization.

Thanks,
The Project C.U.R.E. Team',
'Project C.U.R.E - Reminder')

if not exists (SELECT * FROM dbo.Templates where TemplateName = 'Password Change Confirmation Email')
insert INTO dbo.Templates
		(TemplateName, TemplateText, TemplateSubject)
values ('Password Change Confirmation Email', 'Dear {name},

Thank you for visiting the Project C.U.R.E website. As per your request, we have successfully changed your password.

Thanks,
The Project C.U.R.E. Team',
'Project C.U.R.E - Password change confirmation')

