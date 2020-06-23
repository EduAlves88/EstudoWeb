CREATE PROCEDURE ExcluirTimePorId(
	@TimeID int
	)
as
begin
	Delete From Times where TimeID = @TimeID
end
=========================================================================
CREATE PROCEDURE AtualizarTime(
	@TimeId int, 
	@Time nvarchar(50), 
	@Estado char(2), 
	@Cores nvarchar(50)
	)
as
begin
	Update Times set Time = @Time, 
	Estado = @estado, 
	Cores = @Cores 
	where TimeId = @TimeId 
end
=========================================================================
CREATE PROCEDURE IncluirTime(
	@Time nvarchar(50), 
	@Estado char(2), 
	@Cores nvarchar(50)
	)
as
begin
	Insert into Times values(
		@Time,
		@Estado,
		@Cores
		)
end
========================================================================
CREATE PROCEDURE ObterTimes
as
begin
	Select * from Times
end