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

The groups below are the current groups who are in need of a Sort Team Leader. If you are able to lead a group, please go to the calendar using the link and then click on the "I will lead" button.  We appreciate your service to our organization.

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