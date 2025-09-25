CREATE TABLE [Framework].[User]
(
    [Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    [Name] NVARCHAR(255) NOT NULL,
    [CreatedBy] UNIQUEIDENTIFIER NOT NULL,
    [LastModifiedBy] UNIQUEIDENTIFIER NOT NULL,
    [CreatedDateTime] DATETIME2(7) NOT NULL DEFAULT GETUTCDATE(),
    [LastModifiedDateTime] DATETIME2(7) NOT NULL DEFAULT GETUTCDATE(),
    
    CONSTRAINT [PK_Framework_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

-- Add indexes for common queries
CREATE NONCLUSTERED INDEX [IX_Framework_User_Name] 
ON [Framework].[User] ([Name] ASC);
GO

CREATE NONCLUSTERED INDEX [IX_Framework_User_CreatedBy] 
ON [Framework].[User] ([CreatedBy] ASC);
GO
