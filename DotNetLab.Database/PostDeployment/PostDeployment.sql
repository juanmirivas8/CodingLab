-- This file contains SQL statements that will be executed after the build script.
-- Create a system user ID for initial data seeding and system operations
DECLARE @SystemUserId UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000001';

-- Check if system user exists, if not create it
IF NOT EXISTS (SELECT 1 FROM [Framework].[User] WHERE [Id] = @SystemUserId)
BEGIN
    INSERT INTO [Framework].[User] ([Id], [Name], [CreatedBy], [LastModifiedBy], [CreatedDateTime], [LastModifiedDateTime])
    VALUES (@SystemUserId, 'System', @SystemUserId, @SystemUserId, GETUTCDATE(), GETUTCDATE());
    
    PRINT 'System user created successfully.';
END
ELSE
BEGIN
    PRINT 'System user already exists.';
END
GO

-- Create an admin user for testing purposes
DECLARE @AdminUserId UNIQUEIDENTIFIER = '11111111-1111-1111-1111-111111111111';
DECLARE @SystemUserId UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000001';

IF NOT EXISTS (SELECT 1 FROM [Framework].[User] WHERE [Id] = @AdminUserId)
BEGIN
    INSERT INTO [Framework].[User] ([Id], [Name], [CreatedBy], [LastModifiedBy], [CreatedDateTime], [LastModifiedDateTime])
    VALUES (@AdminUserId, 'Administrator', @SystemUserId, @SystemUserId, GETUTCDATE(), GETUTCDATE());
    
    PRINT 'Admin user created successfully.';
END
ELSE
BEGIN
    PRINT 'Admin user already exists.';
END
GO

PRINT 'PostDeployment script completed successfully.';
GO
