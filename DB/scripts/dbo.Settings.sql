CREATE TABLE [dbo].[Settings] (
    [ID]          INT           NOT NULL,
    [Name]        VARCHAR (50)  NOT NULL,
    [Value]       VARCHAR (255)  NOT NULL,
    [Description] VARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


--#####################################
--	Insert Script with Default Values #
--#####################################

if Not exists (select 1 from Settings where ID = -1)
	insert into Settings
	Values (
		-1, 'IsBusinessModel', '0', 'Set whether the app is in the business model'
	)

if Not exists (select 1 from Settings where ID = 1)
	insert into Settings
	Values (
		1, 'ShowCloseMessage', '0', 'Show the Message asking the user to confirm'
	)

if Not exists (select 1 from Settings where ID = 2)
	insert into Settings
	Values (
		2,	'RemoveDesc', '0', 'Removes the Description Field'
	)

if Not exists (select 1 from Settings where ID = 3)
	insert into Settings
	Values (
		3,	'RemoveTicketNo', '0', 'Removes the Ticket Number dropdown'
	)

if Not exists (select 1 from Settings where ID = 4)
	insert into Settings
	Values (
		4, 'IsToasts', '0', 'Show notifications or messages'
	)

if Not exists (select 1 from Settings where ID = 5)
	insert into Settings
	Values (
		5, 'Username', '', 'The username used when capturing time'
	)

if Not exists (select 1 from Settings where ID = 6)
	insert into Settings
	Values (
		6, 'Password', '', 'The password used when capturing time'
	)

if Not exists (select 1 from Settings where ID = 7)
	insert into Settings
	Values (
		7, 'IsSelenium', '0', 'The username used when capturing time'
	)

if Not exists (select 1 from Settings where ID = 8)
	insert into Settings
	Values (
		8, 'URL', '', 'The URL used for capturing time'
	)