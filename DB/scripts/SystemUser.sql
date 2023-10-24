/** Object:  Table [dbo].[SystemUsers]    Script Date: 2023/10/24 11:55:23 **/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SystemUsers](
	[ID] [int] NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Password] [varchar](255) NOT NULL,
	[Email] [varchar](255) NULL,
	[Role] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[SystemUsers]  WITH CHECK ADD  CONSTRAINT [CK_User_ID] CHECK  (([ID]<>NULL AND [ID]>(0)))
GO

ALTER TABLE [dbo].[SystemUsers] CHECK CONSTRAINT [CK_User_ID]
GO

if not exists (select 1 from SystemUsers where SystemUserID = 1)
begin
	Insert into SystemUsers 
	Values (
		1,
		'Admin',
		'f1tLamipQScZXSS67Upk5FBgXx/chf2gvx1IZQFubig=',
		'braydengls@gmail.com',
		1
	)
end