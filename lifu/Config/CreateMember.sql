Create PROCEDURE [dbo].[CreateMember]
                @Account nvarchar(50),
                @Password nvarchar(100),
                @PasswordSalt nvarchar(100),
                @Name nvarchar(50),
                @Email nvarchar(50),
                @JobTitle nvarchar(50),
                @Permission nvarchar(500),
                @Poster nvarchar(20),
                @UnitId int,
                @Gender int,
                @MyPic nvarchar(50),
                @Id   int OUTPUT
AS
   DECLARE @ReturnCode    int
                 DECLARE @memberCount    int
                 select @memberCount= count(Id) from Members where Account= @Account
                 if @memberCount> 0
                 begin
                                 set  @ReturnCode= 0
                 end
                 else
                 begin
                                 insert into Members (
                                                Account ,Password,PasswordSalt, Name,Email,JobTitle ,Permission, Poster,UnitId ,Gender, MyPic
                                 ) VALUES
        (
                                   @Account,@Password,@PasswordSalt, @Name,@Email ,@JobTitle, @Permission,@Poster ,@UnitId, @Gender,@MyPic
                                 )
                                 set @ReturnCode = @@Identity
                 end
                 Select @Id= @ReturnCode;
                

SELECT * FROM Members  WHERE Id = @id;
