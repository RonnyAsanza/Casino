use Casino;

SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserKey], [UserName], [Password], [Money], [Enabled], [DateCreatedUtc], [DateModifiedUtc]) VALUES (1, N'Ronny Asanza', '123', 140, 1,  CAST(N'2023-06-04T16:48:02.3366667' AS DateTime2),  CAST(N'2023-06-04T16:48:02.3366667' AS DateTime2))

SET IDENTITY_INSERT [dbo].[User] OFF
