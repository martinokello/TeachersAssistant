exec dbo.GroupSubmissionsBySubjectRoleAndYear
go
exec dbo.NumberOfStudentsGradedInSubjectAndyearBtwnYears 2019,2019,3
go
exec dbo.AverageAndMedianGradeAttainedBySubjectAcrossAllRolesAndyearBtwnYears 2019,2019,1,''
go
exec dbo.AverageAttainedGradesGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYears 2019,2019,3
go
exec dbo.MedianGradeAttainedGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYears 2019,2019,5
go
exec dbo.AverageAttainedGradesGroupedByGradeAndSubjectAndyearBtwnYears 2019,2019,3,N'Gammar11Plus'
go
exec dbo.MedianGradeAttainedGroupedByGradeAndSubjectAndyearBtwnYears 2019,2019,5,N'Grammar11Plus'
go
if OBJECT_ID('GroupSubmissionsBySubjectRoleAndYear') IS NOT NULL
drop procedure dbo.GroupSubmissionsBySubjectRoleAndYear
go
create procedure dbo.GroupSubmissionsBySubjectRoleAndYear 
AS
select count(stds.StudentId) as NumberOfStudents, sbj.SubjectName, subms.StudentRole, Year(subms.DateDue) as YearDue
from dbo.Students stds inner join dbo.AssignmentSubmissions subms
on stds.StudentId = subms.StudentId 
inner join dbo.Subjects sbj 
on sbj.SubjectId = subms.SubjectId
where subms.DateSubmitted > subms.DateDue
group by sbj.SubjectName, subms.StudentRole,  Year(subms.DateDue)
go

if OBJECT_ID('GroupSubmissionsBySubjectRoleAndYearBtwnYears') IS NOT NULL
drop procedure dbo.GroupSubmissionsBySubjectRoleAndYearBtwnYears
go
create procedure GroupSubmissionsBySubjectRoleAndYearBtwnYears(
 @YearBegin int,
 @YearEnd int,
 @StudentRole nvarchar(max)
) 
AS
select count(stds.StudentId) as NumberOfStudents, sbj.SubjectName, subms.StudentRole, Year(subms.DateDue) as YearDue
from dbo.Students stds inner join dbo.AssignmentSubmissions subms
on stds.StudentId = subms.StudentId 
inner join dbo.Subjects sbj 
on sbj.SubjectId = subms.SubjectId
where subms.DateSubmitted > subms.DateDue and Year(subms.DateDue) >= @YearBegin and Year(subms.DateDue) <= @YearEnd and subms.StudentRole = @StudentRole
group by sbj.SubjectName, subms.StudentRole,  Year(subms.DateDue)
go

if OBJECT_ID('GroupSubmissionsBySubjectRoleAndYearForSubject') IS NOT NULL
drop procedure dbo.GroupSubmissionsBySubjectRoleAndYearForSubject
go
create procedure dbo.GroupSubmissionsBySubjectRoleAndYearForSubject(
 @subjectId int,
 @StudentRole nvarchar(max)
 ) 
AS
select count(stds.StudentId) as NumberOfStudents, sbj.SubjectName, subms.StudentRole, Year(subms.DateDue) as YearDue
from dbo.Students stds inner join dbo.AssignmentSubmissions subms
on stds.StudentId = subms.StudentId 
inner join dbo.Subjects sbj 
on sbj.SubjectId = subms.SubjectId
where subms.DateSubmitted > subms.DateDue and @subjectId = sbj.SubjectId and subms.StudentRole = @StudentRole
group by sbj.SubjectName, subms.StudentRole,  Year(subms.DateDue)
go

if OBJECT_ID('GroupSubmissionsBySubjectRoleAndYearBtwnYearsBySubject') IS NOT NULL
drop procedure dbo.GroupSubmissionsBySubjectRoleAndYearBtwnYearsBySubject
go
create procedure GroupSubmissionsBySubjectRoleAndYearBtwnYearsBySubject(
 @YearBegin int,
 @YearEnd int,
 @SubjectId int,
 @StudentRole nvarchar(max)
) 
AS
select count(stds.StudentId) as NumberOfStudents, sbj.SubjectName, subms.StudentRole, Year(subms.DateDue) as YearDue
from dbo.Students stds inner join dbo.AssignmentSubmissions subms
on stds.StudentId = subms.StudentId 
inner join dbo.Subjects sbj 
on sbj.SubjectId = subms.SubjectId
where subms.DateSubmitted > subms.DateDue and Year(subms.DateDue) >= @YearBegin and Year(subms.DateDue) <= @YearEnd and @subjectId = sbj.SubjectId and subms.StudentRole = @StudentRole
group by sbj.SubjectName, subms.StudentRole,  Year(subms.DateDue)
go
if OBJECT_ID('GroupByNumberOfStudentsReceivedGradesInSubjectAndyearBtwn') IS NOT NULL
drop procedure dbo.GroupByNumberOfStudentsReceivedGradesInSubjectAndyearBtwn
go
create procedure dbo.GroupByNumberOfStudentsReceivedGradesInSubjectAndyearBtwn (
 @YearBegin int,
 @YearEnd int,
 @SubjectId int,
 @StudentRole nvarchar(max)
 )
AS
select count(stds.StudentId) NumberOfStudents, sbj.SubjectName, subms.StudentRole, subms.Grade, Year(subms.DateDue) as YearDue
from dbo.Students stds inner join dbo.AssignmentSubmissions subms
on stds.StudentId = subms.StudentId 
inner join dbo.Subjects sbj 
on sbj.SubjectId = subms.SubjectId
where  @subjectId = sbj.SubjectId and subms.StudentRole = @StudentRole and Year(subms.DateDue) >= @YearBegin and Year(subms.DateDue) <= @YearEnd 
group by sbj.SubjectName, subms.StudentRole, Year(subms.DateDue), subms.Grade
order by subms.Grade
go

if OBJECT_ID('NumberOfStudentsGradedInSubjectAndyearBtwnYears') IS NOT NULL
drop procedure dbo.NumberOfStudentsGradedInSubjectAndyearBtwnYears
go
create procedure dbo.NumberOfStudentsGradedInSubjectAndyearBtwnYears(
 @YearBegin int,
 @YearEnd int,
 @subjectId int
) 
AS
select count(stds.StudentId) as NumberOfStudents, sbj.SubjectName, subms.Grade, Year(subms.DateDue) as YearDue
from dbo.Students stds inner join dbo.AssignmentSubmissions subms
on stds.StudentId = subms.StudentId 
inner join dbo.Subjects sbj 
on sbj.SubjectId = subms.SubjectId
where Year(subms.DateDue) >= @YearBegin and Year(subms.DateDue) <= @YearEnd  and @subjectId = sbj.SubjectId
group by sbj.SubjectName, Year(subms.DateDue), subms.Grade
go

if OBJECT_ID('AverageAttainedGradesGroupedByGradeAndSubjectAndyearBtwnYears') IS NOT NULL
drop procedure dbo.AverageAttainedGradesGroupedByGradeAndSubjectAndyearBtwnYears
go
create procedure dbo.AverageAttainedGradesGroupedByGradeAndSubjectAndyearBtwnYears(
 @YearBegin int,
 @YearEnd int,
 @subjectId int,
 @StudentRole nvarchar(max)
) 
AS
select cast(Avg(subms.GradeNumeric) as decimal) as AverageGrade, sbj.SubjectName, subms.StudentRole, Year(subms.DateDue) as YearDue
from dbo.Students stds inner join dbo.AssignmentSubmissions subms
on stds.StudentId = subms.StudentId 
inner join dbo.Subjects sbj 
on sbj.SubjectId = subms.SubjectId
where sbj.SubjectId = @subjectId and Year(subms.DateDue) >= @YearBegin and Year(subms.DateDue) <= @YearEnd  and subms.StudentRole = @StudentRole
group by sbj.SubjectName, subms.StudentRole,  Year(subms.DateDue), subms.Grade
go

if OBJECT_ID('MedianGradeAttainedGroupedByGradeAndSubjectAndyearBtwnYears') IS NOT NULL
drop procedure dbo.MedianGradeAttainedGroupedByGradeAndSubjectAndyearBtwnYears
go
create procedure dbo.MedianGradeAttainedGroupedByGradeAndSubjectAndyearBtwnYears(
 @YearBegin int,
 @YearEnd int,
 @subjectId int,
 @StudentRole nvarchar(max)
) 
AS
select Avg(GradeNumeric) as MedianGrade, SubjectName, YearDue, Grade, StudentRole
from
	(
		select subms.GradeNumeric as GradeNumeric,subms.Grade as Grade, subms.StudentRole, sbj.SubjectName as SubjectName, Year(subms.DateDue) as YearDue, subms.AssignmentSubmissionId as submissionsId,
		row_number() over(partition by subms.Grade order by subms.GradeNumeric asc, subms.AssignmentSubmissionId asc) as rowAsc,
		row_number() over(partition by subms.Grade order by subms.GradeNumeric desc, subms.AssignmentSubmissionId desc) as rowDes
		from (
				dbo.Students stds inner join dbo.AssignmentSubmissions subms
				on stds.StudentId = subms.StudentId 
				inner join dbo.Subjects sbj 
				on sbj.SubjectId = subms.SubjectId
			)
		where sbj.SubjectId = @subjectId  and Year(subms.DateDue) >= @YearBegin and Year(subms.DateDue) <= @YearEnd  and subms.StudentRole = @StudentRole
	) as x
where x.rowAsc in (x.rowDes, x.rowDes-1, x.rowDes + 1)
group by SubjectName,YearDue, Grade, StudentRole
go

--select * from dbo.AssignmentSubmissions

if OBJECT_ID('AverageAttainedGradesGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYears') IS NOT NULL
drop procedure dbo.AverageAttainedGradesGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYears
go
create procedure dbo.AverageAttainedGradesGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYears(
 @YearBegin int,
 @YearEnd int,
 @subjectId int
) 
AS
select cast(Avg(subms.GradeNumeric) as decimal) as AverageGrade, subms.Grade, sbj.SubjectName, Year(subms.DateDue) as YearDue
from dbo.Students stds inner join dbo.AssignmentSubmissions subms
on stds.StudentId = subms.StudentId 
inner join dbo.Subjects sbj 
on sbj.SubjectId = subms.SubjectId
where sbj.SubjectId = @subjectId and Year(subms.DateDue) >= @YearBegin and Year(subms.DateDue) <= @YearEnd
group by sbj.SubjectName, Year(subms.DateDue), subms.Grade
go

if OBJECT_ID('MedianGradeAttainedGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYears') IS NOT NULL
drop procedure dbo.MedianGradeAttainedGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYears
go
create procedure dbo.MedianGradeAttainedGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYears(
 @YearBegin int,
 @YearEnd int,
 @subjectId int
) 
AS
select Avg(GradeNumeric) as MedianGrade, SubjectName, YearDue, Grade, submissionsId
from
	(
		select subms.GradeNumeric as GradeNumeric,subms.Grade as Grade, sbj.SubjectName as SubjectName, Year(subms.DateDue) as YearDue, subms.AssignmentSubmissionId as submissionsId,
		row_number() over(partition by subms.Grade order by subms.GradeNumeric asc, subms.AssignmentSubmissionId asc) as rowAsc,
		row_number() over(partition by subms.Grade order by subms.GradeNumeric desc, subms.AssignmentSubmissionId desc) as rowDes
		from (
				dbo.Students stds inner join dbo.AssignmentSubmissions subms
				on stds.StudentId = subms.StudentId 
				inner join dbo.Subjects sbj 
				on sbj.SubjectId = subms.SubjectId
			)
		where sbj.SubjectId = @subjectId  and Year(subms.DateDue) >= @YearBegin and Year(subms.DateDue) <= @YearEnd
	) as x
where x.rowAsc in (x.rowDes, x.rowDes-1, x.rowDes + 1)
group by SubjectName,YearDue, Grade, submissionsId
order by submissionsId
go


if OBJECT_ID('AverageAndMedianGradeAttainedBySubjectAcrossAllRolesAndyearBtwnYears') IS NOT NULL
drop procedure dbo.AverageAndMedianGradeAttainedBySubjectAcrossAllRolesAndyearBtwnYears
go
create procedure dbo.AverageAndMedianGradeAttainedBySubjectAcrossAllRolesAndyearBtwnYears(
 @YearBegin int,
 @YearEnd int,
 @subjectId int,
 @StudentRole nvarchar(max)
) 
AS
if @StudentRole = N''
	Begin
	select  Avg(GradeNumeric) as AverageGrade, sbj.SubjectName, Year(subms.DateDue) as YearDue, StudentRole=@StudentRole
	from(				
			dbo.Students stds inner join dbo.AssignmentSubmissions subms
			on stds.StudentId = subms.StudentId 
			inner join dbo.Subjects sbj 
			on sbj.SubjectId = subms.SubjectId
		) where sbj.SubjectId = @subjectId  and Year(subms.DateDue) >= @YearBegin and Year(subms.DateDue) <= @YearEnd
		group by sbj.SubjectName, Year(subms.DateDue)

	select distinct Avg(GradeNumeric) as MedianGrade
	from
		(
			select subms.GradeNumeric as GradeNumeric, subms.AssignmentSubmissionId as submissionsId,
			row_number() over(order by subms.GradeNumeric asc, subms.AssignmentSubmissionId asc) as rowAsc,
			row_number() over(order by subms.GradeNumeric desc, subms.AssignmentSubmissionId desc) as rowDes
			from (
					dbo.Students stds inner join dbo.AssignmentSubmissions subms
					on stds.StudentId = subms.StudentId 
					inner join dbo.Subjects sbj 
					on sbj.SubjectId = subms.SubjectId
				)
			where sbj.SubjectId = @subjectId  and Year(subms.DateDue) >= @YearBegin and Year(subms.DateDue) <= @YearEnd
		) as x
	where x.rowAsc in (x.rowDes, x.rowDes-1, x.rowDes + 1)
	end
else
	begin
	select Avg(GradeNumeric) as AverageGrade, sbj.SubjectName, Year(subms.DateDue) as YearDue, StudentRole=@StudentRole
	from(				
			dbo.Students stds inner join dbo.AssignmentSubmissions subms
			on stds.StudentId = subms.StudentId 
			inner join dbo.Subjects sbj 
			on sbj.SubjectId = subms.SubjectId
		) where sbj.SubjectId = @subjectId  and Year(subms.DateDue) >= @YearBegin and Year(subms.DateDue) <= @YearEnd and subms.StudentRole = @StudentRole
		group by sbj.SubjectName, Year(subms.DateDue)

	select distinct Avg(GradeNumeric) as MedianGrade
	from
		(
			select subms.GradeNumeric as GradeNumeric, subms.AssignmentSubmissionId as submissionsId,
			row_number() over(order by subms.GradeNumeric asc, subms.AssignmentSubmissionId asc) as rowAsc,
			row_number() over(order by subms.GradeNumeric desc, subms.AssignmentSubmissionId desc) as rowDes
			from (
					dbo.Students stds inner join dbo.AssignmentSubmissions subms
					on stds.StudentId = subms.StudentId 
					inner join dbo.Subjects sbj 
					on sbj.SubjectId = subms.SubjectId
				)
			where sbj.SubjectId = @subjectId  and Year(subms.DateDue) >= @YearBegin and Year(subms.DateDue) <= @YearEnd and subms.StudentRole = @StudentRole
		) as x
	where x.rowAsc in (x.rowDes, x.rowDes-1, x.rowDes + 1)
end

------------------------------------------------------ Added Power to Recognize Subject Areas Across Subjects --------------------------------------------------------------------------------------------------



if OBJECT_ID('GroupSubmissionsBySubjectRoleAndYearAcrossSubject') IS NOT NULL
drop procedure dbo.GroupSubmissionsBySubjectRoleAndYearAcrossSubject
go
create procedure dbo.GroupSubmissionsBySubjectRoleAndYearAcrossSubject(
 @subject nvarchar(200),
 @StudentRole nvarchar(max)
 ) 
AS
select count(stds.StudentId) as NumberOfStudents, sbj.SubjectName, subms.StudentRole, Year(subms.DateDue) as YearDue
from dbo.Students stds inner join dbo.AssignmentSubmissions subms
on stds.StudentId = subms.StudentId 
inner join dbo.Subjects sbj 
on sbj.SubjectId = subms.SubjectId
where subms.DateSubmitted > subms.DateDue and sbj.SubjectName like '%'+@subject+'%' and subms.StudentRole = @StudentRole
group by sbj.SubjectName, subms.StudentRole,  Year(subms.DateDue)
go

if OBJECT_ID('GroupSubmissionsBySubjectRoleAndYearBtwnYearsAcrossSubject') IS NOT NULL
drop procedure dbo.GroupSubmissionsBySubjectRoleAndYearBtwnYearsAcrossSubject
go
create procedure GroupSubmissionsBySubjectRoleAndYearBtwnYearsAcrossSubject(
 @YearBegin int,
 @YearEnd int,
 @subject nvarchar(200),
 @StudentRole nvarchar(max)
) 
AS
select count(stds.StudentId) as NumberOfStudents, sbj.SubjectName, subms.StudentRole, Year(subms.DateDue) as YearDue
from dbo.Students stds inner join dbo.AssignmentSubmissions subms
on stds.StudentId = subms.StudentId 
inner join dbo.Subjects sbj 
on sbj.SubjectId = subms.SubjectId
where subms.DateSubmitted > subms.DateDue and Year(subms.DateDue) >= @YearBegin and Year(subms.DateDue) <= @YearEnd and sbj.SubjectName like '%'+@subject+'%' and subms.StudentRole = @StudentRole
group by sbj.SubjectName, subms.StudentRole,  Year(subms.DateDue)
go
if OBJECT_ID('GroupByNumberOfStudentsReceivedGradesInSubjectAndyearBtwnAcrossSubject') IS NOT NULL
drop procedure dbo.GroupByNumberOfStudentsReceivedGradesInSubjectAndyearBtwnAcrossSubject
go
create procedure dbo.GroupByNumberOfStudentsReceivedGradesInSubjectAndyearBtwnAcrossSubject (
 @YearBegin int,
 @YearEnd int,
 @subject nvarchar(200),
 @StudentRole nvarchar(max)
 )
AS
select count(stds.StudentId) NumberOfStudents, sbj.SubjectName, subms.StudentRole, subms.Grade, Year(subms.DateDue) as YearDue
from dbo.Students stds inner join dbo.AssignmentSubmissions subms
on stds.StudentId = subms.StudentId 
inner join dbo.Subjects sbj 
on sbj.SubjectId = subms.SubjectId
where   sbj.SubjectName like '%'+@subject+'%'  and subms.StudentRole = @StudentRole and Year(subms.DateDue) >= @YearBegin and Year(subms.DateDue) <= @YearEnd 
group by sbj.SubjectName, subms.StudentRole, Year(subms.DateDue), subms.Grade
order by subms.Grade
go

if OBJECT_ID('NumberOfStudentsGradedInSubjectAndyearBtwnYearsAcrossSubject') IS NOT NULL
drop procedure dbo.NumberOfStudentsGradedInSubjectAndyearBtwnYearsAcrossSubject
go
create procedure dbo.NumberOfStudentsGradedInSubjectAndyearBtwnYearsAcrossSubject(
 @YearBegin int,
 @YearEnd int,
 @subject nvarchar(200)
) 
AS
select count(stds.StudentId) as NumberOfStudents, sbj.SubjectName, subms.Grade, Year(subms.DateDue) as YearDue
from dbo.Students stds inner join dbo.AssignmentSubmissions subms
on stds.StudentId = subms.StudentId 
inner join dbo.Subjects sbj 
on sbj.SubjectId = subms.SubjectId
where Year(subms.DateDue) >= @YearBegin and Year(subms.DateDue) <= @YearEnd  and sbj.SubjectName like '%'+@subject+'%' 
group by sbj.SubjectName, Year(subms.DateDue), subms.Grade
go

if OBJECT_ID('AverageAttainedGradesGroupedByGradeAndSubjectAndyearBtwnYearsAcrossSubject') IS NOT NULL
drop procedure dbo.AverageAttainedGradesGroupedByGradeAndSubjectAndyearBtwnYearsAcrossSubject
go
create procedure dbo.AverageAttainedGradesGroupedByGradeAndSubjectAndyearBtwnYearsAcrossSubject(
 @YearBegin int,
 @YearEnd int,
 @subject nvarchar(200),
 @StudentRole nvarchar(max)
) 
AS
select cast(Avg(subms.GradeNumeric) as decimal) as AverageGrade, sbj.SubjectName, subms.StudentRole, Year(subms.DateDue) as YearDue
from dbo.Students stds inner join dbo.AssignmentSubmissions subms
on stds.StudentId = subms.StudentId 
inner join dbo.Subjects sbj 
on sbj.SubjectId = subms.SubjectId
where  sbj.SubjectName like '%'+@subject+'%'  and Year(subms.DateDue) >= @YearBegin and Year(subms.DateDue) <= @YearEnd  and subms.StudentRole = @StudentRole
group by sbj.SubjectName, subms.StudentRole,  Year(subms.DateDue), subms.Grade
go

if OBJECT_ID('MedianGradeAttainedGroupedByGradeAndSubjectAndyearBtwnYearsAcrossSubject') IS NOT NULL
drop procedure dbo.MedianGradeAttainedGroupedByGradeAndSubjectAndyearBtwnYearsAcrossSubject
go
create procedure dbo.MedianGradeAttainedGroupedByGradeAndSubjectAndyearBtwnYearsAcrossSubject(
 @YearBegin int,
 @YearEnd int,
 @subject nvarchar(200),
 @StudentRole nvarchar(max)
) 
AS
select Avg(GradeNumeric) as MedianGrade, SubjectName, YearDue, Grade, StudentRole
from
	(
		select subms.GradeNumeric as GradeNumeric,subms.Grade as Grade, subms.StudentRole, sbj.SubjectName as SubjectName, Year(subms.DateDue) as YearDue, subms.AssignmentSubmissionId as submissionsId,
		row_number() over(partition by subms.Grade order by subms.GradeNumeric asc, subms.AssignmentSubmissionId asc) as rowAsc,
		row_number() over(partition by subms.Grade order by subms.GradeNumeric desc, subms.AssignmentSubmissionId desc) as rowDes
		from (
				dbo.Students stds inner join dbo.AssignmentSubmissions subms
				on stds.StudentId = subms.StudentId 
				inner join dbo.Subjects sbj 
				on sbj.SubjectId = subms.SubjectId
			)
		where  sbj.SubjectName like '%'+@subject+'%' and Year(subms.DateDue) >= @YearBegin and Year(subms.DateDue) <= @YearEnd  and subms.StudentRole = @StudentRole
	) as x
where x.rowAsc in (x.rowDes, x.rowDes-1, x.rowDes + 1)
group by SubjectName,YearDue, Grade, StudentRole
go

--select * from dbo.AssignmentSubmissions

if OBJECT_ID('AverageAttainedGradesGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYearsAcrossSubject') IS NOT NULL
drop procedure dbo.AverageAttainedGradesGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYearsAcrossSubject
go
create procedure dbo.AverageAttainedGradesGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYearsAcrossSubject(
 @YearBegin int,
 @YearEnd int,
 @subject nvarchar(200)
) 
AS
select cast(Avg(subms.GradeNumeric) as decimal) as AverageGrade, subms.Grade, sbj.SubjectName, Year(subms.DateDue) as YearDue
from dbo.Students stds inner join dbo.AssignmentSubmissions subms
on stds.StudentId = subms.StudentId 
inner join dbo.Subjects sbj 
on sbj.SubjectId = subms.SubjectId
where sbj.SubjectName like '%'+@subject+'%' and Year(subms.DateDue) >= @YearBegin and Year(subms.DateDue) <= @YearEnd
group by sbj.SubjectName, Year(subms.DateDue), subms.Grade
go

if OBJECT_ID('MedianGradeAttainedGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYearsAcrossSubject') IS NOT NULL
drop procedure dbo.MedianGradeAttainedGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYearsAcrossSubject
go
create procedure dbo.MedianGradeAttainedGroupedByGradeAndSubjectAcrossAllRolesAndyearBtwnYearsAcrossSubject(
 @YearBegin int,
 @YearEnd int,
 @subject nvarchar(200)
) 
AS
select Avg(GradeNumeric) as MedianGrade, SubjectName, YearDue, Grade, submissionsId
from
	(
		select subms.GradeNumeric as GradeNumeric,subms.Grade as Grade, sbj.SubjectName as SubjectName, Year(subms.DateDue) as YearDue, subms.AssignmentSubmissionId as submissionsId,
		row_number() over(partition by subms.Grade order by subms.GradeNumeric asc, subms.AssignmentSubmissionId asc) as rowAsc,
		row_number() over(partition by subms.Grade order by subms.GradeNumeric desc, subms.AssignmentSubmissionId desc) as rowDes
		from (
				dbo.Students stds inner join dbo.AssignmentSubmissions subms
				on stds.StudentId = subms.StudentId 
				inner join dbo.Subjects sbj 
				on sbj.SubjectId = subms.SubjectId
			)
		where sbj.SubjectName like '%'+@subject+'%' and Year(subms.DateDue) >= @YearBegin and Year(subms.DateDue) <= @YearEnd
	) as x
where x.rowAsc in (x.rowDes, x.rowDes-1, x.rowDes + 1)
group by SubjectName,YearDue, Grade, submissionsId
order by submissionsId
go


if OBJECT_ID('AverageAndMedianGradeAttainedBySubjectAcrossAllRolesAndyearBtwnYearsAcrossSubject') IS NOT NULL
drop procedure dbo.AverageAndMedianGradeAttainedBySubjectAcrossAllRolesAndyearBtwnYearsAcrossSubject
go
create procedure dbo.AverageAndMedianGradeAttainedBySubjectAcrossAllRolesAndyearBtwnYearsAcrossSubject(
 @YearBegin int,
 @YearEnd int,
 @subject nvarchar(200),
 @StudentRole nvarchar(max)
) 
AS
if @StudentRole = N''
	Begin
	select  Avg(GradeNumeric) as AverageGrade, sbj.SubjectName, Year(subms.DateDue) as YearDue, StudentRole=@StudentRole
	from(				
			dbo.Students stds inner join dbo.AssignmentSubmissions subms
			on stds.StudentId = subms.StudentId 
			inner join dbo.Subjects sbj 
			on sbj.SubjectId = subms.SubjectId
		) where sbj.SubjectName like '%'+@subject+'%' and Year(subms.DateDue) >= @YearBegin and Year(subms.DateDue) <= @YearEnd
		group by sbj.SubjectName, Year(subms.DateDue)

	select distinct Avg(GradeNumeric) as MedianGrade
	from
		(
			select subms.GradeNumeric as GradeNumeric, subms.AssignmentSubmissionId as submissionsId,
			row_number() over(order by subms.GradeNumeric asc, subms.AssignmentSubmissionId asc) as rowAsc,
			row_number() over(order by subms.GradeNumeric desc, subms.AssignmentSubmissionId desc) as rowDes
			from (
					dbo.Students stds inner join dbo.AssignmentSubmissions subms
					on stds.StudentId = subms.StudentId 
					inner join dbo.Subjects sbj 
					on sbj.SubjectId = subms.SubjectId
				)
			where sbj.SubjectName like '%'+@subject+'%' and Year(subms.DateDue) >= @YearBegin and Year(subms.DateDue) <= @YearEnd
		) as x
	where x.rowAsc in (x.rowDes, x.rowDes-1, x.rowDes + 1)
	end
else
	begin
	select Avg(GradeNumeric) as AverageGrade, sbj.SubjectName, Year(subms.DateDue) as YearDue, StudentRole=@StudentRole
	from(				
			dbo.Students stds inner join dbo.AssignmentSubmissions subms
			on stds.StudentId = subms.StudentId 
			inner join dbo.Subjects sbj 
			on sbj.SubjectId = subms.SubjectId
		) where sbj.SubjectName like '%'+@subject+'%' and Year(subms.DateDue) >= @YearBegin and Year(subms.DateDue) <= @YearEnd and subms.StudentRole = @StudentRole
		group by sbj.SubjectName, Year(subms.DateDue)

	select distinct Avg(GradeNumeric) as MedianGrade
	from
		(
			select subms.GradeNumeric as GradeNumeric, subms.AssignmentSubmissionId as submissionsId,
			row_number() over(order by subms.GradeNumeric asc, subms.AssignmentSubmissionId asc) as rowAsc,
			row_number() over(order by subms.GradeNumeric desc, subms.AssignmentSubmissionId desc) as rowDes
			from (
					dbo.Students stds inner join dbo.AssignmentSubmissions subms
					on stds.StudentId = subms.StudentId 
					inner join dbo.Subjects sbj 
					on sbj.SubjectId = subms.SubjectId
				)
			where sbj.SubjectName like '%'+@subject+'%' and Year(subms.DateDue) >= @YearBegin and Year(subms.DateDue) <= @YearEnd and subms.StudentRole = @StudentRole
		) as x
	where x.rowAsc in (x.rowDes, x.rowDes-1, x.rowDes + 1)
end
