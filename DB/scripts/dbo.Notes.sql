CREATE TABLE [dbo].[Notes] (
    [NoteID]   INT           NULL,
    [Name]     VARCHAR (255) NOT NULL,
    [Note]     VARCHAR (255) NOT NULL,
    [Date]     VARCHAR (255) NOT NULL,
    [ParentID] INT           NOT NULL,
    CONSTRAINT [CK_Notes_NoteID] CHECK ([NoteID]<>NULL AND [NoteID]>(0))
);


GO

CREATE TRIGGER [dbo].[Trigger_Notes]
ON [dbo].[Notes]
FOR INSERT
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if any rows were inserted with NULL NoteID
    IF EXISTS (SELECT * FROM inserted WHERE NoteID IS NULL)
    BEGIN
        -- Set NoteID for NULL values
        UPDATE n
        SET n.NoteID = ISNULL((SELECT MAX(NoteID) + 1 FROM Notes), 1)
        FROM Notes n
        INNER JOIN inserted i ON n.NoteID = i.NoteID
        WHERE i.NoteID IS NULL;
    END;
END;