USE RwaApartmani

UPDATE [AspNetUsers] SET PasswordHash=CONVERT(varchar(max), HASHBYTES ('SHA2_512', '123') ,2) -- SET SHA512 PW

INSERT INTO AspNetRoles (Name) VALUES ('Admin'), ('User') -- Add ROLES
INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES (1,1), (2,2) -- Combine Roles and Users

GO
--Procedure
CREATE PROCEDURE AuthUser
	@username NVARCHAR(50),
	@password NVARCHAR(128)
AS
BEGIN
	SELECT * FROM [dbo].[AspNetUsers] WHERE Username=@username AND PasswordHash=@password
END
GO

-- PROC: LoadUsers
CREATE PROCEDURE LoadUsers
AS
BEGIN
	SELECT * FROM [dbo].[AspNetUsers]
END
GO

-- PROC: AddUser
CREATE PROCEDURE AddUser
	@email nvarchar(50),
	@password nvarchar(max),
	@phoneNumber nvarchar(50),
	@username NVARCHAR(50),
	@adress nvarchar(50)
AS
BEGIN
	INSERT INTO [dbo].[AspNetUsers]([Email],[EmailConfirmed],[PasswordHash],[PhoneNumber],[PhoneNumberConfirmed],
	[LockoutEnabled],[AccessFailedCount],[UserName],[Address]) 
	VALUES(@email,1, @password ,@phoneNumber,1,0,0,@username,@adress)
END
GO

-- PROC: SaveUser
CREATE PROCEDURE SaveUser
	@email nvarchar(50),
	@phoneNumber nvarchar(50),
	@username NVARCHAR(50),
	@adress nvarchar(50),
	@userID INT
AS
BEGIN
	UPDATE [dbo].[AspNetUsers] SET Email=@email, PhoneNumber=@phoneNumber, Username=@username, Address = @adress WHERE Id=@userID
END
GO

-- PROC: AddApartment
CREATE PROCEDURE AddApartment
	@ownerId int,
	@typeId int,
	@statusId int,
	@cityId int,
	@adress nvarchar(50),
	@name NVARCHAR(50),
	@nameEng NVARCHAR(50),
	@price money,
	@maxAdults int,
	@maxChildren int,
	@totalRooms int,
	@beachDistance int
AS
BEGIN
	INSERT INTO [dbo].[Apartment](OwnerId, TypeId, StatusId, CityId, Address, Name, NameEng, Price, MaxAdults, MaxChildren, TotalRooms, BeachDistance) 
	VALUES(@ownerId, @typeId, @statusId, @cityId, @adress, @name, @nameEng, @price, @maxAdults, @maxChildren, @totalRooms, @beachDistance)
	SELECT Id FROM Apartment WHERE Id = @@IDENTITY
END
GO

-- PROC: SaveApartment
CREATE PROCEDURE SaveApartment
	@ownerId int,
	@statusId int,
	@cityId int,
	@adress nvarchar(50),
	@name NVARCHAR(50),
	@nameEng NVARCHAR(50),
	@price money,
	@maxAdults int,
	@maxChildren int,
	@totalRooms int,
	@beachDistance int,
	@apartmentID INT
AS
BEGIN
	UPDATE [dbo].[Apartment] SET Name=@name, NameEng=@nameEng, OwnerId=@ownerId, StatusId=@statusId, CityId=@cityId,
	Address = @adress ,Price=@price, MaxAdults=@maxAdults, MaxChildren=@maxChildren, TotalRooms=@totalRooms, BeachDistance = @beachDistance 
	WHERE Id=@apartmentID
END
GO

--PRCO: DeleteApartment
CREATE PROCEDURE DeleteApartment
	@apartmentID INT
AS
BEGIN
	UPDATE[dbo].[Apartment] SET DeletedAt=GETDATE() WHERE Id=@apartmentID
END
GO

-- PROC: LoadApartments
CREATE PROCEDURE LoadApartments
AS
BEGIN
	SELECT 
		apart.Id as apartId, 
		apart.Address as apartAddress, 
		apart.Name as apartName,
		apart.NameEng as apartNameEng,
		apart.Price as apartPrice,
		apart.MaxAdults as apartMaxAdults,
		apart.MaxChildren as apartMaxChildren,
		apart.TotalRooms as apartTotalRooms,
		apart.BeachDistance as apartBeachDistance,
		apartO.Id as ownerId,
		apartO.Name as ownerName,
		apartS.Id as statusId,
		apartS.Name as statusName,
		apartS.NameEng as statusNameEng,
		city.Id as cityId,
		city.Name as cityName
	FROM [dbo].[Apartment] AS apart
	INNER JOIN ApartmentOwner as apartO ON apart.OwnerId = apartO.Id
	INNER JOIN ApartmentStatus as apartS ON apart.StatusId = apartS.Id
	INNER JOIN City	as city ON apart.CityId = city.id
	WHERE apart.DeletedAt IS NULL
END
GO

CREATE PROCEDURE LoadFreeApartments
AS
BEGIN
	SELECT * FROM [dbo].[Apartment] WHERE DeletedAt IS NULL AND StatusId = 3
END
GO

-- PROC: LoadApartment
CREATE PROCEDURE LoadApartment
	@apartmentID INT
AS
BEGIN
	SELECT * FROM [dbo].[Apartment] WHERE Id = @apartmentID
END
GO

-- PROC: LoadUser
CREATE PROCEDURE LoadUser
	@userId INT
AS
BEGIN
	SELECT * FROM [dbo].[AspNetUsers] WHERE Id = @userId
END
GO

-- PROC: LoadApartmentOwner
CREATE PROCEDURE LoadApartmentOwner
AS
BEGIN
	SELECT * FROM [dbo].[ApartmentOwner]
END
GO

-- PROC: LoadApartmentStatus
CREATE PROCEDURE LoadApartmentStatus
AS
BEGIN
	SELECT * FROM [dbo].[ApartmentStatus]
END
GO

-- PROC: LoadCity
CREATE PROCEDURE LoadCity
AS
BEGIN
	SELECT * FROM [dbo].[City]
END
GO

-- PROC: LoadTags
CREATE PROCEDURE LoadTags
AS
BEGIN
	SELECT * FROM [dbo].[Tag]
END
GO

CREATE PROCEDURE LoadApartmentTags
	@apartmentId INT
AS
BEGIN
	SELECT * FROM [dbo].[TaggedApartment] WHERE ApartmentId = @apartmentId
END
GO

CREATE PROCEDURE AddApartmentTag
	@apartmentId INT,
	@tagId INT
AS
BEGIN
	INSERT INTO TaggedApartment (ApartmentId, TagId) VALUES (@apartmentId, @tagId)
END
GO

CREATE PROCEDURE DeleteApartmentTagByApartmentId
	@apartmentId INT
AS
BEGIN
	DELETE FROM TaggedApartment WHERE ApartmentId = @apartmentId
END
GO

-- PROC: LoadTagTypes
CREATE PROCEDURE LoadTagTypes
AS
BEGIN
	SELECT * FROM [dbo].[TagType]
END
GO

-- PROC: SaveTag
CREATE PROCEDURE SaveTag
	@name nvarchar(255),
	@nameEng nvarchar(255),
	@tagTypeId int,
	@tagID INT
AS
BEGIN
	UPDATE [dbo].[Tag] SET Name=@name, NameEng=@nameEng, TypeId=@tagTypeId WHERE Id=@tagID
END
GO

--PRCO: DeleteTag
CREATE PROCEDURE DeleteTag
	@tagID INT
AS
BEGIN
	DELETE FROM TaggedApartment WHERE TagId=@tagID
	DELETE FROM [dbo].[Tag] WHERE Id=@tagID
END
GO

--PROC: AddTag
CREATE PROCEDURE AddTag
	@tagTypeId int,
	@name nvarchar(255),
	@nameEng nvarchar(255)
AS
BEGIN
INSERT INTO [dbo].[Tag](TypeId, Name, NameEng)
VALUES (@tagTypeId, @name, @nameEng)
END
GO

--PROCS: ApartmentPictures
CREATE PROCEDURE AddApartmentPicture
	@apartmentId int,
	@path nvarchar(max),
	@base64Content nvarchar(max),
	@name nvarchar(max),
	@isRepresentative bit
AS
BEGIN
	INSERT INTO [dbo].[ApartmentPicture](CreatedAt, ApartmentId, Path, Base64Content, Name, IsRepresentative)
	VALUES (GETDATE(), @apartmentId, @path, @base64Content, @name, @isRepresentative)
END
GO

--PROCS: LoadApartmentPicture
CREATE PROCEDURE LoadApartmentPicture
	@apartmentId INT
AS
BEGIN
	SELECT * FROM ApartmentPicture WHERE ApartmentId=@apartmentId
END
GO

--PROCS: LoadTaggedApartmentByApartmentId
CREATE PROCEDURE LoadTaggedApartmentByApartmentId
	@apartmentId INT
AS
BEGIN
	SELECT 
		taggedApart.Id as taggedApartId,
		tag.Id as tagId,
		tag.Name as tagName
	FROM TaggedApartment as taggedApart
	INNER JOIN Tag as tag ON taggedApart.TagId = tag.Id
	WHERE taggedApart.ApartmentId = @apartmentId
END
GO

--PROC: LoadApartmentPictureByApartmentId
CREATE PROCEDURE LoadApartmentPictureByApartmentId
	@apartmentId INT
AS
BEGIN
	SELECT Id, Name, Path, IsRepresentative
	FROM ApartmentPicture WHERE
	ApartmentId = @apartmentId and DeletedAt IS NULL AND Name IS NOT NULL AND Path IS NOT NULL AND Base64Content IS NOT NULL
END
GO

--PROC: DeleteApartmentImageById
CREATE PROCEDURE DeleteApartmentImageById
	@id INT
AS
BEGIN
	UPDATE ApartmentPicture SET DeletedAt = GETDATE() WHERE Id = @id
END
GO

--PROC: AddApartmentReservation
CREATE PROCEDURE AddApartmentReservation
	@apartmentId int,
	@details nvarchar(255),
	@userId int,
	@userName nvarchar(255),
	@userEmail nvarchar(255),
	@userPhoneNumber nvarchar(255),
	@userAddress nvarchar(255)
AS
BEGIN
INSERT INTO ApartmentReservation(ApartmentId, Details,UserId, UserName, UserEmail, UserPhone, UserAddress) 
VALUES (@apartmentId, @details,@userId, @userName, @userEmail, @userPhoneNumber, @userAddress)
END
GO