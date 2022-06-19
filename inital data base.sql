DECLARE @PermissionsId uniqueidentifier
DECLARE @RoleId uniqueidentifier 

DECLARE @RolePermissionsId uniqueidentifier

DECLARE @UserId uniqueidentifier
DECLARE @UserPassword nvarchar
DECLARE @UserPasswordSalt nvarchar
DECLARE @UserRegisterDate datetime2

set @RoleId = NEWID()
set @PermissionsId = NEWID()

set @RolePermissionsId = NEWID()

set @UserId = NEWID()
set @UserPassword = '16b9039a42439f494d2a407e922b667e0490b23ddeeff3eb46d727a30af9f173'
set @UserPasswordSalt = '30d0b94f-6af2-49c7-9b54-da2b2287c5e9'
set @UserRegisterDate = GETDATE()

insert into Permissions(Id,Title,PermissionFlag) values (@PermissionsId,'Add A New Product','product-add')

insert into Roles (Id,IsActive,RoleName) values (@RoleId,1,'Admin')

insert into RolePermissions (Id,PermissionId,RoleId) 
values (@RolePermissionsId,@PermissionsId,@RoleId)

insert into users (Id,Title,Password,PasswordSalt,RegisterDate,LastLoginDate) 
values 
(@UserId,'admin',
'2c27958bb665532251d415fcbd751107a641a6972f4444507cb2e6cb91c83944',
'8e94cd23-154a-42a8-8aff-a42c6429e338',
@UserRegisterDate,
@UserRegisterDate)

insert into UserRoles (Id,RoleId,UserId) 
values (NEWID(),@RoleId,@UserId)