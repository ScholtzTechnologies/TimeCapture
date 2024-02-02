/** Object:  Table [dbo].[SystemUsers]    Script Date: 2023/10/24 11:55:23 **/
SET ANSI_NULLS ON


SET QUOTED_IDENTIFIER ON

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SystemUsers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SystemUsers](
	[ID] [int] NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Password] [varchar](255) NOT NULL,
	[Email] [varchar](255) NULL,
	[Role] [int] NOT NULL,
	[License] [varchar](255) NULL,
	CONSTRAINT [CK_User_ID] CHECK  (([ID]<>NULL AND [ID]>(0))),
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END

if not exists (select 1 from SystemUsers where ID = 1)
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

IF COL_LENGTH('SystemUsers', 'ID') IS NULL
BEGIN
	ALTER TABLE SystemUsers ADD ID INT NOT NULL
END


IF COL_LENGTH('SystemUsers', 'Name') IS NULL
BEGIN
	ALTER TABLE SystemUsers ADD Name VARCHAR(255) NOT NULL
END


IF COL_LENGTH('SystemUsers', 'Password') IS NULL
BEGIN
	ALTER TABLE SystemUsers ADD Password VARCHAR(255) NULL
END


IF COL_LENGTH('SystemUsers', 'Email') IS NULL
BEGIN
	ALTER TABLE SystemUsers ADD Email VARCHAR(255) NULL
END


IF COL_LENGTH('SystemUsers', 'Role') IS NULL
BEGIN
	ALTER TABLE SystemUsers ADD Role INT NULL
END


IF COL_LENGTH('SystemUsers', 'License') IS NULL
BEGIN
	ALTER TABLE SystemUsers ADD License VARCHAR(255) NULL
END
