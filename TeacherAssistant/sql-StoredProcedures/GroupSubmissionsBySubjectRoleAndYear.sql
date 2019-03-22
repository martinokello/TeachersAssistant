exec dbo.GroupSubmissionsBySubjectRoleAndYear
go
exec dbo.PercentileGroupedByGradeAndSubjectAndyear
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
if OBJECT_ID('PercentileGroupedByGradeAndSubjectAndyear') IS NOT NULL
drop procedure dbo.PercentileGroupedByGradeAndSubjectAndyear
go
create procedure dbo.PercentileGroupedByGradeAndSubjectAndyear 
AS
select count(stds.StudentId) * 100/(select count(*) from dbo.Students st inner join dbo.AssignmentSubmissions sb on st.StudentId = sb.StudentId inner join dbo.Subjects sj on sj.SubjectId = sb.SubjectId) as Percentile, sbj.SubjectName, subms.StudentRole, subms.Grade, Year(subms.DateDue) as YearDue
from dbo.Students stds inner join dbo.AssignmentSubmissions subms
on stds.StudentId = subms.StudentId 
inner join dbo.Subjects sbj 
on sbj.SubjectId = subms.SubjectId
group by sbj.SubjectName, subms.StudentRole, Year(subms.DateDue), subms.Grade
go

if OBJECT_ID('PercentileGroupedByGradeAndSubjectAndyearBtwnYears') IS NOT NULL
drop procedure dbo.PercentileGroupedByGradeAndSubjectAndyearBtwnYears
go
create procedure dbo.PercentileGroupedByGradeAndSubjectAndyearBtwnYears(
 @YearBegin int,
 @YearEnd int,
 @subjectId int,
 @StudentRole nvarchar(max)
) 
AS
select count(stds.StudentId) * 100/(select count(*) from dbo.Students st inner join dbo.AssignmentSubmissions sb on st.StudentId = sb.StudentId inner join dbo.Subjects sj1 on sj1.SubjectId = @subjectId and StudentRole = @StudentRole) as Percentile, sbj.SubjectName, subms.StudentRole, subms.Grade, Year(subms.DateDue) as YearDue
from dbo.Students stds inner join dbo.AssignmentSubmissions subms
on stds.StudentId = subms.StudentId 
inner join dbo.Subjects sbj 
on sbj.SubjectId = subms.SubjectId
where Year(subms.DateDue) >= @YearBegin and Year(subms.DateDue) <= @YearEnd  and @subjectId = sbj.SubjectId  and subms.StudentRole = @StudentRole
group by sbj.SubjectName, subms.StudentRole,  Year(subms.DateDue), subms.Grade
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
select cast((select top 1 percentile_cont(0.5) within group(order by subms.GradeNumeric) over (partition by  Year(subms.DateDue))) as decimal) as MedianGrade, sbj.SubjectName, subms.StudentRole, Year(subms.DateDue) as YearDue
from dbo.Students stds inner join dbo.AssignmentSubmissions subms
on stds.StudentId = subms.StudentId 
inner join dbo.Subjects sbj 
on sbj.SubjectId = subms.SubjectId
where sbj.SubjectId = @subjectId  and Year(subms.DateDue) >= @YearBegin and Year(subms.DateDue) <= @YearEnd  and subms.StudentRole = @StudentRole
group by sbj.SubjectName, subms.GradeNumeric, subms.StudentRole, Year(subms.DateDue)
go
--select * from dbo.AssignmentSubmissions