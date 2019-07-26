create database Sixagenv2
go

use Sixagenv2
go

Create table Login(
ID int primary key identity, 
Usuario varchar(50) not null,
Clave varchar(50) not null
)

go

Create table Empleados(
ID int primary key identity,
Nombre varchar(50) not null,
Puesto varchar(100) not null,
Fecha_Contratacion date,
Telefono varchar(50) not null,
Email varchar(50) not null,
IDUsuario int references Login(ID)
)

go

Create table Clientes(
ID int primary key identity,
Nombre varchar(50) not null,
Direccion varchar(300) not null,
Telefono varchar(50) not null,
Email varchar(50) not null,
IDUsuario int references Login(ID)
)

go

Create table Agendas(
ID int primary key identity,
Motivo varchar(50) not null,
Descripcion varchar(300) not null,
Cliente int references Clientes(ID),
Empleado int references Empleados(ID),
Fecha date not null, 
Hora varchar(10) not null,
Herramientas_Necesarias varchar(100)
)

go

Create table Equipos_Reparacion(
ID int primary key identity,
Nombre_Equipo varchar(50) not null,
Descripcion varchar(50) not null,
Dueno int references Clientes(ID),
Empleado int references Empleados(ID),
Problema varchar(100) not null,
Estado varchar(15) check (Estado in('Dañado', 'Reparado')) not null,
Fecha_Entrada date
)

go

Create table Productos(
ID int primary key identity,
Producto varchar(50) not null,
Modelo varchar(100) not null,
Descripcion varchar(300) not null,
Cantidad int not null,
Precio decimal(18,2) not null,
Imagen varchar(100) not null
--Fecha_Vencimiento date
)

go

Create table Ventas(
ID int primary key identity,
Producto int References Productos(ID) not null,
Descripcion varchar(300),
Cantidad int not null,
Precio_total decimal(18,2) not null,
Cliente int references Clientes(ID),
Empleado int references Empleados(ID)
)

go

create table Roles(
ID int primary key identity,
Role varchar(30) not null,
IDUsuario int references Login(ID)
)

go


alter table Login add unique (Usuario)

go

Insert into Login values('MarcosRestituyo', 'Markito2000')
Insert into Login values('IsmelRosario', 'Markito2000')
Select * from Login

insert into Roles values('Admin', 1)
insert into Roles values('Empleado', 2)
select * from Roles


Insert into Empleados values('Marcos Restituyo', 'Jefe','2019-03-26', '809-616-9743', 'markitofrikistin@hotmail.com', 1)
select * from Empleados


Insert into Clientes values('Ismel Rosario', 'Los guandules C/San francisco #14', '809-616-9743', '20175534@itla.edu.do', 2)
select * from Clientes


Insert into Agendas values('Television da�ada', 'Direccion: Respaldo San Francisco #24', 1, 1, '2019-04-05', '3:30:00', 'Destornillador, Tester, Soldador, Esta�o')
select * from Agendas

Select a.ID, Motivo, e.* from Agendas a join Empleados e on(a.Empleado = e.ID)


Insert into Equipos_Reparacion values('Inversor', 'Inversor Leyba', 1, 1, 'El inversor se apaga rapido, al parecer no carga', 'Dañado', '2019-03-27')
select * from Equipos_Reparacion


Insert into Productos values('Televisor', 'ASD521RT', 'Samsung, 33 pulgadas, Smart TV', 5, 32500.55, '../../Imagenes/1.jpg')
Insert into Productos values('Componente', 'AYTSDFR', 'Samsung, 2 Bocinas', 5, 7500.55, '../../Imagenes/2.jpg')
Insert into Productos values('Inversor', '5to', 'Phase II, 1 Kilo', 3, 8500.20, '../../Imagenes/3.png')
Insert into Productos values('Celular', 'J7 Prime', 'Samsung, Color Rosado', 5, 7500.10, '../../Imagenes/4.jpg')
select * from Productos


Insert into Ventas values(1, 'Samsung, 33 pulgadas, Smart TV', 1, 32500.55, 1, 1)
select * from Ventas

go