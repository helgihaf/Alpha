--
-- Autogenerated Oracle script for database schema project
-- Generated at 2012-12-20T17:44:58
-- Knightrunner.Library.Database.Schema.Oracle.OracleScriptGenerator, Knightrunner.Library.Database.Schema.Oracle, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null

create sequence sr.sr_repository_seq;
create table sr.sr_repository
(
	id_repository number(*,0) not null,
	name varchar2(255) not null,
	created date not null,
	created_by varchar2(255) not null,
	modified date not null,
	modified_by varchar2(255) not null,
	constraint sr_repository_pk primary key (id_repository),
	constraint sr_repository_name_uq unique (name)
);
/
create or replace trigger sr.sr_repository_new
 before
  insert or update
 on sr.sr_repository
referencing new as new old as old
 for each row
 when (nvl(new.id_repository,0) = 0)
begin
  select sr.sr_repository_seq.nextval into :new.id_repository from dual;
end;
/
create sequence sr.sr_software_item_seq;
create table sr.sr_software_item
(
	id_software_item number(*,0) not null,
	id_repository number(*,0) not null,
	name varchar2(1000) not null,
	software_item_type number(3,0) not null,
	status number(3,0) not null,
	id_team number(*,0),
	description varchar2(2000),
	description_url varchar2(1000),
	deployment_directory varchar2(500),
	implementation varchar2(1000),
	source_control_path varchar2(500),
	drop_location varchar2(500),
	build_name varchar2(255),
	log_application_name varchar2(255),
	log_application_version varchar2(255),
	external_id varchar2(100),
	constraint sr_software_item_pk primary key (id_software_item),
	constraint sr_software_item_id_name_uk unique (id_repository, name),
	constraint sr_software_item_repository_fk foreign key (id_repository) references sr.sr_repository (id_repository),
	constraint sr_software_item_id_team_fk foreign key (id_team) references sr.sr_team (id_team)
);
/
create or replace trigger sr.sr_software_item_new
 before
  insert or update
 on sr.sr_software_item
referencing new as new old as old
 for each row
 when (nvl(new.id_software_item,0) = 0)
begin
  select sr.sr_software_item_seq.nextval into :new.id_software_item from dual;
end;
/
create sequence sr.sr_file_item_seq;
create table sr.sr_file_item
(
	id_file_item number(*,0) not null,
	id_software_item number(*,0) not null,
	file_path varchar2(500) not null,
	version varchar2(255),
	constraint sr_file_item_pk primary key (id_file_item),
	constraint sr_file_item_path_uk unique (id_software_item, file_path),
	constraint sr_file_item_software_item_fk foreign key (id_software_item) references sr.sr_software_item (id_software_item)
);
/
create or replace trigger sr.sr_file_item_new
 before
  insert or update
 on sr.sr_file_item
referencing new as new old as old
 for each row
 when (nvl(new.id_file_item,0) = 0)
begin
  select sr.sr_file_item_seq.nextval into :new.id_file_item from dual;
end;
/
create sequence sr.sr_measurement_seq;
create table sr.sr_measurement
(
	id_measurement number(*,0) not null,
	id_software_item number(*,0) not null,
	measurement_type number(3,0) not null,
	created date not null,
	period number(10,0) not null,
	detail varchar2(255),
	measurement_value number(*,0) not null,
	constraint sr_measurement_pk primary key (id_measurement),
	constraint sr_measurement_uk unique (id_software_item, measurement_type, period, detail),
	constraint sr_measurement_fk foreign key (id_software_item) references sr.sr_software_item (id_software_item)
);
/
create or replace trigger sr.sr_measurement_new
 before
  insert or update
 on sr.sr_measurement
referencing new as new old as old
 for each row
 when (nvl(new.id_measurement,0) = 0)
begin
  select sr.sr_measurement_seq.nextval into :new.id_measurement from dual;
end;
/
create sequence sr.sr_dependency_seq;
create table sr.sr_dependency
(
	id_dependency number(*,0) not null,
	id_software_item_from number(*,0) not null,
	id_software_item_to number(*,0) not null,
	source number(3,0) not null,
	type number(3,0) not null,
	constraint sr_dependency_pk primary key (id_dependency),
	constraint sr_dependency_from_to_uk unique (id_software_item_from, id_software_item_to),
	constraint sr_dependency_from_fk foreign key (id_software_item_from) references sr.sr_software_item (id_software_item),
	constraint sr_dependency_to_fk foreign key (id_software_item_to) references sr.sr_software_item (id_software_item)
);
/
create or replace trigger sr.sr_dependency_new
 before
  insert or update
 on sr.sr_dependency
referencing new as new old as old
 for each row
 when (nvl(new.id_dependency,0) = 0)
begin
  select sr.sr_dependency_seq.nextval into :new.id_dependency from dual;
end;
/
create sequence sr.sr_measurement_bookmark_seq;
create table sr.sr_measurement_bookmark
(
	id_measurement_bookmark number(*,0) not null,
	id_repository number(*,0) not null,
	measurement_type number(3,0) not null,
	last_datetime date not null,
	constraint sr_measurement_bookmark_pk primary key (id_measurement_bookmark),
	constraint sr_measurement_bookmark_uq unique (id_repository, measurement_type),
	constraint sr_measurement_bookmark_fk foreign key (id_repository) references sr.sr_repository (id_repository)
);
/
create or replace trigger sr.sr_measurement_bookmark_new
 before
  insert or update
 on sr.sr_measurement_bookmark
referencing new as new old as old
 for each row
 when (nvl(new.id_measurement_bookmark,0) = 0)
begin
  select sr.sr_measurement_bookmark_seq.nextval into :new.id_measurement_bookmark from dual;
end;
/
create sequence sr.sr_team_seq;
create table sr.sr_team
(
	id_team number(*,0) not null,
	name varchar2(255) not null,
	active number(1,0) not null,
	contact varchar2(1000),
	external_id varchar2(100),
	constraint sr_team_pk primary key (id_team),
	constraint sr_team_uq unique (name)
);
/
create or replace trigger sr.sr_team_new
 before
  insert or update
 on sr.sr_team
referencing new as new old as old
 for each row
 when (nvl(new.id_team,0) = 0)
begin
  select sr.sr_team_seq.nextval into :new.id_team from dual;
end;
/
